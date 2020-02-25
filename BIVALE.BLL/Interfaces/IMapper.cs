using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIVALE.BLL.Interfaces
{
	public interface IMapper<DTO, TEntity>
	{
		TEntity Map(DTO obj);

		DTO Map(TEntity obj);

		IList<TEntity> MapList(IList<DTO> obj);

		IList<DTO> MapList(IList<TEntity> obj);

		IEnumerable<TEntity> MapList(IEnumerable<DTO> obj);

		IEnumerable<DTO> MapList(IEnumerable<TEntity> obj);

		ICollection<TEntity> MapList(ICollection<DTO> obj);

		ICollection<DTO> MapList(ICollection<TEntity> obj);
	}
}
