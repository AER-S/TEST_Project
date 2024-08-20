using System;
using UnityEngine;
using UnityEngine.UI;


public class Card : MonoBehaviour
{
    [Serializable]
    public struct CardData
    {
        public int Index;
        public int Value;
    }

    public static Action CardFlipped;
    
    [SerializeField] private Image frontImage;
    [SerializeField] private Animation animation;

    public CardData Data;
    
    private bool _isFlipped;
    
    
    public void SetImage(Sprite newImage)
    {
        frontImage.sprite = newImage;
    }

    public void OnClick()
    {
        if (!_isFlipped)
        {
            Flip();
            GameManager.Instance.TakeCard(this);
        }
    }
    public void Flip()
    {
        animation.Play("Flip");
        _isFlipped = true;
        CardFlipped?.Invoke();
    }

    public void UnFlip()
    {
        animation.Play("FlipBack");
        _isFlipped = false;
        CardFlipped?.Invoke();
    }

    public void DestroyCard()
    {
        animation.Play("Destroy");
    }

    public void DestroyGameObject()
    {
        gameObject.SetActive(false);
        transform.localScale = Vector3.one;
    }
}
