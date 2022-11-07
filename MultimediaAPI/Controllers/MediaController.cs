using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultimediaAPI.Services;
using MultimediaAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace MultimediaAPI.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class MediaController : Controller
    {
        private readonly IMediaService _mediaService;
        public MediaController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }
        [HttpGet]
        public JsonResult Index()
        {
            return new JsonResult(_mediaService.GetAllMedia());
        }
        [HttpGet]
        [Route("get/{mediaId}")]
        public JsonResult GetMedia(int mediaId)
        {
            return new JsonResult(_mediaService.GetMedia(mediaId));
        }
        [HttpGet]
        [Route("{action}/{mediaId}")]
        [Authorize]
        public JsonResult GetMediaFileInfo(int mediaId)
        {
            var currentUser = HttpContext.User;
            string isAdmin = "false";
            if (currentUser.HasClaim(c => c.Type == "isadmin"))
                isAdmin = currentUser.Claims.FirstOrDefault(c => c.Type == "isadmin").Value.ToLower();
            if (isAdmin != "true")
                return new JsonResult("forbidden: login as admin to access this content");
            return new JsonResult(_mediaService.GetFileInfo(mediaId));
        }
        [HttpPost]
        [Route("create")]
        public JsonResult CreateMedia(Media media)
        {
            return new JsonResult(_mediaService.CreateMedia(media));
        }
        [HttpDelete]
        [Route("delete/{mediaId}")]
        public JsonResult DeleteMedia(int mediaId)
        {
            return new JsonResult(_mediaService.DeleteMedia(mediaId));
        }
        [HttpPut]
        [Route("update")]
        public JsonResult UpdateMedia(Media media)
        {
            return new JsonResult(_mediaService.UpdateMedia(media));
        }
    }
}
