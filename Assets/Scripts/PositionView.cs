using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PositionView : MonoBehaviour, IPointerClickHandler
{
    public EventHandler Clicked;

    public Vector3 _tilePosition;

    private void OnEnable()
    {
        _tilePosition = transform.position;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClicked(EventArgs.Empty);
    }

    protected virtual void OnClicked(EventArgs eventArgs)
    {
        Debug.Log(_tilePosition);
        var handler = Clicked;
        handler?.Invoke(this, eventArgs);
    }
}
