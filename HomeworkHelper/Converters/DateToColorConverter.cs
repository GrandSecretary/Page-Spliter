using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace HomeworkHelper.Converters
{
    public class DateToColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] is DateTime objectiveDate && values[1] is bool isCompleted)
            {
                DateTime today = DateTime.Today;

                if (isCompleted)
                {
                    return Brushes.Green;
                }
                else if (today == objectiveDate)
                {
                    return Brushes.Black;
                }
                else if (today < objectiveDate)
                {
                    return Brushes.Gray;
                }
                else
                {
                    return Brushes.Red;
                }
            }
            else return Brushes.Yellow;

        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

}
