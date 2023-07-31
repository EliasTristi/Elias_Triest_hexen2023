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
}
