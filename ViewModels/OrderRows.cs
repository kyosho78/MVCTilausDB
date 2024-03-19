using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCTilausDB.ViewModels
{
    public class OrderRows
    {
        public int TilausID { get; set; }


        public int TuoteID { get; set; }

        public string TuotteenNimi { get; set; }

        public decimal Ahinta { get; set; }

        public int Maara { get; set; }







    }
}