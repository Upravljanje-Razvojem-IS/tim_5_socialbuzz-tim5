using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IsporukaService.Entities
{
    public class Isporuka
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// pocetak porudzbine
        /// </summary>
        [Required]
        public DateTime DatumPorudzbine { get; set; }
        /// <summary>
        /// vreme isporuke
        /// </summary>
        [Required]
        public DateTime DatumIsporuke { get; set; }
        /// <summary>
        /// Firma
        /// </summary>
        [Required]
        public string Firma { get; set; }
        /// <summary>
        /// ukupan trosak
        /// </summary>
        [Required]
        public decimal Trosak { get; set; }
        /// <summary>
        /// Id prodavca
        /// </summary>
        [Required]
        public int ProdavacId { get; set; }
        /// <summary>
        /// Id kupca
        /// </summary>
        [Required]
        public int KupacId { get; set; }
        /// <summary>
        /// Id lokacije
        /// </summary>
        [ForeignKey("Lokacija")]
        public Guid LokacijaId { get; set; }
        /// <summary>
        /// Lokacija objekat
        /// </summary>
        public Lokacija Lokacija { get; set; }
    }
}
