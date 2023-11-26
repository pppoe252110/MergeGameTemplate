using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeLogic : MonoBehaviour
{
    [SerializeField] private MergeData _mergeData;

    public MergeInfo Merge(MergeCell from, MergeCell to)
    {
        if (from == null || to == null)
            return MergeFailed();

        if (from.MergeItem == null && to.MergeItem == null) 
            return MergeFailed();


        if (from == to) 
            return MergeFailed();

        if (from.MergeItem != to.MergeItem)
        {
            if(from.MergeItem != null && to.MergeItem == null)
            {
                var level = from.Level;

                to.Level = level;
                from.Level = -1;

                to.MergeItem = from.MergeItem;
                from.MergeItem = null;

                from.UpdateCell();
                to.UpdateCell();

                _mergeData.rarity.CellMerged(from, to, level);
            }
            return MergeFailed();
        }

        if (from.Level != to.Level)
            return MergeFailed();

        var oldItem = to.MergeItem;
        var nextItem = GetNextItem(oldItem, from.Level);

        from.MergeItem = null;
        from.Level = -1;

        to.Level = nextItem.level;
        to.MergeItem = nextItem.mergeItem;

        from.UpdateCell();
        to.UpdateCell();

        SoundEffectsManager.PlaySound("pop1");

        return new MergeInfo
        {
            Merged = true,
            oldItem = oldItem,
            newItem = nextItem.mergeItem
        };

    }

    private MergeInfo MergeFailed()
    {
        SoundEffectsManager.PlaySound("pop3");

        return new MergeInfo
        {
            Merged = false
        };
    }

    private NextItemInfo GetNextItem(MergeItem item, int level)
    {
        var oldIndex = Array.IndexOf(_mergeData.items, item);

        var nextIndex = oldIndex + 1;

        var newLevel = level;

        if(nextIndex>=_mergeData.items.Length)
        {
            newLevel++;
            nextIndex -= _mergeData.items.Length;
        }
        return new NextItemInfo
        {
            level = newLevel,
            mergeItem = _mergeData.items[nextIndex]
        };
    }

    public MergeData GetMergeData()
    {
        return _mergeData;
    }
}

public class MergeInfo
{
    public bool Merged;
    public MergeItem oldItem;
    public MergeItem newItem;
}
public class NextItemInfo
{
    public int level;
    public MergeItem mergeItem;
}

