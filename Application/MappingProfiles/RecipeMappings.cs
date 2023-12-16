using Application.Contracts.Recipes.Responses;
using AutoMapper;
using Domain.Aggregates.RecipeAggregate;

namespace Application.MappingProfiles
{
    public class RecipeMappings : Profile
    {
        public RecipeMappings()
        {
            CreateMap<Recipe, RecipeResponse>();
            CreateMap<RecipeIngredient, RecipeIngredientResponse>();
        }
    }
}
