using AutoMapper;
using NetCoreWithReactAndBasicAuth.Web.Models;

namespace NetCoreWithReactAndBasicAuth.Web
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Core.Models.User, UserViewModel>().ReverseMap();
            CreateMap<PostViewModel, Core.Models.Post>();
            CreateMap<Core.Models.Post, PostViewModel>()
                .ForPath(p => p.User.Id, opt => opt.MapFrom(src => src.User.Id))
                .ForPath(p => p.User.FirstName, opt => opt.MapFrom(src => ""))
                .ForPath(p => p.User.LastName, opt => opt.MapFrom(src => ""))
                .ForPath(p => p.User.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForPath(p => p.User.Password, opt => opt.MapFrom(src => ""));
        }
    }
}
