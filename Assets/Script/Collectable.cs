using UnityEngine;

public class Collectable : MonoBehaviour
{
    public CollectableType type;
    public Sprite icon;
    public Rigidbody2D rb2b;
    private void Awake()
    {
        rb2b = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            player.inventory.Add(this);
            Destroy(gameObject);
        }
    }
}
public enum CollectableType
{
    NONE,CARROT_SEED
}