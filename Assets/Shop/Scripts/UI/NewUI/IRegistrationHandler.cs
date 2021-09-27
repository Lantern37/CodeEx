public interface IRegistrationHandler
{
    public void SetMail(string mail);
    public void SetPassword(string password);
    public void SetDataFromInputs(string mail, string password);
    public void BadRegistrationHandle();
}
