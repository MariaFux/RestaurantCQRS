using Application.Contracts.Menus.Responses;
using Application.Contracts.Recipes.Responses;
using AutoMapper;
using Domain.Aggregates.RecipeAggregate;

namespace Restaurant.MappingProfiles
{
    public class RecipeMappings : Profile
    {
        public RecipeMappings()
        {
            CreateMap<Recipe, RecipeResponse>();
            CreateMap<RecipeIngredient, RecipeIngredientResponse>();
            CreateMap<Recipe, RecipeContent>()
                .ForMember(dest => dest.Name, opt
                    => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.TextContent, opt
                    => opt.MapFrom(src => src.TextContent))
                .ForMember(dest => dest.Ingredients, opt
                    => opt.MapFrom(src => src.Ingredients.Select(x => x.Name)));
        }
    }
}
