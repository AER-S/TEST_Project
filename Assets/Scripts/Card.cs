using UnityEngine;
using UnityEngine.UI;


public class Card : MonoBehaviour
{
    [SerializeField] private Image frontImage;

    private CardSlot _slot;

    public void SetSlot(CardSlot slot) => _slot = slot;
    public void SetImage(Sprite newImage)
    {
        frontImage.sprite = newImage;
    }
    public void Flip()
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
    }
}
