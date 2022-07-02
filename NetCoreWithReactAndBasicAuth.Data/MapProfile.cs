using AutoMapper;
using NetCoreWithReactAndBasicAuth.Data.Entities;

namespace NetCoreWithReactAndBasicAuth.Data
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<User, Core.Models.User>().ReverseMap();
            CreateMap<Photo, Core.Models.Photo>();
            CreateMap<Core.Models.Photo, Photo>()
                .ForMember(
                    c => c.Post,
                    opt => opt.Ignore());
            CreateMap<Post, Core.Models.Post>();
            CreateMap<Core.Models.Post, Post>()
                .ForMember(
                    c => c.User,
                    opt => opt.Ignore());
        }
    }
}
