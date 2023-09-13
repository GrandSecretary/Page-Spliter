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
        /// Subject name
        /// </summary>
        public string SubjectName { set; get; }
        /// <summary>
        /// Page count
        /// </summary>
        public int PageCount { set; get; }
        /// <summary>
        /// Deadline
        /// </summary>
        public DateTime DeadLine { set; get; }
        /// <summary>
        /// Distribution by day
        /// </summary>
        public Objective[] Objectives { set; get; }

        /// <summary>
        /// A subject to be mastered over a period of time
        /// </summary>
        /// <param name="SubjectName">Subject name</param>
        /// <param name="PageCount">Page count</param>
        /// <param name="DeadLine"> Deadlineу</param>
        /// <param name="Objectives">Distribution by day</param>
        public Subject(string SubjectName, int PageCount, DateTime DeadLine, Objective[] Objectives)
        {
            this.SubjectName = SubjectName;
            this.PageCount = PageCount;
            this.DeadLine = DeadLine;
            this.Objectives = Objectives;
        }
    }
}
