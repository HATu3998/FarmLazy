using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Inventory;

public class SlotUI : MonoBehaviour
{
    public int slotId;
    public Image itemIcon;
    public TextMeshProUGUI quantityText;
    [SerializeField] private GameObject highLight;

    public void SetItem(Inventory.Slot slot)
    {
        if (slot != null)
        {
            itemIcon.sprite = slot.icon;
            itemIcon.color = new Color(1, 1, 1, 1);
            quantityText.text = slot.count.ToString();
        }
    }
    public void SetEmpty()
    {
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0);
        quantityText.text = "";
    }
    public void setHighLight(bool isOn)
    {
        highLight.SetActive(isOn);
    }
}
