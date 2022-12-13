using Microsoft.AspNetCore.Mvc;
using MultimediaAPI.Models;
using MultimediaAPI.Services;

namespace MultimediaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : Controller
    {
        private readonly IAlbumService _albumService;
        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }
        [HttpGet]
        public JsonResult Index()
        {
            return new JsonResult(_albumService.GetAllAlbum());
        }
        [HttpGet]
        [Route("get/{albumId}")]
        public JsonResult GetAlbum(int albumId)
        {
            return new JsonResult(_albumService.GetAlbum(albumId));
        }
        [HttpPost]
        [Route("create")]
        public JsonResult Create(Album album)
        {
            return new JsonResult(_albumService.CreateAlbum(album));
        }
        [HttpPut]
        [Route("update")]
        public JsonResult Update(Album album)
        {
            return new JsonResult(_albumService.UpdateAlbum(album));
        }
        [HttpDelete]
        [Route("delete/{albumId}")]
        public JsonResult Delete(int albumId)
        {
            return new JsonResult(_albumService.DeleteAlbum(albumId));
        }
    }
}
