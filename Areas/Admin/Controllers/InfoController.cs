using System;
using System.Collections.Generic;
using System.IO;
using aspcore_watchshop.EF;
using aspcore_watchshop.Hepler;
using aspcore_watchshop.Interfaces;
using aspcore_watchshop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aspcore_watchshop.Areas.Admin.Controllers {
    [Area ("Admin")]
    [Authorize]
    public class InfoController : Controller {
        private watchContext _context = null;
        private IInfoModel _infoModel = null;

        public InfoController (watchContext context, IInfoModel info) {
            _context = context;
            _infoModel = info;
        }

        [HttpPut]
        public IActionResult Update (string item) {
            if (item == "" || item == null) return BadRequest ();
            Info info = DataHelper.Instance.ParserJsonTo<Info> (item);
            if (!_infoModel.UpdateModel (_context, 0, info)) return null;
            //Modified Data
            CacheHelper.DataUpdated (Changed.INFO);
            return Ok ();
        }

        [HttpGet]
        public IActionResult Data () {
            return Json (_infoModel.GetVM (_context, 0));
        }

        [HttpPost]
        public IActionResult Upload (IFormFileCollection imgs) {
            if (imgs == null) return BadRequest ();
            foreach (var img in imgs) {
                string path = Path.Combine (Directory.GetCurrentDirectory (), @"wwwroot\images", img.FileName);
                img.CopyToAsync (new FileStream (path, FileMode.OpenOrCreate));
            }
            return Ok ();
        }

    }
}