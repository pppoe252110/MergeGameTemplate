using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MergeFieldAdder : MonoBehaviour
{
    [SerializeField] private MergeField _mergeField;
    [SerializeField] private MergeItem _mergeItem;
    [SerializeField] private int _spawnLevel = 0;

    void Update()
    {
        if (Input.GetKey(KeyCode.J))
        {
            SpawnCell();
        }
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            SpawnCell();
        }

        if (Input.GetKey(KeyCode.H))
        {
            AutoMerge();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            AutoMerge();
        }
    }

    public void AutoMerge()
    {
        var dublicates = _mergeField.GetCells().GroupBy(i => new { i.Level, i.MergeItem })
          .Where(g => g != null && g.Count() > 1)
          .Select(g => g.Key);

        var cells = dublicates.Where(s => s != null);
        if (cells.Count() > 0)
        {
            var empty = cells.Where(s => s.MergeItem != null && s.Level >= 0);
            if (empty.Count() > 0)
            {
                var a = empty.First();

                var targets = _mergeField.GetCells().Where(s => s.Level == a.Level && s.MergeItem == a.MergeItem).ToArray();
                if (targets.Length > 1)
                    _mergeField.GetMergeLogic().Merge(targets[0], targets[1]);
            }
        }

    }

    public void SpawnCell()
    {
        var cells = _mergeField.GetCells();
        var freeCells = cells.Where(s => s.MergeItem == null).ToArray();

        if (freeCells.Length <= 0)
            return;

        var cell = freeCells[Random.Range(0, freeCells.Length)];

        var mergeCell = _mergeField.AddMergeItemToCell(_mergeItem, System.Array.IndexOf(cells, cell));
        mergeCell.Level = _spawnLevel;
    }
}
