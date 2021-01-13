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
    internal class Track
    {
        private string _Composer;
        [Key]
        public int TrackId { get; set; }

        [Required(ErrorMessage = "Track Name is required")]
        [StringLength(120, ErrorMessage = "Track Name is limited to 120 characters.")]
        public string Name { get; set; }
        public int? AlbumId { get; set; }

        [Required(ErrorMessage = "Media Type ID is required")]
        public int MediaTypeId { get; set; }
        public int? GenreId { get; set; }
        public string Composer
        {
            get
            {
                return _Composer;
            }
            set
            {
                _Composer = string.IsNullOrEmpty(value) ? null : value;
            }
        }

        [Required(ErrorMessage = "Milliseconds is required")]
        public int Milliseconds { get; set; }
        public int? Bytes { get; set; }

        [Required(ErrorMessage = "Unit Price is required")]
        public decimal UnitPrice { get; set; }
    }
}
