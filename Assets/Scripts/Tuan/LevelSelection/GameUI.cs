using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LevelUnlockSystem
{

    public class GameUI : MonoBehaviour
    {
        // [SerializeField] private Image[] starsArray;
        // [SerializeField] private Color lockColor, unlockColor;
        // [SerializeField]
        // private GameObject gameOverPanel;



        public void GameOver(int starCount)
        {
            // Debug.Log("StarCount" + starCount);
            // SetStar(starCount);
            if (starCount > 0)
            {
                LevelSystemManager.Instance.LevelComplete(starCount);
            }
            
            // gameOverPanel.SetActive(true);
        }

        // public void SetStar(int starAchieved)
        // {
        //     Debug.Log("Setstar" + starAchieved);
        //     for (int i = 0; i < starsArray.Length; i++)
        //     {

        //         if (i < starAchieved)
        //         {
        //             starsArray[i].color = unlockColor;
        //         }
        //         else
        //         {
        //             starsArray[i].color = lockColor;
        //         }
        //     }
        //     Canvas.ForceUpdateCanvases();
        // }
    }
}