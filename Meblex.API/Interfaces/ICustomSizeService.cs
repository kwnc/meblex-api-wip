using System.Collections.Generic;
using Meblex.API.FormsDto.Request;
using Meblex.API.FormsDto.Response;

namespace Meblex.API.Interfaces
{
    public interface ICustomSizeService
    {
        CustomSizeFormResponse ApproveCustomSizeForm(int id, float price);
        CustomSizeFormResponse GetClientFormById(int id, int userId);
        List<CustomSizeFormResponse> GetAllClientForms(int userId);
        CustomSizeFormResponse GetById(int id);
        int AddCustomSize(CustomSizeAddFrom form, int userId);
        List<CustomSizeFormResponse> GetAllCustomSizeForm();
    }
}