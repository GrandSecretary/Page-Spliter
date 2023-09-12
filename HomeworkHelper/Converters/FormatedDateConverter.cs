using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HomeworkHelper.Converters
{
    public class FormatedDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime objectiveTime)
            {
                DateTime today = DateTime.Today;

                if(objectiveTime == today)
                {
                    return "Today";
                }
                else if(objectiveTime.Day - today.Day == 1)
                {
                    return "Tomorrow";
                }
                else if(today.Day - objectiveTime.Day == 1)
                {
                    return "Yesterday";
                }
                else
                {
                    return objectiveTime.ToShortDateString();
                }
            }
            else return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
