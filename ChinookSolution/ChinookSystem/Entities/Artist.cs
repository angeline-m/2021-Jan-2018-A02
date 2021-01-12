﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
#endregion

namespace ChinookSystem.Entities
{
    [Table("Artists")]
    internal class Artist
    {
        private string _Name;
        [Key]
        public int ArtistID { get; set; }
        [StringLength(120, ErrorMessage = "Artist name is limited to 120 characters.")]
        public string Name
        {
            get { return _Name; }
            set
            { _Name = string.IsNullOrEmpty(value) ? null : value; }
        }

        //navigational property *PARENT*
        //one to many direction parent to child
        public virtual ICollection<Album> Albums { get; set; }
    }
}
