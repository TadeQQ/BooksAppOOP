﻿using System.Linq;
using System.Windows;

namespace BooksApp
{
    public partial class EdytujKsiążkęWindow : Window
    {
        private readonly BooksContext _context;
        private readonly int _ksiazkaId;

        public EdytujKsiążkęWindow(BooksContext context, int ksiazkaId)
        {
            InitializeComponent();
            _context = context;
            _ksiazkaId = ksiazkaId;

            // Wczytaj dane książki do formularza
            WczytajDaneKsiazki();
        }

        private void WczytajDaneKsiazki()
        {
            // Wyszukaj książkę o podanym identyfikatorze
            Książki ksiazka = _context.Książkis.FirstOrDefault(k => k.KsiążkaId == _ksiazkaId);

            if (ksiazka != null)
            {
                // Wypełnij TextBoxy danymi książki
                tytułTextBox.Text = ksiazka.Tytuł;
                imieAutoraTextBox.Text = ksiazka.Autor?.Imię;
                nazwiskoAutoraTextBox.Text = ksiazka.Autor?.Nazwisko;
                wydawnictwoTextBox.Text = ksiazka.Wydawnictwo?.Nazwa;
                kategoriaTextBox.Text = ksiazka.Kategoria?.Nazwa;
            }
        }

        private void ZapiszButton_Click(object sender, RoutedEventArgs e)
        {
            // Znajdź książkę do edycji
            Książki ksiazka = _context.Książkis.FirstOrDefault(k => k.KsiążkaId == _ksiazkaId);

            if (ksiazka != null)
            {
                // Zaktualizuj dane książki na podstawie wprowadzonych wartości w TextBoxach
                ksiazka.Tytuł = tytułTextBox.Text;

                // Znajdź lub utwórz nowego autora na podstawie podanego imienia i nazwiska
                string imie = imieAutoraTextBox.Text;
                string nazwisko = nazwiskoAutoraTextBox.Text;
                Autorzy autor = _context.Autorzies.FirstOrDefault(a => a.Imię == imie && a.Nazwisko == nazwisko);
                if (autor == null)
                {
                    // Set the AutorId property to a unique value that doesn't already exist in the Autorzy table
                    int newAutorId = _context.Autorzies.Max(a => a.AutorId) + 1;
                    autor = new Autorzy { AutorId = newAutorId, Imię = imie, Nazwisko = nazwisko };
                    _context.Autorzies.Add(autor);
                }
                ksiazka.Autor = autor;

                // Znajdź lub utwórz nowe wydawnictwo na podstawie podanej nazwy
                string nazwaWydawnictwa = wydawnictwoTextBox.Text;
                Wydawnictwa wydawnictwo = _context.Wydawnictwas.FirstOrDefault(w => w.Nazwa == nazwaWydawnictwa);
                if (wydawnictwo == null)
                {
                    // Set the WydawnictwoId property to a unique value that doesn't already exist in the Wydawnictwa table
                    int newWydawnictwoId = _context.Wydawnictwas.Max(w => w.WydawnictwoId) + 1;
                    wydawnictwo = new Wydawnictwa { WydawnictwoId = newWydawnictwoId, Nazwa = nazwaWydawnictwa };
                    _context.Wydawnictwas.Add(wydawnictwo);
                }
                ksiazka.Wydawnictwo = wydawnictwo;

                // Znajdź lub utwórz nową kategorię na podstawie podanej nazwy
                string nazwaKategorii = kategoriaTextBox.Text;
                Kategorie kategoria = _context.Kategories.FirstOrDefault(k => k.Nazwa == nazwaKategorii);
                if (kategoria == null)
                {
                    // Set the KategoriaId property to a unique value that doesn't already exist in the Kategorie table
                    int newKategoriaId = _context.Kategories.Max(k => k.KategoriaId) + 1;
                    kategoria = new Kategorie { KategoriaId = newKategoriaId, Nazwa = nazwaKategorii };
                    _context.Kategories.Add(kategoria);
                }
                ksiazka.Kategoria = kategoria;

                // Zapisz zmiany w bazie danych
                _context.SaveChanges();

                // Zamknij okno
                Close();
            }
            else
            {
                MessageBox.Show("Nie znaleziono książki do edycji.");
            }
        }
    }
}
