using UnityEngine;
using UnityEngine.UI;


public class Card : MonoBehaviour
{
    [SerializeField] private Image frontImage;
    
    public int Index { get; set; }
    public int Value { get; set; }

    private CardSlot _slot;

    private bool isFlipped = false;

    public void SetSlot(CardSlot slot) => _slot = slot;
    
    
    public void SetImage(Sprite newImage)
    {
        frontImage.sprite = newImage;
    }

    public void OnClick()
    {
        if (!isFlipped)
        {
            Flip();
            GameManager.Instance.TakeCard(this);
        }
    }
    public void Flip()
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
        isFlipped = true;
    }

    public void UnFlip()
    {
        transform.eulerAngles = Vector3.zero;
        isFlipped = false;
    }
}
