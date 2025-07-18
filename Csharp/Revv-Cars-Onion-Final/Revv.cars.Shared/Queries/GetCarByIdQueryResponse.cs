using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revv.cars.Shared.Queries
{
    public class GetCarByIdQueryResponse
    {
        public string? Id { get; set; }
        public string? Image { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public string? Place { get; set; }
        public int Number { get; set; }
        public string? Date { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UserId { get; set; }
    }
}
