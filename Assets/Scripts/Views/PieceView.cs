using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceView : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    public Player Player => _player;

    public Vector3 WorldPosition => transform.position;

    public void Move(Vector3 toPosition)
    {
        toPosition.y = transform.position.y;
        transform.position = toPosition;
    }

    public void Take()
    {
        gameObject.SetActive(false);
    }

    public void Place(Vector3 worldPosition)
    {
        worldPosition.y = transform.position.y;
        transform.position = worldPosition;
        gameObject.SetActive(true);
    }
}
