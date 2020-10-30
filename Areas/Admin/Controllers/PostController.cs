using aspcore_watchshop.EF;
using aspcore_watchshop.Hepler;
using aspcore_watchshop.Interfaces;
using aspcore_watchshop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aspcore_watchshop.Areas.Admin.Controllers {
    [Area ("Admin")]
    [Authorize]
    public class PostController : Controller {
        private watchContext _context = null;
        private IPostModel _postModel = null;

        public PostController (watchContext context, IPostModel postModel) {
            _context = context;
            _postModel = postModel;
        }

        [HttpGet]
        public IActionResult Data (int id) {
            if (id <= 0) return NotFound ();
            return Json (_postModel.GetVM (_context, id));
        }

        [HttpPost]
        public IActionResult Add (PostVM post) {
            if (!ModelState.IsValid) return BadRequest ();
            if (post.Id != 0) return Conflict ();
            if (!_postModel.AddModel (_context, post)) return null;
            return Ok ();
        }

        [HttpPut]
        public IActionResult Update (PostVM post) {
            if (post.Id <= 0) return NotFound ();
            if (!_postModel.UpdateModel (_context, post.Id, post)) return null;
            return Ok ();
        }

        [HttpPut]
        public IActionResult Remove (int id) {
            if (id <= 0) return NotFound ();
            if (!_postModel.RemoveModel (_context, id)) return null;
            return Ok ();
        }
    }
}