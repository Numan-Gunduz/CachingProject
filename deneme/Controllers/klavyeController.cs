using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace deneme.Controllers
{
    public class klavyeController : Controller
    {
        // GET: klavyeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: klavyeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: klavyeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: klavyeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: klavyeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: klavyeController/Edit/5
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

        // GET: klavyeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: klavyeController/Delete/5
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
