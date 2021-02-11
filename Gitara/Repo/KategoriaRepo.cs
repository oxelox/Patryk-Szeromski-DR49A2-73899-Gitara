using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gitara.DB;
using Gitara.Models;

namespace Gitara.Repo
{
    public class KategoriaRepo
    {
        public String PobierzKategorie(int id)
        {
            using (var db = new Entities())
            {
                return db.Kategoria.FirstOrDefault(x => x.KategoriaId == id)?.Nazwa ?? "";
            }
        }
    }
}