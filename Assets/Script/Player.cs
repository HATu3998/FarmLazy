using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryManager inventory;
   private tileManager tileManager;
    private void Start()
    {
        tileManager = GameManager.instance.tileManager;
    }
    private void Awake()
    {
        inventory = GetComponent<InventoryManager>();
    }
 
    public void DropItem(Item item)
    {
        Vector3 spawnLocation = transform.position;

        Vector3 spawnOffset = Random.insideUnitCircle *1.40f;
      Item droppedItem=  Instantiate(item, spawnLocation + spawnOffset, Quaternion.identity);
        droppedItem.rb2b.AddForce(spawnOffset * .2f, ForceMode2D.Impulse);
    }
    public void DropItem(Item item,int numToDrop)
    { 
    for(int i = 0; i < numToDrop; i++)
        {
            DropItem(item);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GameManager.instance == null) Debug.Log("GameManager.instance is NULL");
            else if (GameManager.instance.tileManager == null) Debug.Log("TileManager is NULL");
            else
            {
                 if(tileManager!= null)
                {
                    Vector3Int position = new Vector3Int((int)transform.position.x, (int)transform.position.y, 0);
                    string tileName = tileManager.GetTileName(position);
                    if (!string.IsNullOrWhiteSpace(tileName))
                    {
                        if(tileName== "Interactable" && inventory.toolbar.selectedSlot.itemName=="Hoe")
                        {
                            tileManager.SetInteracted(position);
                        }
                    }
                }
            }
        }
    }
}
