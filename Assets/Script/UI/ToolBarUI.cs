using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class ToolBarUI : MonoBehaviour
{
    [SerializeField] private List<SlotUI> toolbarSlots = new List<SlotUI>();
    private SlotUI selectedSlot;
    public void SelectSlot(int index)
    {
        if(toolbarSlots.Count == 9)
        {if(selectedSlot != null)
            {
                selectedSlot.setHighLight(false);
            }
            selectedSlot = toolbarSlots[index];
            selectedSlot.setHighLight(true); 
            Debug.Log("selected slot: " + selectedSlot.name);
        }
    }
    private void CheckAlphaNumbericKeys()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectSlot(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectSlot(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectSlot(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectSlot(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectSlot(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectSlot(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectSlot(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SelectSlot(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SelectSlot(8);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SelectSlot(0);
    }

    // Update is called once per frame
    void Update()
    {
        CheckAlphaNumbericKeys();
    }
}
