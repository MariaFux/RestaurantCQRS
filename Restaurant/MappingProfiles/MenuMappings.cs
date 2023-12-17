using AutoMapper;
using Domain.Aggregates.MenuAggregate;
using Restaurant.Contracts.Menus.Responses;

namespace Restaurant.MappingProfiles
{
    public class MenuMappings : Profile
    {
        public MenuMappings()
        {
            CreateMap<Menu, MenuResponse>();
            CreateMap<Menu, MenuRecipeResponse>()
                .ForMember(dest => dest.MenuName, opt
                    => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.RecipeAuthor, opt
                    => opt.MapFrom(src => src.UserProfile))
                .ForMember(dest => dest.RecipeContent, opt
                    => opt.MapFrom(src => src.Recipes));
        }
    }
}
