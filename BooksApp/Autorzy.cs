﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksApp
{
    public partial class Autorzy
    {
        public int AutorId { get; set; }

        public string Imię { get; set; } = null!;

        public string Nazwisko { get; set; } = null!;

        public virtual ICollection<Książki> Książkis { get; set; } = new List<Książki>();
    }
}
