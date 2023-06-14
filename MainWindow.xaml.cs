using System;
using System.Linq;
using System.Windows;
using CertificationsApp;
using Microsoft.EntityFrameworkCore;

namespace TaskManager
{
    public partial class MainWindow : Window
    {
        private readonly TasksMenagerContext _context;

        public MainWindow()
        {
            InitializeComponent();

            _context = new TasksMenagerContext();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshTaskList();
        }

        private void RefreshTaskList()
        {
            // Pobierz listę zadań z bazy danych i przypisz do ListBox
            var tasks = _context.Tasks.Include(t => t.TaskTags).ThenInclude(tt => tt.Tag).ToList();
            TaskListBox.ItemsSource = tasks;
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            // Otwórz okno do dodawania zadania
            var addTaskWindow = new AddTaskWindow(_context);
            addTaskWindow.TaskAdded += AddTaskWindow_TaskAdded;
            addTaskWindow.ShowDialog();
        }

        private void EditTaskButton_Click(object sender, RoutedEventArgs e)
        {
            // Sprawdź, czy wybrano zadanie
            if (TaskListBox.SelectedItem is Task selectedTask)
            {
                // Otwórz okno do edycji zadania
                var editTaskWindow = new EditTaskWindow(_context, selectedTask);
                editTaskWindow.TaskUpdated += EditTaskWindow_TaskUpdated;
                editTaskWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Wybierz zadanie do edycji.");
            }
        }

        private void RemoveTaskButton_Click(object sender, RoutedEventArgs e)
        {
            // Sprawdź, czy wybrano zadanie
            if (TaskListBox.SelectedItem is Task selectedTask)
            {
                // Potwierdź usunięcie zadania
                var result = MessageBox.Show("Czy na pewno chcesz usunąć to zadanie?", "Potwierdź usunięcie", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    // Usuń zadanie z bazy danych
                    _context.Tasks.Remove(selectedTask);
                    _context.SaveChanges();

                    RefreshTaskList();
                }
            }
            else
            {
                MessageBox.Show("Wybierz zadanie do usunięcia.");
            }
        }

        private void AddTaskWindow_TaskAdded(object sender, EventArgs e)
        {
            // Odśwież listę zadań po dodaniu nowego zadania
            RefreshTaskList();
        }

        private void EditTaskWindow_TaskUpdated(object sender, EventArgs e)
        {
            // Odśwież listę zadań po zaktualizowaniu zadania
            RefreshTaskList();
        }
    }
}

