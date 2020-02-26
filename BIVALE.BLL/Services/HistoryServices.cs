using BIVALE.BLL.Generic;
using BIVALE.BLL.Interfaces;
using BIVALE.BLL.Mapping;
using BIVALE.DAL.Models;
using BIVALE.DTO;
using System;
using System.Collections.Generic;

namespace BIVALE.BLL.Services
{
	public class HistoryServices : UnitOfWork, IHistoryServices
	{
		private IRepository<History> historyRepository;
		public IRepository<History> HistoryRepository
		{
			get
			{
				if (historyRepository == null)
				{
					historyRepository = GetRepository<History>();
				}
				return historyRepository;
			}
		}

		public void DeleteHistory(int HistoryID)
		{
			throw new NotImplementedException();
		}

		public HistoryDTO GetHistoryByID(int HistoryId)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<HistoryDTO> GetHistorys()
		{
			var objHistoryMapper = DependencyInjector.Retrieve<HistoryMapper>();
			var target = HistoryRepository.Get();
			var result = objHistoryMapper.MapList(target);
			return result;
		}

		public void InsertHistory(HistoryDTO History)
		{
			HistoryMapper obj = DependencyInjector.Retrieve<HistoryMapper>();
			var targetEntity = obj.Map(History);
			var targetDTO = obj.Map(targetEntity);
			HistoryRepository.Insert(targetEntity);
			Save();
		}

		public void UpdateHistory(HistoryDTO History)
		{
			throw new NotImplementedException();
		}
	}
}
