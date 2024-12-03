using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Vocabulary_audio", menuName = "ScriptableObject/Vocabulary_audio")]
public class WordsAudioSO : ScriptableObject
{
    public List<AudioClip> audioClips; 
}
