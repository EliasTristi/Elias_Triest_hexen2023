using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private static Position _startPosition = new Position(0, 0);
    private static List<Position> _validPositions = new List<Position>();

    public static void EntitySpawner(GameObject entity, int amount)
    {
        var views = FindObjectsOfType<PositionView>();

        foreach (var view in views)
            if (view.TilePosition.q != _startPosition.q && view.TilePosition.r != _startPosition.r)
                _validPositions.Add(view.TilePosition);

        for (int i = 0;  i < amount; i++)
        {
            var position = _validPositions[Random.Range(0, _validPositions.Count)];
            Instantiate(entity, TileHelper.TileToWorldPosition(position), entity.transform.rotation);
            _validPositions.Remove(position);
            Debug.Log($"Entity placed at {position.ToString()}");
        }
    }
}
