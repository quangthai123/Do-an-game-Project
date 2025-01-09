using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerLuyenTriNho : GameManager
{
    [SerializeField] private List<Vocabulary> currentVocabularies = new List<Vocabulary>();
    [SerializeField] private GameObject cardHolderEasy;
    [SerializeField] private GameObject cardHolderMedium;
    [SerializeField] private GameObject cardHolderHard;
    [SerializeField] private List<Image> vocaImageCards = new List<Image>();
    [SerializeField] private List<TextMeshProUGUI> vocaTextCards = new List<TextMeshProUGUI>();
    private List<int> vocaIndexsRemain = new List<int>();
    public override void OnSelectDifficulty(int diff)
    {
        base.OnSelectDifficulty(diff);
        SpawnRandomVocaPairs();
        //startTimer = true;
    }
    private void SpawnRandomVocaPairs()
    {
        switch (DifficultyManager.instance.Mode) 
        {
            case Difficulty.easy:
                ActiveHolderByDifficulty(1);
                for (int i=0; i<4; i++)
                {
                    currentVocabularies.Add(VocabularyManager.instance.GetRandomEasyVocabulary());
                }
                AssignVocaImages(8);
                break;
            case Difficulty.normal:
                ActiveHolderByDifficulty(2);
                for (int i = 0; i < 5; i++)
                {
                    currentVocabularies.Add(VocabularyManager.instance.GetRandomMediumVocabulary());
                }
                AssignVocaImages(10);
                break;
            case Difficulty.hard:
                ActiveHolderByDifficulty(3);
                for (int i = 0; i < 6; i++)
                {
                    currentVocabularies.Add(VocabularyManager.instance.GetRandomHardVocabulary());
                }
                AssignVocaImages(12);
                break;
        }
    }
    private void ActiveHolderByDifficulty(int diff)
    {
        if(diff == 1)
        {
            cardHolderEasy.SetActive(true);
            cardHolderMedium.SetActive(false);
            cardHolderHard.SetActive(false);
        } else if(diff == 2) 
        {
            cardHolderEasy.SetActive(false);
            cardHolderMedium.SetActive(true);
            cardHolderHard.SetActive(false);
        } else
        {
            cardHolderEasy.SetActive(false);
            cardHolderMedium.SetActive(false);
            cardHolderHard.SetActive(true);
        }
        GetVocasUI(diff);
    }
    private void GetVocasUI(int diff)
    {
        if (diff == 1)
        {
            for (int i = 0; i < 8; i++)
            {
                vocaImageCards.Add(cardHolderEasy.transform.GetChild(i).Find("Image").GetComponent<Image>());
                vocaTextCards.Add(cardHolderEasy.transform.GetChild(i).Find("Text").GetComponent<TextMeshProUGUI>());
                //cardHolderEasy.transform.GetChild(i).Find("Question").gameObject.SetActive(false);
            }
        }
        else if (diff == 2)
        {
            for (int i = 0; i < 10; i++)
            {
                vocaImageCards.Add(cardHolderMedium.transform.GetChild(i).Find("Image").GetComponent<Image>());
                vocaTextCards.Add(cardHolderMedium.transform.GetChild(i).Find("Text").GetComponent<TextMeshProUGUI>());
                //cardHolderMedium.transform.GetChild(i).Find("Question").gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < 12; i++)
            {
                vocaImageCards.Add(cardHolderHard.transform.GetChild(i).Find("Image").GetComponent<Image>());
                vocaTextCards.Add(cardHolderHard.transform.GetChild(i).Find("Text").GetComponent<TextMeshProUGUI>());
                //cardHolderHard.transform.GetChild(i).Find("Question").gameObject.SetActive(false);
            }
        }

    }
    private void AssignVocaImages(int num)
    {
        for (int i = 0; i < num; i++)
        {
            vocaIndexsRemain.Add(i);
        }
        for (int i = 0; i < num / 2; i++)
        {
            int rdId = Random.Range(0, vocaIndexsRemain.Count);
            vocaImageCards[vocaIndexsRemain[rdId]].sprite = currentVocabularies[i].image;
            vocaImageCards[vocaIndexsRemain[rdId]].gameObject.SetActive(true);
            vocaImageCards[vocaIndexsRemain[rdId]].transform.localScale = new Vector2(.9f, .8f);
            vocaTextCards[vocaIndexsRemain[rdId]].gameObject.SetActive(false);
            vocaIndexsRemain.Remove(vocaIndexsRemain[rdId]);
            rdId = Random.Range(0, vocaIndexsRemain.Count);
            vocaTextCards[vocaIndexsRemain[rdId]].text = currentVocabularies[i].vocabulary;
            vocaTextCards[vocaIndexsRemain[rdId]].gameObject.SetActive(true);
            vocaImageCards[vocaIndexsRemain[rdId]].gameObject.SetActive(false);
            vocaIndexsRemain.Remove(vocaIndexsRemain[rdId]);
        }
    }

}
