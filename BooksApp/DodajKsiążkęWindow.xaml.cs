using System.Linq;
using System.Windows;

namespace BooksApp
{
    public partial class DodajKsiążkęWindow : Window
    {
        private readonly BooksContext _context;

        public DodajKsiążkęWindow(BooksContext context)
        {
            InitializeComponent();
            _context = context;
        }

        private void DodajButton_Click(object sender, RoutedEventArgs e)
        {
            // Pobierz dane z TextBoxów
            int id = int.Parse(idTextBox.Text);
            string tytuł = tytułTextBox.Text;
            string imięAutora = imięAutoraTextBox.Text;
            string nazwiskoAutora = nazwiskoAutoraTextBox.Text;
            string wydawnictwo = wydawnictwoTextBox.Text;
            string kategoria = kategoriaTextBox.Text;

            // Sprawdź, czy istnieje książka o podanym ID
            if (_context.Książkis.Any(k => k.KsiążkaId == id))
            {
                MessageBox.Show("Książka o podanym ID już istnieje. Podaj inne ID.");
                return;
            }

            // Sprawdź, czy istnieje autor o podanym imieniu i nazwisku
            Autorzy autor = _context.Autorzies.FirstOrDefault(a => a.Imię == imięAutora && a.Nazwisko == nazwiskoAutora);
            if (autor == null)
            {
                // Set the AutorId property to a unique value that doesn't already exist in the Autorzy table
                int newAutorId = _context.Autorzies.Max(a => a.AutorId) + 1;
                autor = new Autorzy { AutorId = newAutorId, Imię = imięAutora, Nazwisko = nazwiskoAutora };
                _context.Autorzies.Add(autor);
            }

            // Znajdź lub utwórz wydawnictwo na podstawie nazwy
            Wydawnictwa wydawnictwoEntity = _context.Wydawnictwas.FirstOrDefault(w => w.Nazwa == wydawnictwo);
            if (wydawnictwoEntity == null)
            {
                // Set the WydawnictwoId property to a unique value that doesn't already exist in the Wydawnictwa table
                int newWydawnictwoId = _context.Wydawnictwas.Max(w => w.WydawnictwoId) + 1;
                wydawnictwoEntity = new Wydawnictwa { WydawnictwoId = newWydawnictwoId, Nazwa = wydawnictwo };
                _context.Wydawnictwas.Add(wydawnictwoEntity);
            }

            // Znajdź lub utwórz kategorię na podstawie nazwy
            Kategorie kategoriaEntity = _context.Kategories.FirstOrDefault(k => k.Nazwa == kategoria);
            if (kategoriaEntity == null)
            {
                // Set the KategoriaId property to a unique value that doesn't already exist in the Kategorie table
                int newKategoriaId = _context.Kategories.Max(k => k.KategoriaId) + 1;
                kategoriaEntity = new Kategorie { KategoriaId = newKategoriaId, Nazwa = kategoria };
                _context.Kategories.Add(kategoriaEntity);
            }

            // Utwórz nową książkę
            Książki nowaKsiążka = new Książki
            {
                KsiążkaId = id,
                Tytuł = tytuł,
                AutorId = autor.AutorId,
                WydawnictwoId = wydawnictwoEntity.WydawnictwoId,
                KategoriaId = kategoriaEntity.KategoriaId
            };

            // Dodaj książkę do kontekstu bazy danych
            _context.Książkis.Add(nowaKsiążka);
            _context.SaveChanges();

            MessageBox.Show("Książka została dodana do bazy danych.");

            // Zamknij okno
            Close();
        }



    }
}
