using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public partial class Docent
    {
        public string Naam => $"{Voornaam} {Familienaam}";
    }
}
