namespace notes.Authentication
{
    public interface IAuthenticator
    {
        bool IsUserAuthenticated() // TODO: проверять userId
            ;
    }
}