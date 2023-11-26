using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeField : MonoBehaviour
{
    [SerializeField] private MergeCell[] _cells;
    [SerializeField] private MergeLogic _mergeLogic;

    private void Start()
    {
        foreach (MergeCell cell in _cells)
        {
            cell.Init(_mergeLogic);
        }
    }

    public MergeCell AddMergeItemToCell(MergeItem item, int cellIndex)
    {
        _cells[cellIndex].MergeItem = item;
        _cells[cellIndex].Level = 0;
        _cells[cellIndex].UpdateCell();
        return _cells[cellIndex];
    }

    public MergeCell[] GetCells()
    {
        return _cells;
    }

    public MergeLogic GetMergeLogic()
    {
        return _mergeLogic;
    }
}
