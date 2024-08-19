using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public void PopulateWith(Card newCard)
    {
        newCard.transform.SetParent(transform);
        newCard.SetSlot(this);
    }
}
