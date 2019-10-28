using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlausDaryklosGamybosValdymoSistema.Models
{
    public partial class Klientas
    {
        public Klientas()
        {
            KlientoUzsakymoAplankas = new HashSet<KlientoUzsakymoAplankas>();
            Sutartis = new HashSet<Sutartis>();
        }

        public decimal KlientoId { get; set; }
        public string KlientoPavadinimas { get; set; }
        public string ImonesKodas { get; set; }
    public string KontaktinisNR { get; set; }
    public string Adresas { get; set; }

    public ICollection<KlientoUzsakymoAplankas> KlientoUzsakymoAplankas { get; set; }
        public ICollection<Sutartis> Sutartis { get; set; }
    }
}
