using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meblex.API.DTO;
using Meblex.API.FormsDto.Response;

namespace Meblex.API.Interfaces
{
    public interface IFurnitureService
    {
        Task<int> AddFurniture(List<string> photos, PieceOfFurnitureAddDto pieceOfFurniture);
        List<FurnitureResponse> GetAllFurniture();
        TResponse GetSingle<TEntity, TResponse>(int id) where TEntity : class where TResponse : class;
        List<TResponse> GetAll<TEntity, TResponse>() where TEntity : class where TResponse : class;
        FurnitureResponse GetPieceOfFurniture(int id);
    }
}
