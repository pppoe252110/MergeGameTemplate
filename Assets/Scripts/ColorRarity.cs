using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorRarity", menuName = "ScriptableObjects/ColorRarity", order = 1)]
public class ColorRarity : BaseRarity
{
    public ColorRarityData[] rarity;

    public override void CellMerged(MergeCell from, MergeCell to, int level)
    {
        from.SetBackgroundColor(Color.clear);
        to.SetBackgroundColor(rarity[(int)Mathf.Repeat(level, rarity.Length)].color);
    }

    public override void CellUpdated(MergeCell cell, int level)
    {
        if (level < 0)
        {
            cell.SetBackgroundColor(Color.clear);
            return;
        }
        cell.SetBackgroundColor(rarity[(int)Mathf.Repeat(level, rarity.Length)].color);
    }

    [System.Serializable]
    public class ColorRarityData
    {
        public string name;
        public Color color = Color.white;
    }
}
