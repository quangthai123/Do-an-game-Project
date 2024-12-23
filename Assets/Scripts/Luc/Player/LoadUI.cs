using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static Cinemachine.DocumentationSortingAttribute;

public class LoadUI : MonoBehaviour
{

    public Slider expBar;
    public TextMeshProUGUI level;
    public playerData _playerData;
    public expPlayer _expPlayer;

    void Update()
    {
        updateUI();
    }
    public void updateUI()
    {
        expBar.value = _playerData.GetExpValue();
        level.text = "Lv." + _playerData.GetLevel().ToString();
    }
}
