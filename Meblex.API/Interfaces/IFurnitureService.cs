using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Meblex.API.DTO;
using Meblex.API.FormsDto.Request;
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
        int AddPart(PartAddForm part);

        int AddOne<TEntity, TDto>(TDto toAdd, List<string> duplicates)
            where TEntity : class where TDto : class;
    }
}
