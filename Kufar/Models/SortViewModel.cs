using System;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Internal;

namespace Kufar.Models
{
    public class SortViewModel
    {
        public SortType TitleSort { get; set; } 
        public SortType DescriptionSort { get; set; }
        public SortType Current{ get; set; }

        public SortViewModel(SortType sortOrder)
        {
            TitleSort = sortOrder == SortType.TitleAsc ? SortType.TitleDesc : SortType.TitleAsc;
            DescriptionSort = sortOrder == SortType.DescriptionAsc ? SortType.DescriptionDesc : SortType.DescriptionAsc;
            Current = sortOrder;
        }
    }

} 
