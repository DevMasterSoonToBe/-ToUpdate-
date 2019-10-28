using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace AlausDaryklosGamybosValdymoSistema.Models
{
    public partial class Sutartis
    {
        public decimal SutartiesId { get; set; }
        public decimal? KlientoUzsakymoId { get; set; }
        public decimal? TiekejoId { get; set; }
        public decimal? KlientoId { get; set; }
    /*[DataType(DataType.Upload)]
     public Byte[] Sutartis1 { get; set; }*/
        [Required(ErrorMessage = "Sutarties pasirašymo data privaloma!")]
        [DataType(DataType.Date, ErrorMessage = "Įveskite tinkamą datą!")]
            public DateTime SutartiesPasirasymoData { get; set; }
        [Required(ErrorMessage = "Sutarties terminas privalomas!")]
        [DataType(DataType.Date, ErrorMessage = "Įveskite tinkamą datą!")]
            public DateTime SutartiesTerminas { get; set; }
    public string FailoPavadinimas { get; set; }

    public Klientas Kliento { get; set; }
        public KlientoUzsakymoAplankas KlientoUzsakymo { get; set; }
        public Tiekejas Tiekejo { get; set; }
    }
}
