//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Casino_besit.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reports
    {
        public int Report_ID { get; set; }
        public Nullable<int> User_ID { get; set; }
        public Nullable<decimal> Wins { get; set; }
        public Nullable<decimal> Losses { get; set; }
    
        public virtual Users Users { get; set; }
    }
}
