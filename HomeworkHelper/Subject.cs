using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkHelper
{
    public struct Subject
    {
        /// <summary>
        /// Назва предмету
        /// </summary>
        public string SubjectName { set; get; }
        /// <summary>
        /// Кількість сторінок
        /// </summary>
        public int PageCount { set; get; }
        /// <summary>
        /// Дата дедлайну
        /// </summary>
        public DateTime DeadLine { set; get; }
        /// <summary>
        /// Розподіл по дням
        /// </summary>
        public Objective[] Objectives { set; get; }

        /// <summary>
        /// Предмет, який треба освоїти за період часу
        /// </summary>
        /// <param name="SubjectName">Назва предмету</param>
        /// <param name="PageCount">Кількість сторінок</param>
        /// <param name="DeadLine">Дата дедлайну</param>
        /// <param name="Objectives">Розподіл по дням</param>
        public Subject(string SubjectName, int PageCount, DateTime DeadLine, Objective[] Objectives)
        {
            this.SubjectName = SubjectName;
            this.PageCount = PageCount;
            this.DeadLine = DeadLine;
            this.Objectives = Objectives;
        }
    }
}
