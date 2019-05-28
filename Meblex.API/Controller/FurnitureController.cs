using System.Collections.Generic;
using System.Threading.Tasks;
using AgileObjects.AgileMapper;
using Dawn;
using Meblex.API.DTO;
using Meblex.API.FormsDto.Request;
using Meblex.API.FormsDto.Response;
using Meblex.API.Helper;
using Meblex.API.Interfaces;
using Meblex.API.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace Meblex.API.Controller
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class FurnitureController : ControllerBase
    {
        private readonly IPhotoService _photoService;
        private readonly IFurnitureService _furnitureService;
        public FurnitureController(IPhotoService photoService, IFurnitureService furnitureService)
        {
            _photoService = photoService;
            _furnitureService = furnitureService;
        }

        [HttpPost("add")]
        [SwaggerOperation(
            Summary = "Adding piece of furniture",
            Description = "Can add piece of furniture",
            OperationId = "AddPieceOfFurniture")]
        [SwaggerResponse(201, "", typeof(FurnitureResponse))]
        [SwaggerResponse(500)]
        public async Task<IActionResult> Add([ModelBinder(BinderType = typeof(JsonModelBinder))] PieceOfFurnitureAddForm json, [IFormFilePhoto] List<IFormFile> photos)
        {
            var photosNames =_photoService.SafePhotos(photos);
            var id = await _furnitureService.AddFurniture(photosNames, Mapper.Map(json).ToANew<PieceOfFurnitureAddDto>());
            return StatusCode(201, _furnitureService.GetPieceOfFurniture(id));
        }

        [AllowAnonymous]
        [EnableQuery]
        [HttpGet("colors")]
        public IActionResult GetColors()
        {
            var response = _furnitureService.GetAll<Color, ColorsResponse>();
            return StatusCode(200,response );
        }
        [AllowAnonymous]
        [HttpGet("color/{id}")]
        public IActionResult GetColor(int id)
        {
            var ID = Guard.Argument(id, nameof(id)).NotNegative();
            var response = _furnitureService.GetSingle<Color, ColorsResponse>(ID);
            return StatusCode(200, response);
        }
        [AllowAnonymous]
        [EnableQuery]
        [HttpGet("materials")]
        public IActionResult GetMaterial()
        {
            var response = _furnitureService.GetAll<Material, MaterialResponse>();
            return StatusCode(200, response);
        }
        [AllowAnonymous]
        [HttpGet("material/{id}")]
        public IActionResult GetMaterial(int id)
        {
            var ID = Guard.Argument(id, nameof(id)).NotNegative();
            var response = _furnitureService.GetSingle<Material, MaterialResponse>(ID);
            return StatusCode(200, response);
        }
        [AllowAnonymous]
        [EnableQuery]
        [HttpGet("patterns")]
        public IActionResult GetPatterns()
        {
            var response = _furnitureService.GetAll<Pattern, PatternsResponse>();
            return StatusCode(200, response);
        }
        [AllowAnonymous]
        [HttpGet("pattern/{id}")]
        public IActionResult GetPattern(int id)
        {
            var ID = Guard.Argument(id, nameof(id)).NotNegative();
            var response = _furnitureService.GetSingle<Pattern, PatternsResponse>(ID);
            return StatusCode(200, response);
        }
        [AllowAnonymous]
        [EnableQuery]
        [HttpGet("rooms")]
        public IActionResult GetRooms()
        {
            var response = _furnitureService.GetAll<Room, RoomsResponse>();
            return StatusCode(200, response);
        }
        [AllowAnonymous]
        [HttpGet("room/{id}")]
        public IActionResult GetRoom(int id)
        {
            var ID = Guard.Argument(id, nameof(id)).NotNegative();
            var response = _furnitureService.GetSingle<Room, RoomsResponse>(ID);
            return StatusCode(200, response);
        }
        [AllowAnonymous]
        [EnableQuery]
        [HttpGet("parts")]
        public IActionResult GetParts()
        {
            var response = _furnitureService.GetAll<Part, PartResponse>();
            return StatusCode(200, response);
        }
        [AllowAnonymous]
        [HttpGet("part/{id}")]
        public IActionResult GetPart(int id)
        {
            var ID = Guard.Argument(id, nameof(id)).NotNegative();
            var response = _furnitureService.GetSingle<Part, PartResponse>(ID);
            return StatusCode(200, response);
        }
        [AllowAnonymous]
        [EnableQuery]
        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            var response = _furnitureService.GetAll<Category, CategoryResponse>();
            return StatusCode(200, response);
        }
        [AllowAnonymous]
        [HttpGet("category/{id}")]
        public IActionResult GetCategory(int id)
        {
            var ID = Guard.Argument(id, nameof(id)).NotNegative();
            var response = _furnitureService.GetSingle<Category, CategoryResponse>(ID);
            return StatusCode(200, response);
        }
        [AllowAnonymous]
        [EnableQuery]
        [HttpGet("furniture")]
        public IActionResult GetFurniture()
        {
            var response = _furnitureService.GetAllFurniture();
            return StatusCode(200, response);
        }
        [AllowAnonymous]
        [HttpGet("pieceOfFurniture/{id}")]
        public IActionResult GetPieceOfFurniture(int id)
        {
            var ID = Guard.Argument(id, nameof(id)).NotNegative();
            var response = _furnitureService.GetPieceOfFurniture(ID);
            return StatusCode(200, response);
        }
    }
}