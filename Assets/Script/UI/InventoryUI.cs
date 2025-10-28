using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    
    public string inventoryName;
    public List<SlotUI> slots = new List<SlotUI>();
    
    [SerializeField] private Canvas canvas;
    private bool dragSingle;
    private Inventory inventory;
    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }
    private void Start()
    {
        if (GameManager.instance == null || GameManager.instance.player == null)
        { 
            return;
        }
        inventory = GameManager.instance.player.inventory.GetInventoryByName(inventoryName);

        if (inventory == null)
        {
             
            return; // D?ng l?i n?u kh?ng t?m th?y
        }
        SetupSlots();
        Refresh();
    }
    // Update is called once per frame
 
    
  public   void Refresh()
    {
        if(slots.Count==  inventory.slots.Count)
        {
            for(int i=0;i< slots.Count; i++)
            {
                if ( inventory.slots[i].itemName != "")
                {
                    slots[i].SetItem( inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty(); 
                }
            }
        } 
    }
    public void Remove()
    {
      Item itemToDrop = GameManager.instance.itemManager.
            GetItemByName( inventory.slots[UIManager.draggedSlot.slotId].itemName );

        if (itemToDrop !=null)
        {
            if (UIManager.dragSingle)
            {
              GameManager.instance.  player.DropItem(itemToDrop );
              inventory.Remove(UIManager.draggedSlot.slotId );
            }
            else{
                GameManager.instance.player.DropItem(itemToDrop,  inventory.slots[UIManager.draggedSlot.slotId].count);
            inventory.Remove(UIManager.draggedSlot.slotId, inventory.slots[UIManager.draggedSlot.slotId].count);
            }
            Refresh();
        }
        UIManager.draggedSlot = null;
    }
    public void SlotBeginDrag(SlotUI slot)
    {
     UIManager.   draggedSlot = slot;
        UIManager.draggedIcon = Instantiate(UIManager.draggedSlot.itemIcon);
        UIManager.draggedIcon.transform.SetParent(canvas.transform);
        UIManager.draggedIcon.raycastTarget = false;
        UIManager.draggedIcon.rectTransform.sizeDelta = new Vector2(50, 50);
        MoveToMousePosition(UIManager.draggedIcon.gameObject);
        Debug.Log("Start Drag:" + UIManager.draggedSlot.name);
    }
    public void SlotDrag()
    {
        MoveToMousePosition(UIManager.draggedIcon.gameObject);
        Debug.Log("Dragging " + UIManager.draggedSlot.name);
    }
    public void SlotEndDrag()
    {
        if (UIManager.draggedIcon != null)
        {
            Destroy(UIManager.draggedIcon.gameObject);
            UIManager.draggedIcon = null;
        }

        // Ki?m tra xem chu?t c? n?m trong UI kh?ng
        if (!IsPointerOverUI())
        {
            // N?u kh?ng n?m trong UI -> drop ra ??t
            Remove();
            Debug.Log("Dropped item to ground");
        }
        else
        {
            Debug.Log("Dropped item back in inventory UI");
        }

        UIManager.draggedSlot = null;

    }
    private bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
    public void SlotDrop(SlotUI slot)
    {
        if (UIManager.dragSingle)
        {
            UIManager.draggedSlot.inventory.MoveSlot(UIManager.draggedSlot.slotId, slot.slotId, slot.inventory);

        }
        else
        {
            UIManager.draggedSlot.inventory.MoveSlot(UIManager.draggedSlot.slotId, slot.slotId, slot.inventory,
               UIManager.draggedSlot.inventory.slots[UIManager.draggedSlot.slotId].count);
        }
            GameManager.instance.uiManager.RefreshAll();
    }
    private void MoveToMousePosition(GameObject toMove)
    {
        if(canvas != null)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Input.mousePosition, null, out position);
            toMove.transform.position = canvas.transform.TransformPoint(position);
        }
    }
    void  SetupSlots()
    {
        int counter = 0;
        foreach(SlotUI slot in slots)
        {
            slot.slotId = counter;
            counter++;
            slot.inventory = inventory;
        }
    }
}
