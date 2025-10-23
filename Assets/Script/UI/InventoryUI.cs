using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Player player;
    public List<SlotUI> slots = new List<SlotUI>();
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventory();
        }
    }
    public void ToggleInventory()
    {
        if (!inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(true);
            Refresh();
        }
        else
        {
            inventoryPanel.SetActive(false);
        }

    }
    void Refresh()
    {
        if(slots.Count== player.inventory.slots.Count)
        {
            for(int i=0;i< slots.Count; i++)
            {
                if (player.inventory.slots[i].itemName != "")
                {
                    slots[i].SetItem(player.inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty(); 
                }
            }
        }
    }
    public void Remove(int slotID)
    {
      Item itemToDrop = GameManager.instance.itemManager.GetItemByName(player.inventory.slots[slotID].itemName );

        if (itemToDrop !=null)
        {
            player.DropItem(itemToDrop);
            player.inventory.Remove(slotID);
            Refresh();
        }
       
    }
}
