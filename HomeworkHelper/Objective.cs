using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkHelper
{
    public struct Objective
    {
        /// <summary>
        /// Objective date
        /// </summary>
        public DateTime ObjectiveDate { get; set; }
        /// <summary>
        /// Page count of objective
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// Execution status
        /// </summary>
        public bool IsCompleted { get; set; }
        /// <summary>
        /// Objective, set for the day
        /// </summary>
        /// <param name="ObjectiveDate">Objective date</param>
        /// <param name="PageCount">Page count of objective</param>
        public Objective(DateTime ObjectiveDate, int PageCount)
        {
            this.ObjectiveDate = ObjectiveDate;
            this.PageCount = PageCount;
            IsCompleted = false;
        }
        /// <summary>
        /// Objective, set for the day
        /// </summary>
        /// <param name="ObjectiveDate">Objective date</param>
        /// <param name="PageCount">Page count of objective</param>
        /// <param name="IsCompleted">Execution status</param>
        public Objective(DateTime ObjectiveDate, int PageCount, bool IsCompleted)
        {
            this.ObjectiveDate = ObjectiveDate;
            this.PageCount = PageCount;
            this.IsCompleted = IsCompleted;
        }
    }
}
