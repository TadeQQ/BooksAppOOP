using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksApp
{
    public partial class Książki
    {
        public int KsiążkaId { get; set; }

        public int? AutorId { get; set; }

        public int? WydawnictwoId { get; set; }

        public int? KategoriaId { get; set; }

        public string? Tytuł { get; set; }


        public virtual Autorzy Autor { get; set; } = null!;
        public virtual Kategorie Kategoria { get; set; } = null!;
        public virtual Wydawnictwa Wydawnictwo { get; set; } = null!;
    }
}
