using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin+","+ SD.Role_Employee)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
            public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Company company = new Company();
            if(id==null)
            {
                //This is for create 
                return View(company);
            }
            //this is for edit
            company = _unitOfWork.Company.Get(id.GetValueOrDefault());
            if(company==null)
            {
                return NotFound();
            }

            return View(company);

            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(Company company)
        {
            if(ModelState.IsValid)
            {
                if(company.Id==0)
                {
                    _unitOfWork.Company.Add(company);
                    _unitOfWork.save();
                }
                else
                {
                    _unitOfWork.Company.Update(company);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        #region API CALLS

        [HttpGet]

        public IActionResult GetAll()
        {
            var allobj = _unitOfWork.Company.GetAll();
            return Json(new { data = allobj });
        }

        [HttpDelete]

        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Company.Get(id);  
            if(objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _unitOfWork.Company.Remove(objFromDb);
            _unitOfWork.save();
            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion



    }
}