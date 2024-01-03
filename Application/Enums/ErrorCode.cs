namespace Application.Enums
{
    public enum ErrorCode
    {
        BadRequest = 400,
        NotFound = 404,
        ServerError = 500,

        //Validation errors in the range 100 - 199
        ValidationError = 101,

        //Infrastructure errors in the range 200 - 299
        IdentityUserAlreadyExists = 201,
        IdentityCreationFailed = 202,
        IdentityUserDoesNotExist = 203,
        IncorrectPassword = 204,

        //Application errors in the range 300 - 399
        AddRecipeToMenuNotPossible = 300,
        MenuDeleteNotPossible = 301,

        UnknownError = 999
    }
}
