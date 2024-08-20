using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public void PopulateWith(Card newCard)
    {
        newCard.transform.SetParent(transform);
    }
}
