using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Deck : MonoBehaviour
{
    private static int _deckSize = 12;
    private int _amount = 5;

    private Vector3 _spacing = new Vector3(160, 0);
    private Vector3 _increment = new Vector3(64, 0, 0) * -1;

    [SerializeField]
    private GameObject[] _prefabs;
    private GameObject[] _cards;

    private void Start()
    {
        _cards = new GameObject[_deckSize];

        for (int i  = 0; i < _deckSize; i++)
        {
            var card = Instantiate(_prefabs[Random.Range(0, _prefabs.Length)], transform);
            card.SetActive(false);
            _cards[i] = card;
        }

        DeckUpdate();
    }

    public void DeckUpdate()
    {
        List<GameObject> tempCards = new List<GameObject>(_cards);

        int amount;
        var startPosition = transform.position;

        startPosition = GeneratePosition(tempCards, startPosition);

        for (int i = 0; i < tempCards.Count; i++)
        {
            var card = tempCards[i];
            if (card.GetComponent<Card>().IsPlayed)
            {
                card.SetActive(false);
                tempCards.Remove(card);
            }
        }

        if (tempCards.Count >= 5)
            amount = _amount;
        else
            amount = tempCards.Count;

        for (int i = 0; i < amount; i++)
        {
            var card = tempCards[i];

            card.SetActive(true);
            card.transform.position = startPosition;

            startPosition += _spacing;
        }

        _cards = tempCards.ToArray();
    }

    private Vector3 GeneratePosition(List<GameObject> tempCards, Vector3 startPosition)
    {
        switch (tempCards.Count)
        {
            case 0:
                startPosition = transform.position + _increment;
                break;
            case 1:
                startPosition = transform.position + _increment * 1;
                break;
            case 2:
                startPosition = transform.position + _increment * 2;
                break;
            case 3:
                startPosition = transform.position + _increment * 3;
                break;
            case 4:
                startPosition = transform.position + _increment * 4;
                break;
            case >= 5:
                startPosition = transform.position + _increment * 5;
                break;
        }

        return startPosition;
    }
}
