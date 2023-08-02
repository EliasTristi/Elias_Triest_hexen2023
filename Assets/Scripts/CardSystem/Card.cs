using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private CardType cardType;
    public CardType CardType => cardType;

    private int tileLayer = 6;

    private GameObject _copy;

    private Position selectedPosition;

    [HideInInspector]
    public bool IsPlayed = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _copy = Instantiate(gameObject, transform.parent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _copy.transform.position = eventData.position;
        var mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(mousePos, out var hit) && hit.collider.gameObject.layer == tileLayer)
        {
            var view = hit.transform.gameObject.GetComponent<PositionView>();
            // code highlight
            selectedPosition = view.TilePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(mousePos, out var hit) && hit.collider.gameObject.layer == tileLayer)
        {
            var view = hit.transform.gameObject.GetComponent<PositionView>();
            selectedPosition = view.TilePosition;
            
            Destroy(_copy);
            
            IsPlayed = true;
            view.OnPointerClick(eventData);

        }
        else
            Destroy(_copy);
    }
}
