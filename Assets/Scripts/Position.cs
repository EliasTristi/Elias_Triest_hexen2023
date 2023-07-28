using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Position
{
    private readonly int _q;
    private readonly int _r;
    private readonly int _s;

    public int q => _q;
    public int r => _r;
    public int s => _s;

    public Position(int q, int r, int s)
    {
        _q = q;
        _r = r;
        _s = s;
    }

    public override string ToString()
    {
        return $"Position(q: {q}, r: {r}, s: {s}";
    }
}
