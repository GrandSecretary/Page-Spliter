using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    /// Interaction logic for AddSubject.xaml
    /// </summary>
    public partial class AddSubject : Window
    {
        public string EnteredSubject { get; private set; }
        public int EnteredPageCount { get; private set; }
        public DateTime EnteredDeadline { get; private set; }
        public AddSubject()
        {
            InitializeComponent();

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
        
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void applyButton_Click(object sender, RoutedEventArgs e)
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
                try
                {
                    deadline = Convert.ToDateTime(deadline_textBox.Text);

                    EnteredDeadline = deadline;
                    EnteredPageCount = pageCount;
                    EnteredSubject = subjectName_textBox.Text;

                    

                    if((deadline - DateTime.Today).Days < 0) throw new Exception();
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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && apply_Button.IsEnabled)
            {
                apply_Button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            
        }
    }
}
