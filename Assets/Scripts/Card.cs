using System;
using UnityEngine;
using UnityEngine.UI;


public class Card : MonoBehaviour
{
    [System.Serializable]
    public struct CardData
    {
        public int Index;
        public int Value;
    }
    
    [SerializeField] private Image frontImage;
    [SerializeField] private Animation animation;

    public CardData Data;
    

    private bool isFlipped = false;
    

    


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
        animation.Play("Flip");
        isFlipped = true;
    }

    public void UnFlip()
    {
        animation.Play("FlipBack");
        isFlipped = false;
    }

    public void DestroyCard()
    {
        animation.Play("Destroy");
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
