namespace Domain.Exceptions
{
    public class RecipeNotValidException : NotValidException
    {
        internal RecipeNotValidException() {}
        internal RecipeNotValidException(string message) : base(message) {}
        internal RecipeNotValidException(string message, Exception inner) : base(message, inner) {}
    }
}
