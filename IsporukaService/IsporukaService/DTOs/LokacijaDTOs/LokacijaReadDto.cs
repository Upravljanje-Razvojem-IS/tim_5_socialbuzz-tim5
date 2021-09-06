using System;

namespace IsporukaService.DTOs.LokacijaDTOs
{
    public class LokacijaReadDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Drzava
        /// </summary>
        public string Drzava { get; set; }
        /// <summary>
        /// Grad
        /// </summary>
        public string Grad { get; set; }
        /// <summary>
        /// Adresa
        /// </summary>
        public string Adresa { get; set; }
        /// <summary>
        /// Ptt
        /// </summary>
        public string Ptt { get; set; }
    }
}
