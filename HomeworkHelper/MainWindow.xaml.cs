using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace HomeworkHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        readonly ObservableCollection<Subject> subjects = new ObservableCollection<Subject>();
        ObservableCollection<Objective> objs = new ObservableCollection<Objective>();

        private int chosenSubjectSelection;


        readonly XMLSerializator serialization = new XMLSerializator();

        private ObservableCollection<Subject> ImportSavedData()
        {
            string path = String.Format($@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Page Spliter\saved.xml");

            DirectoryInfo directory = new DirectoryInfo($@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Page Spliter");
            if (!directory.Exists) Directory.CreateDirectory($@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Page Spliter");

            return serialization.Import(path);
        }

        private Objective[] DaysDistribution(int pageCount, DateTime deadline)
        {
            if (pageCount != 0 && deadline != null)
            {

                DateTime today = DateTime.Today;
                int days = (deadline - today).Days;
                Objective[] objectives = new Objective[days];

                for (int i = 0; i < objectives.Length; i++) objectives[i].ObjectiveDate = today.AddDays(i);

                for (int i = 0; pageCount != 0; i++)
                {
                    objectives[i].PageCount++;
                    pageCount--;

                    if (i == objectives.Length - 1) i = -1;
                }


                return objectives;

            }
            else return null;
        }

        public MainWindow()
        {
            InitializeComponent();

            subjects = ImportSavedData();
            subjects_listBox.ItemsSource = subjects;
            objectives_listBox.ItemsSource = objs;
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddSubject addSubject = new AddSubject
            {
                Owner = this
            };
            addSubject.ShowDialog();

            Objective[] objectives = DaysDistribution(addSubject.EnteredPageCount,
                addSubject.EnteredDeadline);

            if (objectives != null)
            {
                subjects.Add(new Subject(addSubject.EnteredSubject,
                    addSubject.EnteredPageCount,
                    addSubject.EnteredDeadline,
                    objectives));
                saveButton.IsEnabled = true;
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Subject subject = (Subject)subjects_listBox.SelectedItem;
            int selectedSubjectIndex = subjects.IndexOf((Subject)subjects_listBox.SelectedItem);

            EditSubject editSubject = new EditSubject(subject.SubjectName, subject.PageCount, subject.DeadLine)
            {
                Owner = this
            };
            editSubject.ShowDialog();

            Objective[] objectives = DaysDistribution(editSubject.PageCount_toChange, editSubject.Deadline_toChange);

            if (objectives != null)
            {
                subjects[selectedSubjectIndex] = new Subject(editSubject.SubjectName_toChange,
                    editSubject.PageCount_toChange, editSubject.Deadline_toChange, objectives);
            }

        }
        private void Subjects_listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (subjects_listBox.SelectedItem != null)
            {
                chosenSubjectSelection = subjects_listBox.SelectedIndex;

                objs = new ObservableCollection<Objective>(((Subject)subjects_listBox.SelectedItem).Objectives);
                objectives_listBox.ItemsSource = objs;

                editButton.IsEnabled = true;
                deleteButton.IsEnabled = true;
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (saveButton.IsEnabled)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save your changes?",
                    "Confirmation", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (result == MessageBoxResult.Yes)
                {
                    serialization.Export(@"Saved.xml", subjects);
                }
                else
                {

                }
            }
        }

        private void Objects_listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (objectives_listBox.SelectedItem != null && subjects_listBox.SelectedItem != null)
            {
                completeButton.IsEnabled = true;

                if (!((Objective)objectives_listBox.SelectedItem).IsCompleted &&
                    ((Objective)objectives_listBox.SelectedItem).ObjectiveDate < DateTime.Today)
                {
                    spreadButton.IsEnabled = true;
                }
                else spreadButton.IsEnabled = false;
            }
            else { completeButton.IsEnabled = false; spreadButton.IsEnabled = false; }
        }

        private void CompleteButton_Click(object sender, RoutedEventArgs e)
        {
            int subjectIndex = subjects.IndexOf((Subject)subjects_listBox.SelectedItem);
            int objectiveIndex = Array.IndexOf(subjects[subjectIndex].Objectives,
                (Objective)objectives_listBox.SelectedItem);

            if (!subjects[subjectIndex].Objectives[objectiveIndex].IsCompleted)
            {
                subjects[subjectIndex].Objectives[objectiveIndex].IsCompleted = true;
                objs[objectiveIndex] = new Objective(objs[objectiveIndex].ObjectiveDate,
                    objs[objectiveIndex].PageCount,
                    true);

                subjects[subjectIndex] = new Subject(subjects[subjectIndex].SubjectName,
                    subjects[subjectIndex].PageCount - subjects[subjectIndex].Objectives[objectiveIndex].PageCount,
                    subjects[subjectIndex].DeadLine, subjects[subjectIndex].Objectives);

                saveButton.IsEnabled = true;


            }
            else
            {
                MessageBox.Show("Task is already completed!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            subjects_listBox.SelectedIndex = chosenSubjectSelection;

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButton.OKCancel,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.OK)
            {
                subjects.Remove((Subject)subjects_listBox.SelectedItem);
                objs.Clear();
                saveButton.IsEnabled = true;
                deleteButton.IsEnabled = false;
                editButton.IsEnabled = false;
            }
            else
            {

            }
        }



        private void SplitButton_Click(object sender, RoutedEventArgs e)
        {
            Objective selectedObjective = (Objective)objectives_listBox.SelectedItem;
            Objective[] objectives = ((Subject)subjects_listBox.SelectedItem).Objectives;

            int pageCount = ((Objective)objectives_listBox.SelectedItem).PageCount;

            List<Objective> objectivesList = new List<Objective>(objectives);
            objectivesList.Remove(selectedObjective);
            objectives = objectivesList.ToArray();


            for (int i = 0; pageCount > 0; i++)
            {
                if ((objectives[i].ObjectiveDate - DateTime.Today).Days >= 0 && !objectives[i].IsCompleted)
                {
                    objectives[i].PageCount++;
                    pageCount--;
                }


                if (i == objectives.Length - 1) i = -1;
            }

            objs = new ObservableCollection<Objective>(objectives);
            int subjectIndex = subjects.IndexOf((Subject)subjects_listBox.SelectedItem);

            subjects[subjectIndex] = new Subject(subjects[subjectIndex].SubjectName,
                subjects[subjectIndex].PageCount, subjects[subjectIndex].DeadLine, objectives);


            subjects_listBox.SelectedIndex = chosenSubjectSelection;

            saveButton.IsEnabled = true;
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            serialization.Export($@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Page Spliter\saved.xml", subjects);
            saveButton.IsEnabled = false;
        }
    }
}
