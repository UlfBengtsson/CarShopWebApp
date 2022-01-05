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
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)// constructor Dipenincy Injection
        {
            _brandService = brandService;
        }

        public ActionResult Index()
        {
            return View(_brandService.GetAll());
        }

        public ActionResult Details(int id)
        {
            Brand brand = _brandService.FindById(id);

            if (brand == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(brand);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateBrandViewModel createBrandViewModel)
        {
            if (ModelState.IsValid)
            {
                _brandService.Create(createBrandViewModel);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(createBrandViewModel);
            }
        }

        public ActionResult Edit(int id)
        {
            Brand brand = _brandService.FindById(id);

            if (brand == null)
            {
                return RedirectToAction(nameof(Index));
            }

            CreateBrandViewModel createBrandViewModel = new CreateBrandViewModel() { Name = brand.Name };
            ViewBag.BrandId = brand.Id;

            return View(createBrandViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CreateBrandViewModel brandViewModel)
        {
            if (ModelState.IsValid)
            {
                if (_brandService.Edit(id, brandViewModel))
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("Storage", "Unable to save changes");
            }

            ViewBag.BrandId = id;
            return View(brandViewModel);
        }

        public ActionResult Delete(int id)
        {
            _brandService.Remove(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
