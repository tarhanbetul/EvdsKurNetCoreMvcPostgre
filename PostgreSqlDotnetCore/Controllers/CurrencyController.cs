using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PostgreSqlDotnetCore.Models.Evds;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PostgreSqlDotnetCore.Controllers
{
    public class CurrencyController : Controller
    {
        EvdsEntities db = new EvdsEntities();
        // GET: CurrencyController
        public ActionResult Index()
        {
          

            var model = db.ParaBirimi.ToList();
            return View(model);
        }

        // GET: CurrencyController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CurrencyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CurrencyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ParaBirimi collection)
        {
            try
            {
                db.ParaBirimi.Add(collection);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CurrencyController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CurrencyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CurrencyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CurrencyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

      
    }
}
