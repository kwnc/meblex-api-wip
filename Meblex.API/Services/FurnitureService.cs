using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AgileObjects.AgileMapper;
using Meblex.API.Context;
using Meblex.API.DTO;
using Meblex.API.FormsDto.Response;
using Meblex.API.Helper;
using Meblex.API.Interfaces;
using Meblex.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

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

        public FurnitureResponse GetPieceOfFurniture(int id)
        {
            var pieceOfFurniture = _context.Furniture.Find(id);
            if (pieceOfFurniture == null) throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, "Furniture with that id does not exist");
            return new FurnitureResponse()
            {
                Id = pieceOfFurniture.PieceOfFurnitureId,
                Name = pieceOfFurniture.Name,
                Description = pieceOfFurniture.Description,
                RoomId = pieceOfFurniture.RoomId,
                CategoryId = pieceOfFurniture.CategoryId,
                PartsId = pieceOfFurniture.Parts.Select(x => x.PartId).ToList(),
                Size = pieceOfFurniture.Size,
                Price = pieceOfFurniture.Price,
                Count = pieceOfFurniture.Count,
                PhotoNames = pieceOfFurniture.Photos.Select(x => x.Path).ToList()
            };
        }

        public List<FurnitureResponse> GetAllFurniture()
        {
            var furniture = _context.Furniture;
            var response = new List<FurnitureResponse>();
            foreach (var pieceOfFurniture in furniture)
            {
                var add = new FurnitureResponse()
                {
                    Id = pieceOfFurniture.PieceOfFurnitureId,
                    Name = pieceOfFurniture.Name,
                    Description = pieceOfFurniture.Description,
                    RoomId = pieceOfFurniture.RoomId,
                    CategoryId = pieceOfFurniture.CategoryId,
                    PartsId = pieceOfFurniture.Parts.Select(x => x.PartId).ToList(),
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

    }
}
