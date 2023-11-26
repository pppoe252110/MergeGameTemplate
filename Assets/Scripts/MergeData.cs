using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MergeData", menuName = "ScriptableObjects/MergeData", order = 1)]
public class MergeData : ScriptableObject
{
    public MergeItem[] items;
    public BaseRarity rarity;
}