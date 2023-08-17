using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMovedEventArgs : EventArgs
{
    public Position fromPosition { get; }
    public Position toPosition { get; }
    public PieceView Piece { get; }

    public PieceMovedEventArgs(Position fromPosition, Position toPosition, PieceView piece)
    {
        this.fromPosition = fromPosition;
        this.toPosition = toPosition;
        Piece = piece;
    }
}

public class PieceTakenEventArgs : EventArgs
{
    public Position fromPosition { get; }
    public PieceView Piece { get; }

    public PieceTakenEventArgs(Position fromPosition, PieceView piece)
    {
        this.fromPosition = fromPosition;
        Piece = piece;
    }
}

public class PiecePlacedEventArgs : EventArgs
{
    public Position toPosition { get; }
    public PieceView Piece { get; }

    public PiecePlacedEventArgs(Position toPosition, PieceView piece)
    {
        this.toPosition = toPosition;
        Piece = piece;
    }
}

public class Board : MonoBehaviour
{
    public event EventHandler<PieceMovedEventArgs> PieceMoved;
    public event EventHandler<PieceTakenEventArgs> PieceTaken;
    public event EventHandler<PiecePlacedEventArgs> PiecePlaced;

    private Dictionary<Position, PieceView> _pieces = new Dictionary<Position, PieceView>();

    private int _distance;

    public Board(int distance) => _distance = distance;

    public bool TryGetPiece(Position position, out PieceView piece)
        => _pieces.TryGetValue(position, out piece);

    public bool IsValid(Position position) => (_distance >= HexHelper.AxialDistance(new Position(0, 0), position));


    public bool Move(Position fromPosition, Position toPosition)
    {
        if (!IsValid(toPosition))
            return false;
        if (_pieces.ContainsKey(toPosition))
            return false;
        if (!_pieces.TryGetValue(fromPosition, out var piece))
            return false;

        _pieces.Remove(fromPosition);
        _pieces[toPosition] = piece;

        OnPieceMoved(new PieceMovedEventArgs(fromPosition, toPosition, piece));

        return true;
    }

    public bool Take(Position fromPosition)
    {
        if (!IsValid(fromPosition))
            return false;
        if (!_pieces.ContainsKey(fromPosition))
            return false;
        if (!_pieces.TryGetValue(fromPosition, out var piece))
            return false;

        _pieces.Remove(fromPosition);

        OnPieceTaken(new PieceTakenEventArgs(fromPosition, piece));

        return true;
    }

    public bool Place(Position position, PieceView piece)
    {
        if (piece == null)
            return false;
        if (!IsValid(position))
            return false;
        if (_pieces.ContainsKey(position))
            return false;
        if (_pieces.ContainsValue(piece))
            return false;

        OnPiecePlaced(new PiecePlacedEventArgs(position, piece));
        _pieces[position] = piece;

        return true;
    }

    public void OnPieceMoved(PieceMovedEventArgs e)
    {
        var handler = PieceMoved;
        handler?.Invoke(this, e);
    }

    public void OnPieceTaken(PieceTakenEventArgs e)
    {
        var handler = PieceTaken;
        handler?.Invoke(this, e);
    }

    public void OnPiecePlaced(PiecePlacedEventArgs e)
    {
        var handler = PiecePlaced;
        handler?.Invoke(this, e);
    }
}
