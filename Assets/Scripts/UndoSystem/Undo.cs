using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undo : MonoBehaviour
{

    //public Card Card { get; set; }
    //public List<GameObject> Enemies { get; set; }


    public void UndoAction(Card usedCard, Card newCard)
    {
        usedCard.gameObject.SetActive(true);

        newCard.gameObject.SetActive(false);

        //foreach (var enemy in Enemies)
        //    enemy.gameObject.SetActive(true);
    }
}
