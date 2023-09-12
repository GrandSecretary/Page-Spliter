using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkHelper
{
    public struct Objective
    {
        public DateTime ObjectiveDate { get; set; }
        public int PageCount { get; set; }
        public bool IsCompleted { get; set; }
        public Objective(DateTime ObjectiveDate, int PageCount)
        {
            this.ObjectiveDate = ObjectiveDate;
            this.PageCount = PageCount;
            IsCompleted = false;
        }
        public Objective(DateTime ObjectiveDate, int PageCount, bool IsCompleted)
        {
            this.ObjectiveDate = ObjectiveDate;
            this.PageCount = PageCount;
            this.IsCompleted = IsCompleted;
        }
    }
}
