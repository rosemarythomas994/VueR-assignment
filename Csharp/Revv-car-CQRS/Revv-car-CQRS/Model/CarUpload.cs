using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc; // Add this

namespace Revv_car_CQRS.Model
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
