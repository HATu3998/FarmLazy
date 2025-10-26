using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Player player;
    public List<SlotUI> slots = new List<SlotUI>();
    private SlotUI draggedSlot;
    private Image draggedIcon;
    [SerializeField] private Canvas canvas;
    private bool dragSingle;
    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventory();
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            dragSingle = true;
        }
        else
        {
            dragSingle = false;
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
    public void Remove()
    {
      Item itemToDrop = GameManager.instance.itemManager.
            GetItemByName(player.inventory.slots[draggedSlot.slotId].itemName );

        if (itemToDrop !=null)
        {
            if (dragSingle)
            {
                player.DropItem(itemToDrop );
                player.inventory.Remove(draggedSlot.slotId );
            }
            else{
                player.DropItem(itemToDrop, player.inventory.slots[draggedSlot.slotId].count);
                player.inventory.Remove(draggedSlot.slotId, player.inventory.slots[draggedSlot.slotId].count);
            }
            Refresh();
        }
        draggedSlot = null;
    }
    public void SlotBeginDrag(SlotUI slot)
    {
        draggedSlot = slot;
        draggedIcon = Instantiate(draggedSlot.itemIcon);
        draggedIcon.transform.SetParent(canvas.transform);
        draggedIcon.raycastTarget = false;
        draggedIcon.rectTransform.sizeDelta = new Vector2(50, 50);
        MoveToMousePosition(draggedIcon.gameObject);
        Debug.Log("Start Drag:" + draggedSlot.name);
    }
    public void SlotDrag()
    {
        MoveToMousePosition(draggedIcon.gameObject);
        Debug.Log("Dragging " + draggedSlot.name);
    }
    public void SlotEndDrag()
    {
        if (draggedIcon != null)
        {
            Destroy(draggedIcon.gameObject);
            draggedIcon = null;
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

        draggedSlot = null;

    }
    private bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
    public void SlotDrop(SlotUI slot)
    {
        Debug.Log("Dropped " + draggedSlot.name + "on" + slot.name);
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
}
