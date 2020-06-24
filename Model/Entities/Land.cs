using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class Land
    {
        public Land()
        {
            Docenten = new List<Docent>();
        }

        public string LandCode { get; set; }
        public string Naam { get; set; }

        public virtual ICollection<Docent> Docenten { get; set; }
    }
}
