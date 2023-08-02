using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PositionView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private UnityEvent OnActivate;
    [SerializeField]
    private UnityEvent OnDeactivate;

    public Position TilePosition => TileHelper.WorldToTilePosition(transform.position);

    private BoardView _parent;

    private void Start()
    {
        _parent = GetComponentInParent<BoardView>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _parent.ChildClicked(this);
    }

    internal void Activate() => OnActivate?.Invoke(); 
    internal void DeActivate() => OnDeactivate?.Invoke();
}
