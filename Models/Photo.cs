using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VinayakSuleyDotCom.Models
{
    [MetadataType(typeof(Photo_Validation))]
    public partial class Photo
    {
    }

    public class Photo_Validation
    {
        [Required(ErrorMessage = "Tags are required.")]
        public string Tags { get; set; }

        [Required(ErrorMessage = "DateTaken is required")]
        public DateTime DateTaken { get; set; }

        [Required(ErrorMessage = "DateUploaded is required")]
        public DateTime DateUploaded { get; set; }

        [Required(ErrorMessage="Preference is required")]
        [Range(1, 3)]
        public int Preference { get; set; }

        [Required(ErrorMessage="Worksafe is required")]
        public bool Worksafe { get; set; }

        [Required(ErrorMessage = "Path to a small thumbnail is required")]
        public string PathToSmallThumb { get; set; }

        [Required(ErrorMessage = "Path to a medium thumbnail is required")]
        public string PathToMediumThumb { get; set; }

        [Required(ErrorMessage = "Path to original file is required")]
        public string PathToOriginalFile { get; set; }

        [Required(ErrorMessage = "Width is a required property")]
        [Range(0, 2048, ErrorMessage = "Size is too large, keep it under 2048")]
        public int Width { get; set; }
        
        [Required(ErrorMessage = "Height is a required property")]
        [Range(0, 2048, ErrorMessage = "Size is too large, keep it under 2048")]
        public int Height { get; set; }

        public string TextField1 { get; set; }

        public string TextField2 { get; set; }

        public string TextField3 { get; set; }

        public string TextField4 { get; set; }
    }
}