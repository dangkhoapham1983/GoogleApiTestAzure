using System.Collections.Generic;

namespace BIVALE.DTO
{
    public class HistoryResultDTO
    {
        public int smart_gateway_id { get; set; }
        public ICollection<DateResultDTO> dates { get; set; }
    }
}
