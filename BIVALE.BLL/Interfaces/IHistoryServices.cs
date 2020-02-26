using BIVALE.DTO;
using System.Collections.Generic;

namespace BIVALE.BLL.Interfaces
{
	public interface IHistoryServices
	{
		IEnumerable<HistoryDTO> GetHistorys();
		HistoryDTO GetHistoryByID(int HistoryId);
		void InsertHistory(HistoryDTO History);
		void DeleteHistory(int HistoryID);
		void UpdateHistory(HistoryDTO History);
		void Save();
	}
}
