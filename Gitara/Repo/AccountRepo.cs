using System.Linq;
using System.Web;
using System.Web.Security;
using Gitara.DB;
using Gitara.Helpers;

namespace Gitara.Repo
{
    public class AccountRepo
    {
        public bool SprawdzLogin(string login)
        {
            using (var db = new Entities())
            {
                return db.Klient.Any(x => x.Login == login);
            }
        }

        public void UtworzKonto(string login, string haslo, string imie, string nazwisko)
        {
            using (var db = new Entities())
            {
                var konto = new Klient
                {
                    Login = login,
                    Nazwisko = nazwisko,
                    Imie = imie,
                    Haslo = PasswordHasher.Hash(haslo)
                };

                db.Klient.Add(konto);
                db.SaveChanges();
            }
        }

        public bool Zaloguj(string login, string haslo)
        {
            using (var db = new Entities())
            {
                var konto = db.Klient.First(x => x.Login == login);

                if (PasswordHasher.DoesPasswordMatchHash(haslo, konto.Haslo))
                {
                    FormsAuthentication.SetAuthCookie(konto.KlientId.ToString(), false);
                    HttpContext.Current.Session["Klient"] = $"{konto.Imie} {konto.Nazwisko}";
                    return true;
                }

                return false;
            }
        }

        public string AktualizujNazweWSesji(int id)
        {
            using (var db = new Entities())
            {
                var konto = db.Klient.Find(id);

                var nazwa = $"{konto.Imie} {konto.Nazwisko}";
                HttpContext.Current.Session["Klient"] = nazwa;
                return nazwa;
            }
        }
    }
}