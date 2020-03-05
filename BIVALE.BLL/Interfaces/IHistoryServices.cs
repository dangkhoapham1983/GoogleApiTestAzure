using BIVALE.DTO;
using System.Collections.Generic;

namespace BIVALE.BLL.Interfaces
{
    public interface IHistoryServices
    {
        IEnumerable<HistoryDTO> GetHistories();

        HistoryResultDTO GetHistoriesByConditions(UserDTO user, UserDTO parentUser,
            string startDate, string endDate, string startTime, string endTime,
            string smartGatewayId, string category);
    }
}
