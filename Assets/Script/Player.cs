using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory inventory;
    private void Awake()
    {
        inventory = new Inventory(21);
    }
    public void DropItem(Collectable item)
    {
        Vector3 spawnLocation = transform.position;

        Vector3 spawnOffset = Random.insideUnitCircle *1.40f;
      Collectable droppedItem=  Instantiate(item, spawnLocation + spawnOffset, Quaternion.identity);
        droppedItem.rb2b.AddForce(spawnOffset * .2f, ForceMode2D.Impulse);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GameManager.instance == null) Debug.Log("GameManager.instance is NULL");
            else if (GameManager.instance.tileManager == null) Debug.Log("TileManager is NULL");
            else
            {
                Vector3Int position = new Vector3Int((int)transform.position.x, (int)transform.position.y, 0);
                if (GameManager.instance.tileManager.IsInteractable(position))
                   { Debug.Log("tile is interactable");
                    GameManager.instance.tileManager.SetInteracted(position);
                }
                else
                    Debug.Log("Tile not interactable at " + position);
            }
        }
    }
}
