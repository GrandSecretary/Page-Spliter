using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HomeworkHelper
{
    /// <summary>
    /// Interaction logic for EditSubject.xaml
    /// </summary>
    public partial class EditSubject : Window
    {
        public string SubjectName_toChange { get; set; }
        public int PageCount_toChange { get; set; }
        public DateTime Deadline_toChange { get; set; }
        public EditSubject(string subjectName, int pageCount, DateTime deadline)
        {
            SubjectName_toChange = subjectName;
            PageCount_toChange = pageCount;
            Deadline_toChange = deadline;

            InitializeComponent();

            subjectName_textBox.Text = SubjectName_toChange;
            pageCount_textBox.Text = Convert.ToString(PageCount_toChange);
            deadline_textBox.Text = Deadline_toChange.ToShortDateString();

            subjectName_textBox.TextChanged += TextBox_TextChanged;
            pageCount_textBox.TextChanged += TextBox_TextChanged;
            deadline_textBox.TextChanged += TextBox_TextChanged;
        }
        public EditSubject()
        {
            InitializeComponent();

            subjectName_textBox.Text = SubjectName_toChange;
            pageCount_textBox.Text = Convert.ToString(PageCount_toChange);
            deadline_textBox.Text = Deadline_toChange.ToShortDateString();

            subjectName_textBox.TextChanged += TextBox_TextChanged;
            pageCount_textBox.TextChanged += TextBox_TextChanged;
            deadline_textBox.TextChanged += TextBox_TextChanged;
        }

        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            apply_Button.IsEnabled =
                !string.IsNullOrWhiteSpace(subjectName_textBox.Text) &&
                !string.IsNullOrWhiteSpace(pageCount_textBox.Text) &&
                !string.IsNullOrWhiteSpace(deadline_textBox.Text);
        }

        private void cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void apply_Button_Click(object sender, RoutedEventArgs e)
        {
            EnteredInfoControl();
        }

        private void EnteredInfoControl()
        {
            int pageCount;
            DateTime deadline;


            try
            {
                pageCount = Convert.ToInt32(pageCount_textBox.Text);
                if (pageCount <= 0) throw new FormatException();

                try
                {
                    deadline = Convert.ToDateTime(deadline_textBox.Text);

                    Deadline_toChange = deadline;
                    PageCount_toChange = pageCount;
                    SubjectName_toChange = subjectName_textBox.Text;



                    if ((deadline - DateTime.Today).Days < 0) throw new Exception();
                    Close();
                }
                catch
                {
                    MessageBox.Show("Entered deadline date is not valid", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    deadline_textBox.Text = String.Empty;
                }
            }
            catch
            {
                MessageBox.Show("Entered page is not valid", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                pageCount_textBox.Text = String.Empty;
            }

        }

    }
}
