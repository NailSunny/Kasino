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
    
    public partial class Game_Sessions
    {
        public int Session_ID { get; set; }
        public Nullable<int> Game_ID { get; set; }
        public Nullable<int> User_ID { get; set; }
        public Nullable<int> Bet_Amount { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<decimal> Win_Amount { get; set; }
    
        public virtual Games Games { get; set; }
        public virtual Users Users { get; set; }
    }
}
