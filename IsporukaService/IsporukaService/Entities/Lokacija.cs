using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IsporukaService.Entities
{
    public class Lokacija
    {
        /// <summary>
        /// Id lokacije
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// naziv drzave
        /// </summary>
        [Required]
        public string Drzava { get; set; }
        /// <summary>
        /// naziv grada
        /// </summary>
        [Required]
        public string Grad { get; set; }
        /// <summary>
        /// naziv adrese
        /// </summary>
        [Required]
        public string Adresa { get; set; }
        /// <summary>
        /// ptt broj
        /// </summary>
        [Required]
        public string Ptt { get; set; }
        /// <summary>
        /// list svih isporuka
        /// </summary>
        public List<Isporuka> Isporuke { get; set; }
    }
}
