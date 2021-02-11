using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using Gitara.DB;

namespace Gitara.Models
{
    public class TowarViewModel
    {
        public string Producent { get; set; }
        public List<SelectListItem> Producenci { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        [DisplayName("Cena")]
        public string MinMax { get; set; }

        public List<Towar> Towary { get; set; }
    }
}