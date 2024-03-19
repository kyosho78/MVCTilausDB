


namespace MVCTilausDB.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Drawing;

    public class OrderSummaryData
    {
        public int TilausID { get; set; }
        public int AsiakasID { get; set; }

      
        public string AsiakkaanNimi { get; set; }


        public string Toimitusosoite { get; set; }



        public DateTime Tilauspvm { get; set; }

        public DateTime Toimituspvm { get; set; }


        public int TuoteID { get; set; }

     
        public string TuotteenNimi { get; set; }

        public decimal Ahinta { get; set; }

        public int TilausriviID { get; set; }

        public int Maara { get; set; }

       public decimal ahinta { get; set; }

      



    }
}