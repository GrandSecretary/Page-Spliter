using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HomeworkHelper.Converters
{
    public class StatusConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] is DateTime objectiveDate && values[1] is bool isCompleted)
            {
                DateTime today = DateTime.Today;

                if(isCompleted)
                {
                    return "Done";
                }
                else if (today == objectiveDate)
                {
                    return "In Progress";
                }
                else if (today < objectiveDate)
                {
                    return "Not Available";
                }
                else
                {
                    return "Expired";
                }
            }
            else return "Unknown";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
