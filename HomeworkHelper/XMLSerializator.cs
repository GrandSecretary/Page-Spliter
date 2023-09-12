using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HomeworkHelper
{
    internal class XMLSerializator
    {
        public void Export(string path, ObservableCollection<Subject> subjects)
        {
            XmlSerializer xml = new XmlSerializer(typeof(ObservableCollection<Subject>));

            using(Stream fStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                xml.Serialize(fStream, subjects);
            }
        }

        public ObservableCollection<Subject> Import(string path)
        {
            ObservableCollection<Subject> subjects;
            try
            {
                XmlSerializer xml = new XmlSerializer(typeof(ObservableCollection<Subject>));

                using(Stream fStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    subjects = xml.Deserialize(fStream) as ObservableCollection<Subject>;
                }
                return subjects;
            }
            catch
            {
                return new ObservableCollection<Subject>();
            }
        }
    }
}
