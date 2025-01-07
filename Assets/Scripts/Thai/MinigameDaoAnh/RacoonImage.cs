using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RacoonImage : MonoBehaviour
{
    [SerializeField] private Image vocaImage;
    private Sprite vocaImageOriginal;
    public void SetImage(Sprite s) 
    {
        vocaImage.sprite = s;
        vocaImageOriginal = vocaImage.sprite;
    }
    public Image GetImage() => vocaImage;
    public void SetToAnswerImageOnPowerUp(Sprite s) => vocaImage.sprite = s;
    public void SetBackToOriginalImage() => vocaImage.sprite = vocaImageOriginal;
}
