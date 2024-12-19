
public class User 
{
    public string Username;
    public string Password;
    public int Exp;
    public int MaxExp;
    public int Level;

    public User(string name, string password, int exp, int maxExp, int level)
    {
        this.Username = name;
        this.Password = password;
        this.Exp = exp;
        this.MaxExp = maxExp;
        this.Level = level;
    }
}
