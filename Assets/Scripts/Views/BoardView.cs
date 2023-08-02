using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PositionEventArgs : EventArgs
{
    public Position Position { get; }

    public PositionEventArgs(Position position)
    {
        Position = position;
    }
}

public class BoardView : MonoBehaviour
{
    public event EventHandler<PositionEventArgs> PositionClicked;

    private Dictionary<Position, PositionView> _positionViews = new Dictionary<Position, PositionView>();
    private List<Position> _activePositions = new List<Position>();


    public List<Position> ActivePositions
    {
        set
        {
            foreach(var position in _activePositions)
                _positionViews[position].DeActivate();

            if (value == null)
                _activePositions.Clear();
            else
                _activePositions = value;

            foreach(var position in value)
                _positionViews[position].Activate();
        }
    }

    private void OnEnable()
    {
        var views = GetComponentsInChildren<PositionView>();

        foreach(var view in views)
        {
            _positionViews.Add(view.TilePosition, view);
        }
    }

    internal void ChildClicked(PositionView positionView)
    {
        OnClicked(new PositionEventArgs(positionView.TilePosition));
    }

    private void OnClicked(PositionEventArgs positionEventArgs)
    {
        var handler = PositionClicked;
        handler?.Invoke(this, positionEventArgs);
    }
}
