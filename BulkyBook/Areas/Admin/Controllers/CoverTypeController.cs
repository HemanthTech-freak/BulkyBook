using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
            public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            CoverType coverType = new CoverType();
            if(id==null)
            {
                //This is for create 
                return View(coverType);
            }
            //this is for edit
            coverType = _unitOfWork.CoverType.Get(id.GetValueOrDefault());
            if(coverType==null)
            {
                return NotFound();
            }

            return View(coverType);

            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(CoverType coverType)
        {
            if(ModelState.IsValid)
            {
                if(coverType.ID==0)
                {
                    _unitOfWork.CoverType.Add(coverType);
                    _unitOfWork.save();
                }
                else
                {
                    _unitOfWork.CoverType.Update(coverType);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(coverType);
        }

        #region API CALLS

        [HttpGet]

        public IActionResult GetAll()
        {
            var allobj = _unitOfWork.CoverType.GetAll();
            return Json(new { data = allobj });
        }

        [HttpDelete]

        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.CoverType.Get(id);  
            if(objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _unitOfWork.CoverType.Remove(objFromDb);
            _unitOfWork.save();
            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion



    }
}