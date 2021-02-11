using System.Linq;
using System.Web.Mvc;
using Gitara.DB;
using Gitara.Helpers;
using Gitara.Models;
using Gitara.Repo;

namespace Gitara.Controllers
{
    [Authorize]
    public class ProduktyController : Controller
    {
        private TowarRepo towarRepo;
        private KategoriaRepo kategoriaRepo;

        public ProduktyController()
        {
            towarRepo = new TowarRepo();
            kategoriaRepo = new KategoriaRepo();
        }

        [AllowAnonymous]
        public ActionResult Index(int kategoriaId)
        {
            ViewBag.KategoriaId = kategoriaId;
            ViewBag.Kategoria = kategoriaRepo.PobierzKategorie(kategoriaId);
            var model = new TowarViewModel
            {
                Producenci = towarRepo.PobierzMarki().ToList(),
                Min = towarRepo.CenaMin(),
                Max = towarRepo.CenaMax(),
                Producent = "0"
            };

            model.Producenci.Insert(0, new SelectListItem
            {
                Value = "0",
                Text = "Dowolny"
            });

            model.MinMax = $"{model.Min},{model.Max}";

            model.Towary = towarRepo.Szukaj(kategoriaId, model.Producent, model.Min, model.Max);

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Szukaj(int kategoria, string producent, string minMax)
        {
            var mm = minMax.Split(',');
            return PartialView("Lista", towarRepo.Szukaj(kategoria, producent, int.Parse(mm[0]), int.Parse(mm[1])));
        }

        [HttpGet]
        public ActionResult Zamow(int id)
        {
            return View(towarRepo.PobierzPoId(id));
        }

        [HttpPost]
        public ActionResult Zamow(Towar model)
        {
            var oid = towarRepo.Zamow(model.TowarId, User.Identity.ID());
            if (oid > 0)
            {
                return RedirectToAction("Potwierdzenie", "Produkty", new {id = oid});
            }
            else
            {
                model = towarRepo.PobierzPoId(model.TowarId);
                ModelState.AddModelError("", "Nie udało się zamówić wybranego instrumentu.");
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Potwierdzenie(int id)
        {
            return View((object)id);
        }

        public ActionResult Historia()
        {
            var model = new ZamowienieHistoriaViewModel()
            {
                Zamowienia = towarRepo.Historia(User.Identity.ID())
            };

            return View(model);
        }
    }
}