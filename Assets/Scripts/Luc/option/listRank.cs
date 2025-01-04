using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using Firebase.Database;
using Firebase;
using UnityEngine.InputSystem.HID;
using System.Linq;

public class listRank : MonoBehaviour
{
    public Transform leaderboardContainer;
    public GameObject leaderboardTemple;
    private DatabaseReference database;
    public List<classPlayerData> topPlayers = new List<classPlayerData>();
    [SerializeField] private AudioSource SfxButton;

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
            entry.transform.Find("Sum").GetComponent<TextMeshProUGUI>().text = player.scoreSum.ToString();
            Debug.Log("da them");

        }
    }
    public void LoadLeaderboard()
    {
        topPlayers.Clear();
        database.Child("user").OrderByChild("ScoreSum").LimitToLast(5).GetValueAsync().ContinueWith(task =>
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
                        scoreSum = int.Parse(userSnapshot.Child("ScoreSum").Value.ToString())
                    };

                    topPlayers.Add(playerData); 

                }
              
                //Debug.Log($"Loaded {topPlayers.Count} players.");
                foreach (var player in topPlayers)
                {
                    Debug.Log($"Player: {player.name}, ScoreSum: {player.scoreSum}");
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
    private IEnumerator DelayedCreateAccount(float delay)
    {

        yield return new WaitForSeconds(delay);
        creatAcount();
    }
   
}
