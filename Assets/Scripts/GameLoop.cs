using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private Board _board;

    void Start()
    {
        var boardView = FindObjectOfType<BoardView>();
        boardView.PositionClicked += OnPositionClicked;

        _board = new Board(TileHelper.Distance);

        _board.PieceMoved += (s, e) => e.Piece.Move(TileHelper.TileToWorldPosition(e.toPosition));
        _board.PieceTaken += (s, e) => e.Piece.Take();
        _board.PiecePlaced += (s, e) => e.Piece.Place(TileHelper.TileToWorldPosition(e.toPosition));

        var pieceViews = FindObjectsOfType<PieceView>();
        foreach (var pieceView in pieceViews)
            _board.Place(TileHelper.WorldToTilePosition(pieceView.WorldPosition), pieceView);
    }

    private void OnPositionClicked(object sender, PositionEventArgs e)
    {
        if (_board.TryGetPiece(e.Position, out var piece))
        {
            var toPosition = new Position(e.Position.q, e.Position.r + 1);
            _board.Move(e.Position, toPosition);
        }

        Debug.Log(TileHelper.TileToWorldPosition(e.Position));
    }
}
