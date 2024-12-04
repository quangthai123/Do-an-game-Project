using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerfectWordHolder : MonoBehaviour
{
    public static PerfectWordHolder instance { get; private set; }
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
            transform.GetChild(i).GetComponent<Animator>().ResetTrigger("CanDance");
        }
    }
    public void ReturnAllAlphabetToHolder()
    {
        foreach(Transform slot in transform)
        {
            if(slot.Find("Alphabet"))
            {
                slot.Find("Alphabet").GetComponent<DraggableItem>().ReturnHolderForNextLv();
            }
        }
    }
    public bool CheckPerfectWordWhenFullSlot()
    {
        foreach(Transform slot in transform)
        {
            if (!slot.gameObject.activeInHierarchy)
                continue;
            if (slot.Find("Alphabet").Find("RedFx").gameObject.activeInHierarchy)          
                return false;       
        }
        return true;
    }
    public void CreateFx()
    {
        foreach (Transform slot in transform)
        {
            slot.GetComponent<Animator>().SetTrigger("CanDance");
        }
    }
}
