using AutoMapper;
using Domain.Aggregates.RecipeAggregate;
using Restaurant.Contracts.Menus.Responses;
using Restaurant.Contracts.Recipes.Responses;

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
