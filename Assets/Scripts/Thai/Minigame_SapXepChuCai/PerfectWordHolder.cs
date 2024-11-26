using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerfectWordHolder : MonoBehaviour
{
    public static PerfectWordHolder instance { get; private set; }
    private string currentWordPut;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }
    public void ActiveSlots()
    {
        foreach(Transform slot in transform)
        {
            slot.gameObject.SetActive(false);
        }
        for(int i=0; i<GameManager_SXChuCai.instance.currentWordLength; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    //public void CheckWrongAlphabetAndPassLv()
    //{
    //    int cnt = -1;
    //    foreach(Transform slot in transform)
    //    {
    //        cnt++;
    //        if (!slot.Find("Alphabet"))
    //            continue;
    //        char alphabet = slot.Find("Alphabet").GetComponent<Image>().sprite.name;
    //        if(!GameManager_SXChuCai.instance.CheckAlphabet(alphabet, cnt))
    //        {
    //            slot.Find("Alphabet").Find("RedFx").gameObject.SetActive(false);
    //        }
    //    }
    //}
}
