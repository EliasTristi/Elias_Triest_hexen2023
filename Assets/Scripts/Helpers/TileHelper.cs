using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHelper : MonoBehaviour
{
    [SerializeField]
    private const float _tileSize = 0.5f;

    public const int Distance = 3;

    public static Position WorldToTilePosition(Vector3 position)
    {
        var tileQ = (Mathf.Sqrt(3f) / 3f * position.x - 1f / 3f * position.z) / _tileSize;
        var tileR = (2f / 3f * position.z) / _tileSize;

        return HexHelper.AxialRound(tileQ, tileR);
    }

    public static Vector3 TileToWorldPosition(Position position)
    {
        var worldX = (Mathf.Sqrt(3f) * position.q + Mathf.Sqrt(3) / 2 * position.r) * _tileSize;
        var worldZ = (3f / 2f * position.r) * _tileSize;

        return new Vector3(worldX, 0, worldZ);
    }
}
