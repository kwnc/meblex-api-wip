using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using AgileObjects.AgileMapper;
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
            var cat = _context.Categories.FirstOrDefault(x => x.CategoryId == pieceOfFurniture.CategoryId);
            var room = _context.Rooms.FirstOrDefault(x => x.RoomId == pieceOfFurniture.RoomId);

            if (cat == null || room == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, "Room or Category does not exist");
            }

            var duplicate = _context.Furniture.Any(x => x.Name == pieceOfFurniture.Name);
            if (duplicate)
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, "Piece of furniture already exist");
            }

            var pieceOfFurnitureInserted = _context.Furniture.Add(new PieceOfFurniture()
            {
                Name = pieceOfFurniture.Name,
                Description = pieceOfFurniture.Description,
                CategoryId = pieceOfFurniture.CategoryId,
                RoomId = pieceOfFurniture.RoomId,
                Size = pieceOfFurniture.Size,
                Price = pieceOfFurniture.Price,
                Count = pieceOfFurniture.Count
            });

            _context.SaveChanges();

            foreach (var photo in photos)
            {
                _context.Photos.Add(new Photo() {Path = photo, PieceOfFurnitureId = pieceOfFurnitureInserted.Entity.PieceOfFurnitureId});
            }

            _context.SaveChanges();

            foreach (var partId in pieceOfFurniture.PartsId)
            {
                var part =  await _context.Parts.FirstOrDefaultAsync(x => x.PartId == partId);
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
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, "Unable to add data");
            }

            return id;
        }

        public int AddPattern(string photoName, PatternAddForm pattern)
        {
            var id = AddOne<Pattern, PatternAddForm>(pattern, new List<string>() { nameof(PatternAddForm.Name) });
            _context.PatternPhotos.Add(new PatternPhoto() { PatternId = id, Path = photoName });
            if (_context.SaveChanges() == 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, "Unable to add data");
            }

            return id;
        }

        public FurnitureResponse GetPieceOfFurniture(int id)
        {
            var pieceOfFurniture = _context.Furniture.Find(id);
            if (pieceOfFurniture == null) throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, "Furniture with that id does not exist");
            var parts = pieceOfFurniture.Parts.Select(x => new FurniturePartResponse()
            {
                Name = x.Name,
                Count = x.Count,
                PartId = x.PartId,
                Price = x.Price,
                Material = Mapper.Map(x.Material).ToANew<MaterialResponse>(),
                Pattern = Mapper.Map(x.Pattern).ToANew<PatternsResponse>(),
                Color = Mapper.Map(x.Color).ToANew<ColorsResponse>()
            }).ToList();

            return new FurnitureResponse()
            {
                Id = pieceOfFurniture.PieceOfFurnitureId,
                Name = pieceOfFurniture.Name,
                Description = pieceOfFurniture.Description,
                RoomId = pieceOfFurniture.RoomId,
                CategoryId = pieceOfFurniture.CategoryId,
                Parts = parts,
                Size = pieceOfFurniture.Size,
                Price = pieceOfFurniture.Price,
                Count = pieceOfFurniture.Count,
                PhotoNames = pieceOfFurniture.Photos?.Select(x => x.Path).ToList() ?? new List<string>()
            };
        }

        public List<FurnitureResponse> GetAllFurniture()
        {
            var furniture = _context.Furniture;
            var response = new List<FurnitureResponse>();
            foreach (var pieceOfFurniture in furniture)
            {
                var parts = pieceOfFurniture.Parts.Select(x => new FurniturePartResponse()
                {
                    Name = x.Name,
                    Count = x.Count,
                    PartId = x.PartId,
                    Price = x.Price,
                    Material = Mapper.Map(x.Material).ToANew<MaterialResponse>(),
                    Pattern = Mapper.Map(x.Pattern).ToANew<PatternsResponse>(),
                    Color = Mapper.Map(x.Color).ToANew<ColorsResponse>()
                }).ToList();

                var add = new FurnitureResponse()
                {
                    Id = pieceOfFurniture.PieceOfFurnitureId,
                    Name = pieceOfFurniture.Name,
                    Description = pieceOfFurniture.Description,
                    RoomId = pieceOfFurniture.RoomId,
                    CategoryId = pieceOfFurniture.CategoryId,
                    Parts = parts,
                    Size = pieceOfFurniture.Size,
                    Price = pieceOfFurniture.Price,
                    Count = pieceOfFurniture.Count,
                    PhotoNames = pieceOfFurniture.Photos.Select(x => x.Path).ToList()
                };
                response.Add(add);
            }

            return response;
        }

        public TResponse GetSingle<TEntity, TResponse>(int id) where TEntity : class where TResponse : class
        {
            var db = _context.Find<TEntity>(id);
            if(db == null) throw new HttpStatusCodeException(HttpStatusCode.NotFound);
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
                            throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, "Already exist");
                        }
                    }
                }
                    
            }
            dbSet.Add(toDb);
            
            if (_context.SaveChanges() == 0) throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, "Unable to add data to db");

            return (int) toDb.GetType().GetProperty(typeof(TEntity).Name + "Id").GetValue(toDb);
        }

        public int AddPart(PartAddForm part)
        {
            var toAdd = Mapper.Map(part).ToANew<Part>();
            var duplicate = _context.Parts.Any(x => x.Name == part.Name && x.PieceOfFurnitureId == part.PieceOfFurnitureId);
            if (duplicate)
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, "Already exist");
            }
            _context.Parts.Add(toAdd);
            if (_context.SaveChanges() == 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, "Unable to add data");
            }

            return toAdd.PartId;
        }



    }
}
