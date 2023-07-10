using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksApp
{
    public partial class Książki
    {
        public int KsiążkaId { get; set; }

        public string? Tytuł { get; set; }

        public int? AutorId { get; set; }

        [ForeignKey("AutorId")]
        public virtual Autorzy? Autor { get; set; }

        public int? WydawnictwoId { get; set; }

        [ForeignKey("WydawnictwoId")]
        public virtual Wydawnictwa? Wydawnictwo { get; set; }

        public int? KategoriaId { get; set; }

        [ForeignKey("KategoriaId")]
        public virtual Kategorie? Kategoria { get; set; }
    }
}
