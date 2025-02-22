using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymClub.Domain.Models
{
    public class PageSettingModel
    {
        public static IEnumerable<int> PageSizeOptions { get; } = new int[] { 10, 20, 30 };
        public PageSettingModel() { }
        public PageSettingModel(int pageNo, int rowCount)
        {
            PageNo = pageNo;
            RowCount = rowCount;
        }
        public int PageSize { get; set; } = 10;
        public int PageNo { get; set; } = 1;
        public int RowCount { get; set; } = 10;
        public int SkipRowCount => (PageNo - 1) * PageSize;
    }
}
