using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingOrganizer
{
    public class Spotkanie
    {
        public int IdSpotkania { get; set; }
        public string NazwaSpotkania { get; set; }
        public DateTime GodzinaRozpoczecia { get; set; }
        public DateTime GodzinaZakonczenia { get; set; }
        public string Miejsce { get; set; }
    }
}
