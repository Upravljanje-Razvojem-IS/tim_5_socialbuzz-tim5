using System;
using System.ComponentModel.DataAnnotations;

namespace LajkMikroservis.Entities
{
    public class LikeTip
    {
        /// <summary>
        /// Like tip id
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Tip lajka
        /// </summary>
        [Required]
        public string Tip { get; set; }
    }
}
