using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gitara.DB;
using Gitara.Models;

namespace Gitara.Repo
{
    public class TowarRepo
    {
        public IEnumerable<SelectListItem> PobierzMarki()
        {
            using (var db = new Entities())
            {
                return db.Towar.Select(x => new SelectListItem {Value = x.Producent, Text = x.Producent}).Distinct()
                    .ToList();
            }
        }

        public int CenaMin()
        {
            using (var db = new Entities())
            {
                return (int) Math.Floor(db.Towar.Select(x => x.Cena).Min());
            }
        }

        public int CenaMax()
        {
            using (var db = new Entities())
            {
                return (int) Math.Ceiling(db.Towar.Select(x => x.Cena).Max());
            }
        }

        public List<Towar> Szukaj(int kategoriaId, string producent, int cenaMin, int cenaMax)
        {
            using (var db = new Entities())
            {
                return (from m in db.Towar.Where(x =>
                        x.KategoriaId == kategoriaId &&
                        (producent == "0" || x.Producent == producent) && x.Cena >= cenaMin &&
                        x.Cena <= cenaMax)
                    from t in db.Transakcje.Where(x => x.TowarId == m.TowarId).DefaultIfEmpty()
                    select m).OrderBy(x => x.Cena).ToList();
            }
        }

        public Towar PobierzPoId(int id)
        {
            using (var db = new Entities())
            {
                return db.Towar.Find(id);
            }
        }

        public int Zamow(int towarId, int klientId)
        {
            using (var db = new Entities())
            {
                if (db.Transakcje.Any(x => x.TowarId == towarId))
                    return -1;

                var zamowienie = new Transakcje
                {
                    TowarId = towarId,
                    KlientId = klientId,
                    Data = DateTime.Now
                };

                db.Transakcje.Add(zamowienie);
                db.SaveChanges();

                return zamowienie.TransakcjaId;
            }
        }

        public List<ZamowienieHistoriaViewModel.Historia> Historia(int klientId)
        {
            using (var db = new Entities())
            {
                return (from tow in db.Towar
                    from t in db.Transakcje.Where(x => x.TowarId == tow.TowarId && x.KlientId == klientId)
                    select new ZamowienieHistoriaViewModel.Historia
                    {
                        Towar = tow,
                        Transakcja = t
                    }).OrderByDescending(x => x.Transakcja.Data).ToList();
            }
        }
    }
}