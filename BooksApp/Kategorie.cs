using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksApp
{
    public partial class Kategorie
    {
        public int KategoriaId { get; set; }

        public string? Nazwa { get; set; }

        public virtual ICollection<Książki> Książkis { get; set; } = new List<Książki>();
    }
}
