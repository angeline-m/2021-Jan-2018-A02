using System;
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
    [Table("Albums")]
    internal class Album
    {
        private string _ReleaseLabel;
        [Key]
        public int AlbumID { get; set; }

        [Required(ErrorMessage = "Album title is required")]
        [StringLength(160, ErrorMessage = "Album title is limited to 160 characters.")]
        public string Title { get; set; }
        public int ArtistId { get; set; }
        public int ReleaseYear { get; set; }

        [StringLength(50, ErrorMessage = "Album release label is limited to 120 characters.")]
        public string ReleaseLabel
        {
            get { return _ReleaseLabel; }
            set
            { _ReleaseLabel = string.IsNullOrEmpty(value) ? null : value; }
        }

        //[NotMapped] annotations are also allowed

        //Navigational properties *CHILD*
        //many to one direction
        public virtual Artist Artist { get; set; }
        //one to many direction
        public virtual ICollection<Track> Track { get; set; }

    }
}
