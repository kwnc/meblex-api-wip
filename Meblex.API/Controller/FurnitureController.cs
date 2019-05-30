using System.Collections.Generic;
using System.Linq;
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
//    [Authorize]
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

//        [Authorize(Roles = "Worker")]
        [DisableRequestSizeLimit]
        [HttpPost("add")]
        [SwaggerOperation(
            Summary = "Adding piece of furniture",
            Description = "Can add piece of furniture",
            OperationId = "AddPieceOfFurniture")]
        [SwaggerResponse(201, "", typeof(FurnitureResponse))]
        [SwaggerResponse(500)]
        
        public async Task<IActionResult> Add([ModelBinder(BinderType = typeof(JsonModelBinder))] PieceOfFurnitureAddForm json, [IFormFilePhoto] List<IFormFile> photos)
        {
            var photosNames =await _photoService.SafePhotos(photos);
            var id = await _furnitureService.AddFurniture(photosNames, Mapper.Map(json).ToANew<PieceOfFurnitureAddDto>());
            return StatusCode(201, _furnitureService.GetPieceOfFurniture(id));
        }

        [AllowAnonymous]
        [EnableQuery]
        [HttpGet("colors")]
        [SwaggerResponse(200, "", typeof(List<ColorsResponse>))]
        [SwaggerResponse(500)]
        public IActionResult GetColors()
        {
            var response = _furnitureService.GetAll<Color, ColorsResponse>();
            return StatusCode(200,response );
        }
        [AllowAnonymous]
        [HttpGet("color/{id}")]
        [SwaggerResponse(200, "", typeof(ColorsResponse))]
        [SwaggerResponse(404)]
        [SwaggerResponse(500)]
        public IActionResult GetColor(int id)
        {
            var ID = Guard.Argument(id, nameof(id)).NotNegative();
            var response = _furnitureService.GetSingle<Color, ColorsResponse>(ID);
            return StatusCode(200, response);
        }

        [Authorize(Roles = "Worker")]
        [HttpPost("color")]
        [SwaggerResponse(200, "", typeof(ColorsResponse))]
        [SwaggerResponse(500)]
        public IActionResult AddColor([FromBody] ColorAddForm color)
        {
            var id = _furnitureService.AddOne<Color, ColorAddForm>(color, new List<string>(){nameof(ColorAddForm.Name), nameof(ColorAddForm.HexCode)});
            var response = _furnitureService.GetSingle<Color, ColorsResponse>(id);

            return StatusCode(201, response);
        }
        [AllowAnonymous]
        [EnableQuery]
        [HttpGet("materials")]
        [SwaggerResponse(200, "", typeof(List<MaterialResponse>))]
        [SwaggerResponse(500)]
        public IActionResult GetMaterial()
        {
            var responses = _furnitureService.GetAll<Material, MaterialResponse>();
            var photos = _furnitureService.GetAllMaterialPhoto();
            foreach (var photo in photos)
            {
                var response = responses.FirstOrDefault(x => x.MaterialId == photo.Key);
                response.Photo = photo.Value;
            }
            return StatusCode(200, responses);
        }
        [AllowAnonymous]
        [HttpGet("material/{id}")]
        [SwaggerResponse(200, "", typeof(MaterialResponse))]
        [SwaggerResponse(404)]
        [SwaggerResponse(500)]
        public IActionResult GetMaterial(int id)
        {
            var ID = Guard.Argument(id, nameof(id)).NotNegative();
            var response = _furnitureService.GetSingle<Material, MaterialResponse>(ID);
            var photo = _furnitureService.GetMaterialPhoto(ID);
            response.Photo = photo;
            return StatusCode(200, response);
        }
        [Authorize(Roles = "Worker")]
        [HttpPost("material")]
        [SwaggerResponse(200, "", typeof(MaterialResponse))]
        [SwaggerResponse(500)]
        public async Task<IActionResult> AddMaterial([ModelBinder(BinderType = typeof(JsonModelBinder))] MaterialAddForm json, [IFormFilePhoto] IFormFile photo)
        {
            var photoName = await _photoService.SafePhoto(photo);
            var id = _furnitureService.AddMaterial(photoName, json);
            var response = _furnitureService.GetSingle<Material, MaterialResponse>(id);
            response.Photo = photoName;

            return StatusCode(201, response);
        }
        [AllowAnonymous]
        [EnableQuery]
        [HttpGet("patterns")]
        [SwaggerResponse(200, "", typeof(List<PatternsResponse>))]
        [SwaggerResponse(500)]
        public IActionResult GetPatterns()
        {
            var responses = _furnitureService.GetAll<Pattern, PatternsResponse>();
            var photos = _furnitureService.GetAllPatternPhoto();
            foreach (var photo in photos)
            {
                var response = responses.FirstOrDefault(x => x.PatternId == photo.Key);
                response.Photo = photo.Value;
            }
            return StatusCode(200, responses);
        }
        [AllowAnonymous]
        [HttpGet("pattern/{id}")]
        [SwaggerResponse(200, "", typeof(PatternsResponse))]
        [SwaggerResponse(404)]
        [SwaggerResponse(500)]
        public IActionResult GetPattern(int id)
        {
            var ID = Guard.Argument(id, nameof(id)).NotNegative();
            var response = _furnitureService.GetSingle<Pattern, PatternsResponse>(ID);
            var photo = _furnitureService.GetPatternPhoto(ID);
            response.Photo = photo;
            return StatusCode(200, response);
        }
        [Authorize(Roles = "Worker")]
        [HttpPost("pattern")]
        [SwaggerResponse(200, "", typeof(PatternsResponse))]
        [SwaggerResponse(500)]
        public async Task<IActionResult> AddPattern([ModelBinder(BinderType = typeof(JsonModelBinder))] PatternAddForm json, [IFormFilePhoto] IFormFile photo)
        {
            var photoName = await _photoService.SafePhoto(photo);
            var id = _furnitureService.AddPattern(photoName, json);
            var response = _furnitureService.GetSingle<Material, MaterialResponse>(id);
            response.Photo = photoName;

            return StatusCode(201, response);
        }
        [AllowAnonymous]
        [EnableQuery]
        [HttpGet("rooms")]
        [SwaggerResponse(200, "", typeof(List<RoomsResponse>))]
        [SwaggerResponse(500)]
        public IActionResult GetRooms()
        {
            var response = _furnitureService.GetAll<Room, RoomsResponse>();
            return StatusCode(200, response);
        }
        [AllowAnonymous]
        [HttpGet("room/{id}")]
        [SwaggerResponse(200, "", typeof(RoomsResponse))]
        [SwaggerResponse(404)]
        [SwaggerResponse(500)]
        public IActionResult GetRoom(int id)
        {
            var ID = Guard.Argument(id, nameof(id)).NotNegative();
            var response = _furnitureService.GetSingle<Room, RoomsResponse>(ID);
            return StatusCode(200, response);
        }
        [Authorize(Roles = "Worker")]
        [HttpPost("room")]
        [SwaggerResponse(200, "", typeof(RoomsResponse))]
        [SwaggerResponse(500)]
        public IActionResult AddRoom([FromBody] RoomAddForm room)
        {
            var id = _furnitureService.AddOne<Room, RoomAddForm>(room, new List<string>() { nameof(RoomAddForm.Name) });
            var response = _furnitureService.GetSingle<Room, RoomsResponse>(id);

            return StatusCode(201, response);
        }
        [AllowAnonymous]
        [EnableQuery]
        [HttpGet("parts")]
        [SwaggerResponse(200, "", typeof(List<PartResponse>))]
        [SwaggerResponse(500)]
        public IActionResult GetParts()
        {
            var response = _furnitureService.GetAll<Part, PartResponse>();
            return StatusCode(200, response);
        }
        [AllowAnonymous]
        [HttpGet("part/{id}")]
        [SwaggerResponse(200, "", typeof(PartResponse))]
        [SwaggerResponse(404)]
        [SwaggerResponse(500)]
        public IActionResult GetPart(int id)
        {
            var ID = Guard.Argument(id, nameof(id)).NotNegative();
            var response = _furnitureService.GetSingle<Part, PartResponse>(ID);
            return StatusCode(200, response);
        }
        [Authorize(Roles = "Worker")]
        [HttpPost("part")]
        [SwaggerResponse(200, "", typeof(PartResponse))]
        [SwaggerResponse(500)]
        public IActionResult AddPart([FromBody] PartAddForm part)
        {
            var id = _furnitureService.AddPart(part);
            var response = _furnitureService.GetSingle<Part, PartResponse>(id);
            return StatusCode(201, response);

        }

        [AllowAnonymous]
        [EnableQuery]
        [HttpGet("categories")]
        [SwaggerResponse(200, "", typeof(List<CategoryResponse>))]
        [SwaggerResponse(500)]
        public IActionResult GetCategories()
        {
            var response = _furnitureService.GetAll<Category, CategoryResponse>();
            return StatusCode(200, response);
        }
        [AllowAnonymous]
        [HttpGet("category/{id}")]
        [SwaggerResponse(200, "", typeof(CategoryResponse))]
        [SwaggerResponse(404)]
        [SwaggerResponse(500)]
        public IActionResult GetCategory(int id)
        {
            var ID = Guard.Argument(id, nameof(id)).NotNegative();
            var response = _furnitureService.GetSingle<Category, CategoryResponse>(ID);
            return StatusCode(200, response);
        }
        [Authorize(Roles = "Worker")]
        [HttpPost("category")]
        [SwaggerResponse(200, "", typeof(CategoryResponse))]
        [SwaggerResponse(500)]
        public IActionResult AddCategory([FromBody] CategoryAddForm category)
        {
            var id = _furnitureService.AddOne<Category, CategoryAddForm>(category, new List<string>() { nameof(CategoryAddForm.Name) });
            var response = _furnitureService.GetSingle<Category, CategoryResponse>(id);

            return StatusCode(201, response);
        }
        [AllowAnonymous]
        [EnableQuery]
        [HttpGet("furniture")]
        [SwaggerResponse(200, "", typeof(List<FurnitureResponse>))]
        [SwaggerResponse(500)]
        public IActionResult GetFurniture()
        {
            var response = _furnitureService.GetAllFurniture();
            return StatusCode(200, response);
        }
        [AllowAnonymous]
        [HttpGet("pieceOfFurniture/{id}")]
        [SwaggerResponse(200, "", typeof(FurnitureResponse))]
        [SwaggerResponse(404)]
        [SwaggerResponse(500)]
        public IActionResult GetPieceOfFurniture(int id)
        {
            var ID = Guard.Argument(id, nameof(id)).NotNegative();
            var response = _furnitureService.GetPieceOfFurniture(ID);
            return StatusCode(200, response);
        }
    }
}