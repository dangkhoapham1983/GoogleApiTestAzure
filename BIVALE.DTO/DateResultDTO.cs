using System.Collections.Generic;

namespace BIVALE.DTO
{
    public class DateResultDTO
    {
        public string date { get; set; }
        public ICollection<TimeResultDTO> times { get; set; }
    }
}