using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Engine Engine;

    [SerializeField]
    private CardType cardType;
    public CardType CardType => cardType;

    private int tileLayer = 6;
    private GameObject _copy;

    private Position _selectedPosition;
    public State State;

    private List<Position> _validPositions = new List<Position>();
    private List<List<Position>> _validPositionGroups = new List<List<Position>>();

    [HideInInspector]
    public bool IsPlayed = false;
    [HideInInspector]
    public bool IsHolding = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _copy = Instantiate(gameObject, transform.parent);
        _validPositions = new List<Position>();
        _validPositionGroups = new List<List<Position>>();
        
        if (cardType == CardType.Teleport)
        {
            _validPositions = Engine.GetValidPositions(cardType);
        }
        else if (cardType == CardType.Laser || cardType == CardType.Slash || cardType == CardType.Push)
        {
            _validPositionGroups = Engine.GetValidPositionGroups(cardType);
            validPositionGroupsToValidPositions();
        }
        else if (cardType == CardType.Blitz)
        {
            _validPositions = Engine.GetValidPositions(cardType);
            //Debug.Log(_validPositions.Count);
        }
        
        IsHolding = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _copy.transform.position = eventData.position;
        var mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(mousePos, out var hit) && hit.collider.gameObject.layer == tileLayer)
        {
            var view = hit.transform.gameObject.GetComponent<PositionView>();
            Engine.SetHighlights(view.TilePosition, CardType, _validPositions, _validPositionGroups);
        }
        else
            Engine.SetActivePositions(new List<Position>());
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(mousePos, out var hit) && hit.collider.gameObject.layer == tileLayer && IsDraggingOverValidTiles(hit))
        {
            var view = hit.transform.gameObject.GetComponent<PositionView>();
            _selectedPosition = view.TilePosition;
            Destroy(_copy);
            IsPlayed = true;
            view.OnPointerClick(eventData);

            //call event to invoke OnExit from playerstate
        }
        else if (hit.collider.gameObject.layer == tileLayer && cardType == CardType.Blitz)
        {
            var view = hit.transform.gameObject.GetComponent<PositionView>();
            _selectedPosition = view.TilePosition;
            Destroy(_copy);
            IsPlayed = true;
            view.OnPointerClick(eventData);

            //call event to invoke OnExit from playerstate
        }
        else
            Destroy(_copy);

        Engine.SetActivePositions(new List<Position>());

        IsHolding = false;
    }

    private bool IsDraggingOverValidTiles(RaycastHit hit)
    {
        if (_validPositions.Contains(hit.transform.gameObject.GetComponent<PositionView>().TilePosition))
            return true;

        return false;
    }

    private void validPositionGroupsToValidPositions()
    {
        foreach (var validPositions in _validPositionGroups)
        {
            foreach(var validPosition in validPositions)
            {
                _validPositions.Add(validPosition);
            }
        }
    }

    public EventHandler PlayClicked;

    public void Play() => OnPlayClicked(EventArgs.Empty);

    private void OnPlayClicked(EventArgs e)
    {
        var handler = PlayClicked;
        handler?.Invoke(this, e);
    }
}
