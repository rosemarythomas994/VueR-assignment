using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Revv.cars.Shared
{
    public class CarUpload
    {
        [Required]
        public IFormFile? ImageFile { get; set; }

        [Required]
        public string? Brand { get; set; }

        [Required]
        public string? Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string? Place { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public string? Date { get; set; }

        [Required]
        public string? UserId { get; set; }
    }
}
