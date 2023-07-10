using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BooksApp
{
    public partial class MainWindow : Window
    {
        private BooksContext _context;

        public MainWindow()
        {
            InitializeComponent();
            _context = new BooksContext();
            LoadBooks();
        }

        private void LoadBooks()
        {
            var books = _context.Książkis.ToList();
            ListViewKsiążki.ItemsSource = books;
        }


        private void DodajKsiążkę_Click(object sender, RoutedEventArgs e)
        {
            DodajKsiążkęWindow dodajKsiążkęWindow = new DodajKsiążkęWindow(_context);
            dodajKsiążkęWindow.ShowDialog();
            LoadBooks();
        }

        private void EdytujKsiążkę_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewKsiążki.SelectedItem != null)
            {
                Książki selectedBook = (Książki)ListViewKsiążki.SelectedItem;
                EdytujKsiążkęWindow edytujKsiążkęWindow = new EdytujKsiążkęWindow(_context, selectedBook.KsiążkaId);
                edytujKsiążkęWindow.ShowDialog();
                LoadBooks();
            }
            else
            {
                MessageBox.Show("Wybierz książkę, którą chcesz edytować.");
            }
        }

        private void UsuńKsiążkę_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewKsiążki.SelectedItem != null)
            {
                Książki selectedBook = (Książki)ListViewKsiążki.SelectedItem;

                // Usuń powiązane rekordy z tabel Kategorie, Autorzy i Wydawnictwa
                if (selectedBook.KategoriaId != null)
                {
                    var kategoria = _context.Kategories.Find(selectedBook.KategoriaId);
                    if (kategoria != null)
                        _context.Kategories.Remove(kategoria);
                }

                if (selectedBook.AutorId != null)
                {
                    var autor = _context.Autorzies.Find(selectedBook.AutorId);
                    if (autor != null)
                        _context.Autorzies.Remove(autor);
                }

                if (selectedBook.WydawnictwoId != null)
                {
                    var wydawnictwo = _context.Wydawnictwas.Find(selectedBook.WydawnictwoId);
                    if (wydawnictwo != null)
                        _context.Wydawnictwas.Remove(wydawnictwo);
                }

                _context.Książkis.Remove(selectedBook);
                _context.SaveChanges();
                LoadBooks();
            }
            else
            {
                MessageBox.Show("Wybierz książkę, którą chcesz usunąć.");
            }
        }

    }
}

