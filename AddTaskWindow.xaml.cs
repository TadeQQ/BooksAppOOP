using System;
using System.Linq;
using System.Windows;
using CertificationsApp;
using Microsoft.EntityFrameworkCore;

namespace TaskManager
{
    public partial class AddTaskWindow : Window
    {
        private readonly TasksMenagerContext _context;

        public event EventHandler TaskAdded;

        public AddTaskWindow(TasksMenagerContext context)
        {
            InitializeComponent();

            _context = context;

            // Pobierz listę kategorii i tagów z bazy danych i przypisz do ComboBox
            var categories = _context.Kategories.ToList();
            CategoryComboBox.ItemsSource = categories;

            var tags = _context.Tagis.ToList();
            TagsListBox.ItemsSource = tags;
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            // Utwórz nowe zadanie na podstawie wprowadzonych danych
            var newTask = new Task
            {
                Nazwa = NameTextBox.Text,
                Opis = DescriptionTextBox.Text,
                Priorytet = PriorityComboBox.SelectedIndex + 1,
                DataDodania = DateTime.Now,
                TerminWykonania = DueDatePicker.SelectedDate,
                Wykonane = false
            };

            // Dodaj zadanie do bazy danych
            _context.Tasks.Add(newTask);

            // Dodaj powiązane tagi do zadania
            var selectedTags = TagsListBox.SelectedItems.Cast<Tagi>().ToList();
            foreach (var tag in selectedTags)
            {
                var taskTag = new TaskTag
                {
                    TaskId = newTask.TaskId,
                    TagId = tag.TagId
                };
                _context.TaskTags.Add(taskTag);
            }

            // Zapisz zmiany w bazie danych
            _context.SaveChanges();

            // Wywołaj zdarzenie TaskAdded
            TaskAdded?.Invoke(this, EventArgs.Empty);

            // Zamknij okno
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Zamknij okno
            Close();
        }
    }
}
