using AutoMapper;
using BIVALE.BLL.Interfaces;
using BIVALE.DAL.Models;
using BIVALE.DTO;
using System.Collections.Generic;

namespace BIVALE.BLL.Mapping
{
    public class NodePermissionMapper : IMapper<NodePermissionDTO, NodePermission>
    {
        public NodePermission Map(NodePermissionDTO obj)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            NodePermission target = mapper.Map<NodePermission>(obj);
            return target;
        }

        public NodePermissionDTO Map(NodePermission obj)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var result = mapper.Map<NodePermissionDTO>(obj);
            return result;
        }

        public IEnumerable<NodePermission> MapList(IEnumerable<NodePermissionDTO> obj)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var result = mapper.Map<IEnumerable<NodePermission>>(obj);
            return result;
        }

        public IEnumerable<NodePermissionDTO> MapList(IEnumerable<NodePermission> obj)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AddProfile(new MappingProfile());
            });

            var mapper = config.CreateMapper();
            var result = mapper.Map<IEnumerable<NodePermissionDTO>>(obj);
            return result;
        }

        public IList<NodePermission> MapList(IList<NodePermissionDTO> obj)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var result = mapper.Map<IList<NodePermission>>(obj);
            return result;
        }

        public IList<NodePermissionDTO> MapList(IList<NodePermission> obj)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var result = mapper.Map<IList<NodePermissionDTO>>(obj);
            return result;
        }

        public ICollection<NodePermission> MapList(ICollection<NodePermissionDTO> obj)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var result = mapper.Map<ICollection<NodePermission>>(obj);
            return result;
        }

        public ICollection<NodePermissionDTO> MapList(ICollection<NodePermission> obj)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var result = mapper.Map<ICollection<NodePermissionDTO>>(obj);
            return result;
        }
    }
}
