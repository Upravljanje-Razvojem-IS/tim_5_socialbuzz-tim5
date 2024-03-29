﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Models
{
    /// <summary>
    /// DTO class which represents model of picture for creation.
    /// </summary>
    public class PictureCreationDto
    {
     
        /// <summary>
        /// Url path of the picture.
        /// </summary>
        [Required(ErrorMessage = "You must enter url path of the picture.")]
        public string Url { get; set; }

        /// <summary>
        /// ID of the post for which the picture was published.
        /// </summary>
        [Required(ErrorMessage = "You must enter post for the picture.")]
        [ForeignKey("PostID")]
        public Guid PostID { get; set; }
    }
}
