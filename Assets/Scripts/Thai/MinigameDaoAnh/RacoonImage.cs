using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RacoonImage : MonoBehaviour
{
    [SerializeField] private Image vocaImage;
    public void SetImage(Sprite s) => vocaImage.sprite = s;
}
