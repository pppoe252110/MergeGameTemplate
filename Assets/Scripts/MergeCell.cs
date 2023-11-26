using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MergeCell : MonoBehaviour, IMergable, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public MergeItem MergeItem { get => _item; set => _item = value; }
    [SerializeField] private MergeItem _item;

    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _itemImage;

    public int Level { get => level; set => level = value; }
    [SerializeField] private int level = -1;

    private MergeLogic _logic;

    public void Init(MergeLogic logic)
    {
        _logic = logic;
        UpdateCell();
    }

    public void UpdateCell()
    {
        if (MergeItem != null)
        {
            _itemImage.sprite = MergeItem.sprite;
            _itemImage.color = Color.white;
        }
        else
        {
            _itemImage.sprite = null;

            _backgroundImage.color = Color.clear;
            _itemImage.color = Color.clear;
        }
        _logic.GetMergeData().rarity.CellUpdated(this, level);
    }

    public void SetBackgroundColor(Color color)
    {
        _backgroundImage.color = color;
    }

    private void ResetItemPosition()
    {
        _itemImage.rectTransform.localPosition = Vector3.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _itemImage.rectTransform.position = (Vector3)eventData.position+Vector3.back;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        MergeCell cell = null;
        foreach(var item in eventData.hovered)
        {
            if(item.TryGetComponent(out cell))
                break;
        }
        if (cell)
        {
            _logic.Merge(this, cell);
        }
        else
        {
            SoundEffectsManager.PlaySound("pop3");
        }
        ResetItemPosition();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!MergeItem)
            return;
        SoundEffectsManager.PlaySound("pop2");
        _itemImage.sprite = _itemImage.sprite;
    }
}
