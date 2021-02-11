using System.Collections.Generic;
using Gitara.DB;

namespace Gitara.Models
{
    public class ZamowienieHistoriaViewModel
    {
        public List<Historia> Zamowienia { get; set; }

        public class Historia
        {
            public Towar Towar { get; set; }
            public Transakcje Transakcja { get; set; }
        }
    }
}