using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRarity : ScriptableObject
{
    public abstract void CellMerged(MergeCell from, MergeCell to, int level);
    public abstract void CellUpdated(MergeCell cell, int level);
}
