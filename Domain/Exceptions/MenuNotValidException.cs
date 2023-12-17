namespace Domain.Exceptions
{
    public class MenuNotValidException : NotValidException
    {
        internal MenuNotValidException() { }
        internal MenuNotValidException(string message) : base(message) { }
        internal MenuNotValidException(string message, Exception inner) : base(message, inner) { }
    }
}
