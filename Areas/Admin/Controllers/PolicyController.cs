using System.IO;
using aspcore_watchshop.EF;
using aspcore_watchshop.Hepler;
using aspcore_watchshop.Interfaces;
using aspcore_watchshop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aspcore_watchshop.Areas.Admin.Controllers {
    [Area ("Admin")]
    [Authorize]
    public class PolicyController : Controller {
        private watchContext _context = null;
        private IPolicyModel _policyModel = null;

        public PolicyController (watchContext context, IPolicyModel policyModel) {
            _context = context;
            _policyModel = policyModel;
        }

        [HttpGet]
        public JsonResult ListData () {
            return Json (_policyModel.GetListVMs (_context));
        }

        [HttpPost]
        public IActionResult Add (string item) {
            if (item == "" || item == null) return BadRequest ();
            PolicyVM policy = DataHelper.Instance.ParserJsonTo<PolicyVM> (item);
            if (!_policyModel.AddModel (_context, policy)) return null;
            //Modified Data
            CacheHelper.DataUpdated (Changed.POLICY);
            return Ok ();
        }

        [HttpPut]
        public IActionResult Update (int id, string item) {
            if (id <= 0) return NotFound ();
            if (item == "" || item == null) return BadRequest ();
            PolicyVM policy = DataHelper.Instance.ParserJsonTo<PolicyVM> (item);
            if (!_policyModel.UpdateModel (_context, id, policy)) return null;
            //Modified Data
            CacheHelper.DataUpdated (Changed.POLICY);
            return Ok ();
        }

        [HttpPut]
        public IActionResult Remove (int id) {
            if (id <= 0) return NotFound ();
            if (!_policyModel.RemoveModel (_context, id)) return null;
            //Modified Data
            CacheHelper.DataUpdated (Changed.POLICY);
            return Ok ();
        }

        [HttpPost]
        public IActionResult Upload (IFormFileCollection imgs) {
            if (imgs == null) return BadRequest ();
            foreach (var img in imgs) {
                string path = Path.Combine (Directory.GetCurrentDirectory (), @"wwwroot\icon", img.FileName);
                img.CopyToAsync (new FileStream (path, FileMode.OpenOrCreate));
            }
            return Ok ();
        }
    }
}