using UnityEngine;

public class Card : MonoBehaviour
{
    public void Flip()
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
    }
}
