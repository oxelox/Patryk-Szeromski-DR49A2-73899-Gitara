using System.Web.Mvc;
using System.Web.Security;
using Gitara.Models;
using Gitara.Repo;

namespace Gitara.Controllers
{
    public class AccountController : Controller
    {
        private AccountRepo accountRepo;

        public AccountController()
        {
            accountRepo = new AccountRepo();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (accountRepo.SprawdzLogin(model.Login))
                {
                    ModelState.AddModelError("", "Wybrany login jest już zajęty");
                    return View(model);
                }
                else
                {
                    accountRepo.UtworzKonto(model.Login, model.Haslo, model.Imie, model.Nazwisko);
                    return RedirectToAction("RegisterComplete");
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult RegisterComplete()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!accountRepo.SprawdzLogin(model.Login))
                {
                    ModelState.AddModelError("", "Nie znaleziuono użytkownika o podanym loginie");
                    return View(model);
                }
                else
                {
                    if (accountRepo.Zaloguj(model.Login, model.Haslo))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Nieprawidłowe hasło");
                        return View(model);
                    }
                }
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult LogOff()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}