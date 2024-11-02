using Casino_besit.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino_besit
{
    public static class GlobalState
    {
        public static Users CurrentUser { get; set; }
        public static Games CurrentGame { get; set; }
    }
}
