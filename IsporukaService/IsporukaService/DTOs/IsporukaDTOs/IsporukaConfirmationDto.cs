using System;

namespace IsporukaService.DTOs.IsporukaDTOs
{
    public class IsporukaConfirmationDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Datum porudzbine
        /// </summary>
        public DateTime DatumPorudzbine { get; set; }
        /// <summary>
        /// Datum isporuke
        /// </summary>
        public DateTime DatumIsporuke { get; set; }
        /// <summary>
        /// Firma
        /// </summary>
        public string Firma { get; set; }
        /// <summary>
        /// trosak
        /// 
        /// </summary>
        public decimal Trosak { get; set; }
        /// <summary>
        /// ProdavacId
        /// </summary>
        public int ProdavacId { get; set; }
        /// <summary>
        /// KupacId
        /// </summary>
        public int KupacId { get; set; }
        /// <summary>
        /// Lokacija Id
        /// </summary>
        public int LokacijaId { get; set; }
    }
}
