using UnityEngine;
using UnityEngine.UI;


public class Card : MonoBehaviour
{
    [SerializeField] private Image frontImage;

    public void SetImage(Sprite newImage)
    {
        frontImage.sprite = newImage;
    }
    public void Flip()
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
    }
}
