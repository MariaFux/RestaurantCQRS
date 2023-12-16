﻿namespace Domain.Exceptions
{
    public class RecipeIngredientNotValidException : NotValidException
    {
        internal RecipeIngredientNotValidException() { }
        internal RecipeIngredientNotValidException(string message) : base(message) { }
        internal RecipeIngredientNotValidException(string message, Exception inner) : base(message, inner) { }
    }
}
