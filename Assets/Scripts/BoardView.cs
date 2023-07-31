using System;
using System.Collections;
using System.Collections.Generic;
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
    //private Dictionary<Position, PositionView> _positions = new Dictionary<Position, PositionView>();
    //private List<Position> _activePositions = new List<Position>();

    public event EventHandler<PositionEventArgs> PositionClicked;

    private void OnEnable()
    {
        var positionViews = GetComponentsInChildren<PositionView>();

        foreach (var positionView in positionViews)
        {
            //_positions[TileHelper.WorldToTilePosition(positionView.transform.position)] = positionView;
            positionView.Clicked += OnClicked;
        }
    }

    private void OnClicked(object sender, EventArgs e)
    {
        if (sender is PositionView view)
        {
            OnClicked(new PositionEventArgs(view.TilePosition));
        }
    }

    private void OnClicked(PositionEventArgs positionEventArgs)
    {
        var handler = PositionClicked;
        handler?.Invoke(this, positionEventArgs);
    }
}
