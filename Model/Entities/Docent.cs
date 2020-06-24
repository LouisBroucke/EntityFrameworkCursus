using System;

namespace Model.Entities
{
    public partial class Docent
    {
        //Properties
        public int DocentId { get; set; }
        public string Voornaam { get; set; }
        public string Familienaam { get; set; }
        public decimal Wedde { get; set; }
        public DateTime InDienst { get; set; }
        public bool? HeeftRijbewijs { get; set; }
        public string LandCode { get; set; }
        public int CampusId { get; set; }

        //Navigation Properties
        public virtual Campus Campus { get; set; }
        public virtual Land Land { get; set; }
    }
}