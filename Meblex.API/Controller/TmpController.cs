using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meblex.API.Context;
using Meblex.API.FormsDto.Request;
using Meblex.API.Interfaces;
using Meblex.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Meblex.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TmpController : ControllerBase
    {
        private readonly IPhotoService _photoService;
        private readonly MeblexDbContext _context;
        public TmpController(IPhotoService photoService, MeblexDbContext context)
        {
            _photoService = photoService;
            _context = context;

        }
        [HttpPost("add")]
        [SwaggerResponse(200)]
        [SwaggerResponse(500)]
        public async Task<IActionResult> AddPairs([FromBody] TmpAddForm form)
        {
            var pieceOfFurniture = _context.Furniture.Find(form.PieceOfFurnitureId);
            if (pieceOfFurniture == null)
            {
                return StatusCode(404, "Piece of furniture does not exist");
            }

            var photosNames = await _photoService.SafePhotos(form.Photos);
            
            photosNames.ForEach(x => _context.Photos.Add(new Photo(){PieceOfFurniture = pieceOfFurniture, PieceOfFurnitureId = form.PieceOfFurnitureId, Path = x}));
            if (_context.SaveChanges() == 0)
            {
                return StatusCode(500, "Unable to add data");
            }
            return Ok();
        }
    }
}