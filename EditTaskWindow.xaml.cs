using System;
using System.Linq;
using System.Windows;
using CertificationsApp;
using Microsoft.EntityFrameworkCore;

namespace CertificationsApp
{
    public partial class EditTaskWindow : Window
    {
        private readonly TasksMenagerContext _context;
        private readonly Task _task;

        public event EventHandler TaskUpdated;

        public EditTaskWindow(TasksMenagerContext context, Task task)
        {
            InitializeComponent();

            _context = context;
            _task = task;

            // Pobierz listę kategorii i tagów z bazy danych i przypisz do ComboBox i ListBox
            var categories = _context.Kategories.ToList();
            CategoryComboBox.ItemsSource = categories;

            var tags = _context.Tagis.ToList();
            TagsListBox.ItemsSource = tags;

            // Ustaw wartości pól formularza na podstawie wybranego zadania
            NameTextBox.Text = task.Nazwa;
            DescriptionTextBox.Text = task.Opis;
            PriorityComboBox.SelectedIndex = task.Priorytet.GetValueOrDefault() - 1;
            DueDatePicker.SelectedDate = task.TerminWykonania;

            // Zaznacz odpowiednie tagi na podstawie powiązań zadanego zadania
            var taskTags = task.TaskTags.Select(tt => tt.Tag).ToList();
            foreach (var tag in taskTags)
            {
                TagsListBox.SelectedItems.Add(tag);
            }
        }

        private void UpdateTaskButton_Click(object sender, RoutedEventArgs e)
        {
            // Zaktualizuj zadanie na podstawie wprowadzonych danych
            _task.Nazwa = NameTextBox.Text;
            _task.Opis = DescriptionTextBox.Text;
            _task.Priorytet = PriorityComboBox.SelectedIndex + 1;
            _task.TerminWykonania = DueDatePicker.SelectedDate;

            // Usuń istniejące powiązania tagów
            var taskTags = _task.TaskTags.ToList();
            foreach (var taskTag in taskTags)
            {
                _context.TaskTags.Remove(taskTag);
            }

            // Dodaj nowe powiązania tagów na podstawie wybranych tagów w ListBox
            var selectedTags = TagsListBox.SelectedItems.Cast<Tagi>().ToList();
            foreach (var tag in selectedTags)
            {
                var taskTag = new TaskTag
                {
                    TaskId = _task.TaskId,
                    TagId = tag.TagId
                };
                _context.TaskTags.Add(taskTag);
            }

            // Zapisz zmiany w bazie danych
            _context.SaveChanges();

            // Wywołaj zdarzenie TaskUpdated
            TaskUpdated?.Invoke(this, EventArgs.Empty);

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
