using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public int Index { get; set; }

    public void PopulateWith(Card newCard)
    {
        newCard.transform.SetParent(transform);
        newCard.SetSlot(this);
    }
}
