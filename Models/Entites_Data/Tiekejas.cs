using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlausDaryklosGamybosValdymoSistema.Models
{
    public partial class Tiekejas
    {
        public Tiekejas()
        {
            Sutartis = new HashSet<Sutartis>();
        }

        public decimal TiekejoId { get; set; }
       
        public string TiekejoPavadinimas { get; set; }
       
        public string TiekejoTtipas { get; set; }

    public string ImonesKodas { get; set; }
    public string KontaktinisNR { get; set; }
    public string Adresas { get; set; }

    public ICollection<Sutartis> Sutartis { get; set; }
    }
}
