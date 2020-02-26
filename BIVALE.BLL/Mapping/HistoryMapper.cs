using System;
using System.Collections.Generic;
using AutoMapper;
using BIVALE.BLL.Interfaces;
using BIVALE.BLL.Services;
using BIVALE.DAL.Models;
using BIVALE.DTO;

namespace BIVALE.BLL.Mapping
{
	public class HistoryMapper : IMapper<HistoryDTO, History>
	{
		public History Map(HistoryDTO obj)
		{
			var config = new MapperConfiguration(cfg => {
				cfg.AllowNullCollections = true;
				cfg.AddProfile(new MappingProfile());
			});
			var mapper = config.CreateMapper();
			History target = mapper.Map<History>(obj);
			return target;
		}

		public HistoryDTO Map(History obj)
		{
			var config = new MapperConfiguration(cfg => {
				cfg.AllowNullCollections = true;
				cfg.AddProfile(new MappingProfile());
			});
			var mapper = config.CreateMapper();
			var result = mapper.Map<HistoryDTO>(obj);
			return result;
		}

		public IEnumerable<History> MapList(IEnumerable<HistoryDTO> obj)
		{
			var config = new MapperConfiguration(cfg => {
				cfg.AllowNullCollections = true;
				cfg.AddProfile(new MappingProfile());
			});
			var mapper = config.CreateMapper();
			var result = mapper.Map<IEnumerable<History>>(obj);
			return result;
		}

		public IEnumerable<HistoryDTO> MapList(IEnumerable<History> obj)
		{
			var config = new MapperConfiguration(cfg => {
				cfg.AllowNullCollections = true;
				cfg.AddProfile(new MappingProfile());
			});

			var mapper = config.CreateMapper();
			var result = mapper.Map<IEnumerable<HistoryDTO>>(obj);
			return result;
		}

		public IList<History> MapList(IList<HistoryDTO> obj)
		{
			var config = new MapperConfiguration(cfg => {
				cfg.AllowNullCollections = true;
				cfg.AddProfile(new MappingProfile());
			});
			var mapper = config.CreateMapper();
			var result = mapper.Map<IList<History>>(obj);
			return result;
		}

		public IList<HistoryDTO> MapList(IList<History> obj)
		{
			var config = new MapperConfiguration(cfg => {
				cfg.AllowNullCollections = true;
				cfg.AddProfile(new MappingProfile());
			});
			var mapper = config.CreateMapper();
			var result = mapper.Map<IList<HistoryDTO>>(obj);
			return result;
		}

		public ICollection<History> MapList(ICollection<HistoryDTO> obj)
		{
			var config = new MapperConfiguration(cfg => {
				cfg.AllowNullCollections = true;
				cfg.AddProfile(new MappingProfile());
			});
			var mapper = config.CreateMapper();
			var result = mapper.Map<ICollection<History>>(obj);
			return result;
		}

		public ICollection<HistoryDTO> MapList(ICollection<History> obj)
		{
			var config = new MapperConfiguration(cfg => {
				cfg.AllowNullCollections = true;
				cfg.AddProfile(new MappingProfile());
			});
			var mapper = config.CreateMapper();
			var result = mapper.Map<ICollection<HistoryDTO>>(obj);
			return result;
		}
	}
}
