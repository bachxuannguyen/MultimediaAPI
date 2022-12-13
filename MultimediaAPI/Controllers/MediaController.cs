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
        [HttpGet]
        [Route("{action}/{mediaId}")]
        [Authorize]
        public JsonResult GetMediaFileInfo(int mediaId)
        {
            var currentUser = HttpContext.User;
            string isRoot = "0";
            if (currentUser.HasClaim(c => c.Type == "isroot"))
                isRoot = currentUser.Claims.FirstOrDefault(c => c.Type == "isroot").Value.ToLower();
            if (isRoot != "1")
                return new JsonResult("login as root user to access this content");
            return new JsonResult(_mediaService.GetFileInfo(mediaId));
        }
    }
}