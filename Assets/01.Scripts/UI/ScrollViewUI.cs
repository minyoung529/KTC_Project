using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class ScrollViewUI<T> where T : VisualElement
{
    private ScrollView itemViewList;

    private List<T> items = new List<T>();

    private int selectedIdx = -1;

    #region PROPERTY
    public int SelectedIdx => selectedIdx;
    public IReadOnlyList<T> Items => items;
    public Action OnSelectIdx { get; set; }
    #endregion

    public T this[int i]
    {
        get { return items[i]; }
    }

    public void Initialize(ScrollView itemViewList)
    {
        this.itemViewList = itemViewList;
    }

    public T Add(TemplateContainer newObj)
    {
        T ui = newObj.Q<T>();
        items.Add(ui);
        itemViewList.Add(ui);

        if (ui is Button button)
        {
            button.clicked += () => SetSelectedIndex(ui);
        }

        return ui;
    }

    public void Remove(int index)
    {
        itemViewList.Remove(items[index]);
        items.RemoveAt(index);
    }

    public void Remove(T ui)
    {
        Remove(IndexOf(ui));
    }

    public T Create(VisualTreeAsset prefab)
    {
        return Add(prefab.CloneTree());
    }

    public int IndexOf(T item)
    {
        return items.IndexOf(item);
    }

    private void SetSelectedIndex(T obj)
    {
        selectedIdx = IndexOf(obj);
        OnSelectIdx?.Invoke();
    }

    public void Select(int idx)
    {
        selectedIdx = idx;
        OnSelectIdx?.Invoke();
    }
}
