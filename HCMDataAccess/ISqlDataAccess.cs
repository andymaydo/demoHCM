namespace HCMDataAccess
{
    public interface ISqlDataAccess
    {
        string ConnStrName { get; set; }
        string GetConnStrName();
    }
}