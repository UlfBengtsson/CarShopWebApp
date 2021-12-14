using CarShopApp.Models;
using CarShopApp.Models.Services;
using CarShopApp.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Controllers
{
    public class InsurancesController : Controller
    {
        private readonly IInsuranceService _insuranceService;

        public InsurancesController(IInsuranceService insuranceService)
        {
            _insuranceService = insuranceService;
        }

        // GET: InsurancesController
        public ActionResult Index(string message = null)//default set message to null
        {
            if (message != null)
            {
                ViewBag.Msg = message;
            }

            return View(_insuranceService.GetAll());
        }

        // GET: InsurancesController/Details/5
        public ActionResult Details(int id)
        {
            Insurance insurance = _insuranceService.FindById(id);

            if (insurance == null)
            {
                return RedirectToAction("Index");
            }

            return View(insurance);
        }

        // GET: InsurancesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InsurancesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateInsuranceViewModel createInsurance)
        {
            if (ModelState.IsValid)
            {
                Insurance insurance = _insuranceService.Create(createInsurance);

                if (insurance != null)
                {
                    return RedirectToAction(nameof(Index), new { message = "Insurance has bin created." });
                }
                ModelState.AddModelError("Storage", "Failed to save into storage.");
            }

            return View(createInsurance);
        }

        // GET: InsurancesController/Edit/5
        public ActionResult Edit(int id)
        {
            Insurance insurance = _insuranceService.FindById(id);

            if (insurance == null)
            {
                return RedirectToAction("Index", new { message = "Unable to find, refreshed list." });
            }

            CreateInsuranceViewModel editInsurance = new CreateInsuranceViewModel()
            {
                Name = insurance.Name,
                Covers = insurance.Covers
            };

            ViewBag.Id = id;

            return View(editInsurance);
        }

        // POST: InsurancesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CreateInsuranceViewModel editInsurance)
        {
            if (ModelState.IsValid)
            {

                if (_insuranceService.Edit(id, editInsurance))
                {
                    return RedirectToAction(nameof(Index), new { message = "Changes saved." });
                }
                ModelState.AddModelError("Storage", "Failed to save changes on storage.");
            }

            ViewBag.Id = id;

            return View(editInsurance);
        }

        // GET: InsurancesController/Delete/5
        public ActionResult Delete(int id)
        {
            if (_insuranceService.Remove(id))
            {
                return RedirectToAction("Index", new { message = "Delete done." });
            }
            else
            {
                return RedirectToAction("Index", new { message = "Delete failed." });
            }
        }

    }
}
