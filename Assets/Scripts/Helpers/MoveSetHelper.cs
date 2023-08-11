using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSetHelper : MonoBehaviour
{
    private readonly Board _board;
    private readonly Position _position;
    private readonly Player _player;

    public delegate bool Validator(Board board, Position fromPosition, Position toPosition);

    private readonly List<Position> _validPositions = new List<Position>();

    public MoveSetHelper(Board board, Position position)
    {
        if (board.TryGetPiece(position, out var piece))

        _board = board;
        _position = position;
        _player = piece.Player;
    }

    public List<Position> CollectValidPositions()
    {
        return _validPositions;
    }

    //left
    public MoveSetHelper Left(int maxSteps = int.MaxValue, params Validator[] validators) 
        => Collect(-1, 0, maxSteps, validators);
    public MoveSetHelper LeftUp(int maxSteps = int.MaxValue, params Validator[] validators)
        => Collect(-1, 1, maxSteps, validators);
    public MoveSetHelper LeftDown(int maxSteps = int.MaxValue, params Validator[] validators)
        => Collect(0, -1, maxSteps, validators);

    //right
    public MoveSetHelper Right(int maxSteps = int.MaxValue, params Validator[] validators)
        => Collect(1, 0, maxSteps, validators);
    public MoveSetHelper RightUp(int maxSteps = int.MaxValue, params Validator[] validators)
        => Collect(0, 1, maxSteps, validators);
    public MoveSetHelper RightDown(int maxSteps = int.MaxValue, params Validator[] validators)
        => Collect(1, -1, maxSteps, validators);


    public MoveSetHelper Collect(int offsetX, int offsetY, int maxSteps = int.MaxValue, params Validator[] validators)
    {
        offsetX *= (_player == Player.Player) ? 1 : -1;
        offsetY *= (_player == Player.Player) ? 1 : -1;

        var nextPos = new Position(_position.q + offsetX, _position.r + offsetY);

        int steps = 0;

        while (steps < maxSteps && _board.IsValid(nextPos))
        {
            _validPositions.Add(nextPos);
            nextPos = new Position(nextPos.q + offsetX, nextPos.r + offsetY);
            steps++;
        }

        return this;
    }
}
