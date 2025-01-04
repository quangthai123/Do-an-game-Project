using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using Firebase.Database;
using Firebase;
using UnityEngine.InputSystem.HID;
using System.Linq;
using static UnityEngine.EventSystems.EventTrigger;

public class listRank : MonoBehaviour
{
    public Transform leaderboardContainer;
    public GameObject leaderboardTemple;
    public GameObject PlayerAccount;
    private DatabaseReference database;
    public List<classPlayerData> topPlayers = new List<classPlayerData>();
    [SerializeField] private AudioSource SfxButton;
    [SerializeField] private playerData _playerData;

    public void Awake()
    {
        database = FirebaseDatabase.GetInstance(FirebaseApp.DefaultInstance,"https://game-3d-english-study-default-rtdb.asia-southeast1.firebasedatabase.app/").RootReference;
       
        LoadLeaderboard();
        StartCoroutine(DelayedCreateAccount(0.5f));
    }

    public void creatAcount()
    {
        float temple = 130f;
        foreach (Transform child in leaderboardContainer)
        {
            Destroy(child.gameObject);
            Debug.Log("da xoa");

        }

        for (int i = 0; i < 5 && i < topPlayers.Count; i++)
        {
            GameObject entry = Instantiate(leaderboardTemple, leaderboardContainer);
            RectTransform entryRectTransform = entry.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(-14, -temple * i);
            entry.gameObject.SetActive(true);
            var player = topPlayers[i];
            int rank = i + 1;
            string rankString;
            rankString = rank.ToString();
            entry.transform.Find("STT").GetComponent<TextMeshProUGUI>().text = rankString;
            entry.transform.Find("UserName").GetComponent<TextMeshProUGUI>().text = player.name.ToString();
            entry.transform.Find("Game1").GetComponent<TextMeshProUGUI>().text = player.scoreGame1.ToString();
            entry.transform.Find("Game2").GetComponent<TextMeshProUGUI>().text = player.scoreGame2.ToString();
            entry.transform.Find("Game3").GetComponent<TextMeshProUGUI>().text = player.scoreGame3.ToString();
            entry.transform.Find("Game4").GetComponent<TextMeshProUGUI>().text = player.scoreGame4.ToString();
            entry.transform.Find("Game5").GetComponent<TextMeshProUGUI>().text = player.scoreGame5.ToString();
            entry.transform.Find("Game6").GetComponent<TextMeshProUGUI>().text = player.scoreGame6.ToString();
            entry.transform.Find("Sum").GetComponent<TextMeshProUGUI>().text = player.scoreSum.ToString();
            Debug.Log("da them");

        }
        int index = 0;
        foreach (var playerMain in topPlayers)
        {
            if (_playerData.GetName() == playerMain.name)
            {
                PlayerAccount.transform.Find("STT").GetComponent<TextMeshProUGUI>().text = (index+1).ToString();
                PlayerAccount.transform.Find("UserName").GetComponent<TextMeshProUGUI>().text = _playerData.GetName().ToString();
                PlayerAccount.transform.Find("Game1").GetComponent<TextMeshProUGUI>().text = _playerData.GetScoreGame1().ToString();
                PlayerAccount.transform.Find("Game2").GetComponent<TextMeshProUGUI>().text = _playerData.GetScoreGame2().ToString();
                PlayerAccount.transform.Find("Game3").GetComponent<TextMeshProUGUI>().text = _playerData.GetScoreGame3().ToString();
                PlayerAccount.transform.Find("Game4").GetComponent<TextMeshProUGUI>().text = _playerData.GetScoreGame4().ToString();
                PlayerAccount.transform.Find("Game5").GetComponent<TextMeshProUGUI>().text = _playerData.GetScoreGame5().ToString();
                PlayerAccount.transform.Find("Game6").GetComponent<TextMeshProUGUI>().text = _playerData.GetScoreGame6().ToString();
                PlayerAccount.transform.Find("Sum").GetComponent<TextMeshProUGUI>().text = _playerData.GetScoreSum().ToString();
                Debug.Log("da lay");

                break; 
            }
            index++; 
        }
    }
    public void LoadLeaderboard()
    {
        topPlayers.Clear();
        database.Child("user").OrderByChild("ScoreSum").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted && task.Result != null)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot userSnapshot in snapshot.Children)
                {

                    var playerData = new classPlayerData
                    {
                        name = userSnapshot.Child("Username").Value.ToString(),
                        scoreGame1 = int.Parse(userSnapshot.Child("ScoreGame1").Value.ToString()),
                        scoreGame2 = int.Parse(userSnapshot.Child("ScoreGame2").Value.ToString()),
                        scoreGame3 = int.Parse(userSnapshot.Child("ScoreGame3").Value.ToString()),
                        scoreGame4 = int.Parse(userSnapshot.Child("ScoreGame4").Value.ToString()),
                        scoreGame5 = int.Parse(userSnapshot.Child("ScoreGame5").Value.ToString()),
                        scoreGame6 = int.Parse(userSnapshot.Child("ScoreGame6").Value.ToString()),
                        scoreSum = int.Parse(userSnapshot.Child("ScoreSum").Value.ToString())
                    };

                    topPlayers.Add(playerData);

                }
                topPlayers.Sort((x, y) => y.GetScoreSum().CompareTo(x.GetScoreSum()));
            }
        });
        Debug.Log("Leaderboard UI updated.");


    }
    public void reload()
    {
        SfxButton.Play();
        LoadLeaderboard();

        StartCoroutine(DelayedCreateAccount(0.5f));
    }
    public void playerAccount()
    {
        
    }
    private IEnumerator DelayedCreateAccount(float delay)
    {

        yield return new WaitForSeconds(delay);
        creatAcount();
    }
   
}
