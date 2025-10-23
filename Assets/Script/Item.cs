using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Item : MonoBehaviour
{
    public ItemData data;
 [HideInInspector]   public Rigidbody2D rb2b;
    private void Awake()
    {
        rb2b = GetComponent<Rigidbody2D>();
    }
}
