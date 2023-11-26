using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "ScriptableObjects/SoundData", order = 1)]
public class SoundData : ScriptableObject
{
    public SoundEffect[] soundEffects;
}
[System.Serializable]
public class SoundEffect
{
    public string name;
    public AudioClip clip;
}
