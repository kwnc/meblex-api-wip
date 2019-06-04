using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using AgileObjects.AgileMapper;
using Dawn;
using Meblex.API.Context;
using Meblex.API.DTO;
using Meblex.API.FormsDto.Request;
using Meblex.API.FormsDto.Response;
using Meblex.API.Helper;
using Meblex.API.Interfaces;
using Meblex.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Meblex.API.Services
{
    public class FurnitureService:IFurnitureService
    {
        private readonly MeblexDbContext _context;
        public FurnitureService(MeblexDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddFurniture(List<string> photos, PieceOfFurnitureAddDto pieceOfFurniture)
        {
            var cat = _context.Categories.SingleOrDefault(x => x.CategoryId == pieceOfFurniture.CategoryId);
            var room = _context.Rooms.SingleOrDefault(x => x.RoomId == pieceOfFurniture.RoomId);

            if (cat == null || room == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, _localizer["Room or category doesn't exist"]);
            }

            var duplicate = _context.Furniture.Any(x => x.Name == pieceOfFurniture.Name);
            if (duplicate)
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, _localizer["Furniture part already exists"]);
            }

            var pieceOfFurnitureInserted = _context.Furniture.Add(new PieceOfFurniture()
            {
                Name = pieceOfFurniture.Name,
                Description = pieceOfFurniture.Description,
                CategoryId = pieceOfFurniture.CategoryId,
                RoomId = pieceOfFurniture.RoomId,
                Size = pieceOfFurniture.Size,
                Price = pieceOfFurniture.Price,
                Count = pieceOfFurniture.Count,
                MaterialId = pieceOfFurniture.MaterialId,
                PatternId = pieceOfFurniture.PatternId,
                ColorId = pieceOfFurniture.ColorId
            });

            _context.SaveChanges();

            foreach (var photo in photos)
            {
                _context.Photos.Add(new Photo() {Path = photo, PieceOfFurnitureId = pieceOfFurnitureInserted.Entity.PieceOfFurnitureId});
            }

            _context.SaveChanges();

            foreach (var partId in pieceOfFurniture.PartsId)
            {
                var part =  await _context.Parts.SingleOrDefaultAsync(x => x.PartId == partId);
                part.PieceOfFurnitureId = pieceOfFurnitureInserted.Entity.PieceOfFurnitureId;
            }

            _context.SaveChanges();

            return pieceOfFurnitureInserted.Entity.PieceOfFurnitureId;
        }

        public int AddMaterial(string photoName, MaterialAddForm material)
        {
            var id = AddOne<Material, MaterialAddForm>(material, new List<string>() {nameof(MaterialAddForm.Name)});
            _context.MaterialPhotos.Add(new MaterialPhoto() {MaterialId = id, Path = photoName});
            if (_context.SaveChanges() == 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, _localizer["Coud not add data"]);
            }

            return id;
        }

        public int AddPattern(string photoName, PatternAddForm pattern)
        {
            var id = AddOne<Pattern, PatternAddForm>(pattern, new List<string>() { nameof(PatternAddForm.Name) });
            _context.PatternPhotos.Add(new PatternPhoto() { PatternId = id, Path = photoName });
            if (_context.SaveChanges() == 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, _localizer["Coud not add data"]);
            }

            return id;
        }

        public FurnitureResponse GetPieceOfFurniture(int id)
        {
            var Id = Guard.Argument(id, nameof(id)).NotZero().NotNegative().Value;
            var pieceOfFurniture = _context.Furniture.Find(Id) ?? throw new HttpStatusCodeException(HttpStatusCode.NotFound, _localizer["Furtniture with this ID exists"]);

            var parts = pieceOfFurniture.Parts?.Select(x =>
                            {
                                var materialPart = x.Material;
                                var patternPart = x.Pattern;
                                var materialPartResponse = Mapper.Map(materialPart).ToANew<MaterialResponse>();
                                var patterPartResponse = Mapper.Map(patternPart).ToANew<PatternsResponse>();
                                materialPartResponse.Photo = materialPart.Photo.Path;
                                patterPartResponse.Photo = patternPart.Photo.Path;
                                return new FurniturePartResponse()
                                {
                                    Name = x.Name,
                                    Count = x.Count,
                                    PartId = x.PartId,
                                    Price = x.Price,
                                    Material = materialPartResponse,
                                    Pattern = patterPartResponse,
                                    Color = Mapper.Map(x.Color).ToANew<ColorsResponse>()
                                };
                            })
                            .ToList() ?? new List<FurniturePartResponse>();
            var room = pieceOfFurniture.Room;
            var category = pieceOfFurniture.Category;
            var color = pieceOfFurniture.Color;
            var material = pieceOfFurniture.Material;
            var pattern = pieceOfFurniture.Pattern;
            var materialResponse = Mapper.Map(material).ToANew<MaterialResponse>();
            materialResponse.Photo = material.Photo.Path;
            var patternResponse = Mapper.Map(pattern).ToANew<PatternsResponse>();
            patternResponse.Photo = pattern.Photo.Path;
            return new FurnitureResponse()
            {
                Id = pieceOfFurniture.PieceOfFurnitureId,
                Name = pieceOfFurniture.Name,
                Description = pieceOfFurniture.Description,
                Room = Mapper.Map(room).ToANew<RoomsResponse>(),
                Category = Mapper.Map(category).ToANew<CategoryResponse>(),
                Parts = parts,
                Size = pieceOfFurniture.Size,
                Price = pieceOfFurniture.Price,
                Count = pieceOfFurniture.Count,
                Photos = pieceOfFurniture.Photos?.Select(x => x.Path).ToList() ?? new List<string>(),
                Pattern =  patternResponse,
                Material = materialResponse,
                Color = Mapper.Map(color).ToANew<ColorsResponse>()
            };
        }

        public List<FurnitureResponse> GetAllFurniture()
        {
            var furniture = _context.Furniture
                .Include(x => x.Category)
                .Include(x => x.Room)
                .Include(x => x.Photos)
                .Include(x => x.Parts)
                .ThenInclude(x => x.Pattern)
                .Include(x => x.Parts)
                .ThenInclude(x => x.Color)
                .Include(x => x.Parts)
                .ThenInclude(x => x.Material);

            var response = new List<FurnitureResponse>();
            foreach (var pieceOfFurniture in furniture)
            {
                
                var parts = pieceOfFurniture.Parts?.Select(x =>
                                {
                                    var materialPart = x.Material;
                                    var patternPart = x.Pattern;
                                    var materialPartResponse = Mapper.Map(materialPart).ToANew<MaterialResponse>();
                                    var patterPartResponse = Mapper.Map(patternPart).ToANew<PatternsResponse>();
                                    materialPartResponse.Photo = materialPart.Photo.Path;
                                    patterPartResponse.Photo = patternPart.Photo.Path;
                                    return new FurniturePartResponse()
                                    {
                                        Name = x.Name,
                                        Count = x.Count,
                                        PartId = x.PartId,
                                        Price = x.Price,
                                        Material = materialPartResponse,
                                        Pattern = patterPartResponse,
                                        Color = Mapper.Map(x.Color).ToANew<ColorsResponse>()
                                    };
                                })
                    .ToList() ?? new List<FurniturePartResponse>();
                var room = pieceOfFurniture.Room ?? throw new HttpStatusCodeException(HttpStatusCode.NotFound, _localizer["Furniture with this ID"] + pieceOfFurniture.PieceOfFurnitureId + _localizer[" doesn't have room"]);
                var category = pieceOfFurniture.Category ?? throw new HttpStatusCodeException(HttpStatusCode.NotFound, _localizer["Furniture with this ID"] + pieceOfFurniture.PieceOfFurnitureId + _localizer["doesn't own a category"]);
                var pattern = pieceOfFurniture.Pattern;
                var color = pieceOfFurniture.Color;
                var material = pieceOfFurniture.Material;
                var materialResponse = Mapper.Map(material).ToANew<MaterialResponse>();
                materialResponse.Photo = material.Photo.Path;
                var patternResponse = Mapper.Map(pattern).ToANew<PatternsResponse>();
                patternResponse.Photo = pattern.Photo.Path;
                var final = new FurnitureResponse()
                {
                    Id = pieceOfFurniture.PieceOfFurnitureId,
                    Name = pieceOfFurniture.Name,
                    Description = pieceOfFurniture.Description,
                    Room = Mapper.Map(room).ToANew<RoomsResponse>(),
                    Category = Mapper.Map(category).ToANew<CategoryResponse>(),
                    Parts = parts,
                    Size = pieceOfFurniture.Size,
                    Price = pieceOfFurniture.Price,
                    Count = pieceOfFurniture.Count,
                    Photos = pieceOfFurniture.Photos.Select(x => x.Path).ToList(),
                    Pattern = patternResponse,
                    Material = materialResponse,
                    Color = Mapper.Map(color).ToANew<ColorsResponse>()
                };
                response.Add(final);
            }

            return response;
        }

        public TResponse GetSingle<TEntity, TResponse>(int id) where TEntity : class where TResponse : class
        {
            var db = _context.Find<TEntity>(id);
            if(db == null) throw new HttpStatusCodeException(HttpStatusCode.NotFound, _localizer["Some bug"]);
            return Mapper.Map(db).ToANew<TResponse>();
        }

        public List<TResponse> GetAll<TEntity, TResponse>() where TEntity : class where TResponse : class
        {
            var db = _context.Set<TEntity>().ToList();
            return db.Select(x => Mapper.Map(x).ToANew<TResponse>()).ToList();
        }

        public int AddOne<TEntity, TDto>(TDto toAdd, List<string> duplicates) where TEntity : class where TDto : class
        {
            var propertiesToCheckDuplicates = duplicates.Select(x => typeof(TDto).GetProperty(x)).ToList();
            var toDb = Mapper.Map(toAdd).ToANew<TEntity>();
            var dbSet = _context.Set<TEntity>();
            if (propertiesToCheckDuplicates != null)
            {
                foreach (var set in dbSet.ToList())
                {
                    foreach (var prop in propertiesToCheckDuplicates)
                    {
                        var p1 = set.GetType().GetProperty(prop.Name).GetValue(set, null);
                        var p2 = prop.GetValue(toAdd, null);
                        var duplicate = Equals(p1, p2);
                        if (duplicate)
                        {
                            throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, _localizer["Already exists"]);
                        }
                    }
                }
                    
            }
            dbSet.Add(toDb);
            
            if (_context.SaveChanges() == 0) throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, _localizer["Could not add data"]);

            return (int) toDb.GetType().GetProperty(typeof(TEntity).Name + "Id").GetValue(toDb);
        }

        public int AddPart(PartAddForm part)
        {
            var toAdd = Mapper.Map(part).ToANew<Part>();
            var duplicate = _context.Parts.Any(x => x.Name == part.Name && x.PieceOfFurnitureId == part.PieceOfFurnitureId);
            if (duplicate)
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, _localizer["Already exists"]);
            }

            toAdd.Color = _context.Colors.Find(part.ColorId) ?? throw new HttpStatusCodeException(HttpStatusCode.NotFound, _localizer["Could not find a color"]); 
            toAdd.Pattern = _context.Patterns.Find(part.PatternId) ?? throw new HttpStatusCodeException(HttpStatusCode.NotFound, _localizer["Could not find a pattern"]); 
            toAdd.Material = _context.Materials.Find(part.MaterialId) ?? throw new HttpStatusCodeException(HttpStatusCode.NotFound, _localizer["Could not find a material"]); 
            toAdd.PieceOfFurniture = _context.Furniture.Find(part.PieceOfFurnitureId) ??  throw new HttpStatusCodeException(HttpStatusCode.NotFound, _localizer["Could not find furniture part"]); ;
            _context.Parts.Add(toAdd);
            if (_context.SaveChanges() == 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, _localizer["Could not add data"]);
            }

            return toAdd.PartId;
        }

        public string GetMaterialPhoto(int id)
        {
            var Id = Guard.Argument(id, nameof(id)).NotNegative();

            var photo = _context.Materials.FirstOrDefault(x => x.MaterialId == Id)?.Photo?.Path;

            if (photo == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, _localizer["Could not find a picture"]);
            }

            return photo;
        }

        public Dictionary<int, string> GetAllMaterialPhoto()
        {
            var rows = _context.MaterialPhotos;

            return rows.ToDictionary(row => row.MaterialId, row => row.Path);
        }

        public string GetPatternPhoto(int id)
        {
            var Id = Guard.Argument(id, nameof(id)).NotNegative().NotZero().Value;

            var photo = _context.Patterns.FirstOrDefault(x => x.PatternId == Id)?.Photo?.Path;

            if (photo == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, _localizer["Could not find a picture"]);
            }

            return photo;
        }

        public Dictionary<int, string> GetAllPatternPhoto()
        {
            var rows = _context.PatternPhotos;

            return rows.ToDictionary(row => row.PatternId, row => row.Path);
        }

        public void RemoveById<TEntity>(int id) where TEntity : class
        {
            var toRemove = _context.Find<TEntity>(id);
            if (toRemove == null) throw new HttpStatusCodeException(HttpStatusCode.NotFound, _localizer["Some bug"]);
            _context.Set<TEntity>().Remove(toRemove);
            if(_context.SaveChanges() == 0) throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, _localizer["Could not delete data"]);
        }
    }
}
