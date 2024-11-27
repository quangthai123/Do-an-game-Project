using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;
using static Cinemachine.DocumentationSortingAttribute;

[CreateAssetMenu(fileName = "playerData")]
public class playerData : ScriptableObject
{
    public Vector3 position;
    public int expValue;
    public int expMax;
    public int levelValue;

    public Vector3 GetPosition()
    {
        return position;
    }
    public int GetExpValue()
    {
        return expValue;
    }
    public int GetExpMax()
    {
        return expMax;
    }
    public int GetLevel()
    {
        return levelValue;
    }
}
