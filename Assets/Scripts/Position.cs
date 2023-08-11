using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Position
{
    private readonly int _q;
    private readonly int _r;

    public int q => _q;
    public int r => _r;

    public Position(int q, int r)
    {
        _q = q;
        _r = r;
    }

    public override string ToString()
    {
        return $"Position(q: {q}, r: {r})";
    }

    public static bool operator ==(Position left, Position right)
    {
        return left._q == right._q && left._r == right._r;
    }

    public static bool operator !=(Position left, Position right)
    {
        return !(left == right);
    }
}
