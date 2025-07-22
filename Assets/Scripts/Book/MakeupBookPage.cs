using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeupBookPage : MonoBehaviour
{
    public event Action<MakeupItemView> ItemViewClick;

    [SerializeField] private MakeupItemViewFactorySO _makeupItemViewFactory;
    [SerializeField] private GridLayoutGroup _eyeShadowItemViewContainer;
    [SerializeField] private GridLayoutGroup _lipstickItemViewContainer;
    [SerializeField] private GridLayoutGroup _blushItemViewContainer;

    private List<MakeupItemView> _makeupItems = new List<MakeupItemView>();

    public void ShowItems(IEnumerable<MakeupItemSO> items)
    {
        Clear();
        _lipstickItemViewContainer.enabled = true;

        foreach (MakeupItemSO item in items)
        {
            MakeupItemView spawnedItem = _makeupItemViewFactory.Get(item, _eyeShadowItemViewContainer.transform, 
                _lipstickItemViewContainer.transform, _blushItemViewContainer.transform);

            spawnedItem.Click += OnItemViewClick;
            _makeupItems.Add(spawnedItem);
        }
        StartCoroutine(DisableLipstickGrid());
    }

    private IEnumerator DisableLipstickGrid()
    {
        yield return null;
        _lipstickItemViewContainer.enabled = false;
    }

    private void Clear()
    {
        foreach (MakeupItemView item in _makeupItems)
        {
            item.Click -= OnItemViewClick;
            GameObject.Destroy(item.gameObject);
        }

        _makeupItems.Clear();
    }

    private void OnItemViewClick(MakeupItemView itemView)
    {
        ItemViewClick?.Invoke(itemView);
    }
}