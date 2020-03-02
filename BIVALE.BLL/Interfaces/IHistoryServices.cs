using BIVALE.DTO;
using System.Collections.Generic;

namespace BIVALE.BLL.Interfaces
{
    public interface IHistoryServices
    {
        IEnumerable<HistoryDTO> GetHistorys();
        IEnumerable<HistoryDTO> GetHistoriesByConditions(UserDTO user,
            ICollection<NodePermissionDTO> userNodePaths, string date, string startTime,
            string endTime, string smartGatewayId, string category);
        HistoryDTO GetHistoryByID(int HistoryId);
        void InsertHistory(HistoryDTO History);
        void DeleteHistory(int HistoryID);
        void UpdateHistory(HistoryDTO History);
        void Save();
    }
}
