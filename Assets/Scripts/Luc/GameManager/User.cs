
public class User 
{
    public string Username;
    public string Password;
    public int ScoreGame1;
    public int ScoreGame2;
    public int ScoreGame3;
    public int ScoreSum;
    public User(string name, string password, int scoregame1,int scoregame2,int scoregame3,int scoresum)
    {
        this.Username = name;
        this.Password = password;
        this.ScoreGame1 = scoregame1;
        this.ScoreGame2 = scoregame2;
        this.ScoreGame3 = scoregame3;
        this.ScoreSum = scoresum;
    }
}
