using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Vocabulary
{
    public string vocabulary;
    public string mean;
    public Sprite image;
    public Vocabulary(string _vocabulary, string _mean, Sprite _image)
    {
        this.vocabulary = _vocabulary;
        this.mean = _mean;
        this.image = _image;
    }
}
public class VocabularyManager : MonoBehaviour
{
    public static VocabularyManager instance { get; private set; }
    private CsvReader csvReader;
    [SerializeField] private List<Vocabulary> easyVocabularies = new List<Vocabulary>();
    [SerializeField] private List<Vocabulary> mediumVocabularies = new List<Vocabulary>();
    [SerializeField] private List<Vocabulary> hardVocabularies = new List<Vocabulary>();
    public Dictionary<string, string> easyVocabulary_mean { get; private set; } = new Dictionary<string, string>();
    public Dictionary<string, string> mediumVocabulary_mean { get; private set; } = new Dictionary<string, string>();
    public Dictionary<string, string> hardVocabulary_mean { get; private set; } = new Dictionary<string, string>();
    private List<Sprite> easyVocaImages;
    private List<Sprite> mediumVocaImages;
    private List<Sprite> hardVocaImages;
    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        else
            instance = this;
        csvReader = GetComponent<CsvReader>();
    }
    private void Start()
    {
        LoadVocabularyMean();
        LoadVocabularyImage();
        LoadVocabularies();
    }
    public Vocabulary GetRandomEasyVocabulary()
    {
        int rd = UnityEngine.Random.Range(0, easyVocabularies.Count);
        return easyVocabularies[rd];
    }
    public Vocabulary GetRandomMediumVocabulary()
    {
        int rd = UnityEngine.Random.Range(0, mediumVocabularies.Count);
        return mediumVocabularies[rd];
    }
    public Vocabulary GetRandomHardVocabulary()
    {
        int rd = UnityEngine.Random.Range(0, hardVocabularies.Count);
        return hardVocabularies[rd];
    }
    private void LoadVocabularies()
    {
        int cnt = 0;
        foreach (var item in easyVocabulary_mean)
        {
            easyVocabularies.Add(new Vocabulary(item.Key, item.Value, easyVocaImages[cnt++]));
        }
        cnt = 0;
        foreach (var item in mediumVocabulary_mean)
        {
            mediumVocabularies.Add(new Vocabulary(item.Key, item.Value, mediumVocaImages[cnt++]));
        }
        cnt = 0;
        foreach (var item in hardVocabulary_mean)
        {
            hardVocabularies.Add(new Vocabulary(item.Key, item.Value, hardVocaImages[cnt++]));
        }
    }

    private void LoadVocabularyImage()
    {
        easyVocaImages = Resources.Load<Vocabulary_imageSO>("Vocabulary_image").easyVocaImages;
        mediumVocaImages = Resources.Load<Vocabulary_imageSO>("Vocabulary_image").mediumVocaImages;
        hardVocaImages = Resources.Load<Vocabulary_imageSO>("Vocabulary_image").hardVocaImages;
    }
    private void LoadVocabularyMean()
    {
        int cnt = 0;
        foreach (string s in csvReader.easyVocabularyData)
        {
            if (cnt == 0 || cnt % 2 == 0)
            {
                if(!easyVocabulary_mean.ContainsKey(s))
                    easyVocabulary_mean.Add(s, csvReader.easyVocabularyData[cnt + 1]);
            }
            cnt++;
        }
        cnt = 0;
        foreach (string s in csvReader.mediumVocabularyData)
        {
            if (cnt == 0 || cnt % 2 == 0)
                mediumVocabulary_mean.Add(s, csvReader.mediumVocabularyData[cnt + 1]);
            cnt++;
        }
        cnt = 0;
        foreach (string s in csvReader.hardVocabularyData)
        {
            if (cnt == 0 || cnt % 2 == 0)
                hardVocabulary_mean.Add(s, csvReader.hardVocabularyData[cnt + 1]);
            cnt++;
        }
    }
}
