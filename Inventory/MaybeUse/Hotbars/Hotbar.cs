using UnityEngine;

public class Hotbar : MonoBehaviour
{
    [SerializeField] private HotbarSlot[] hotbarSlots = new HotbarSlot[3];

    public void Add(Item itemToAdd)
    {
        foreach (HotbarSlot hotbarSlot in hotbarSlots)
        {
            if (hotbarSlot.AddItem(itemToAdd)) return;
        }
    }

    public HotbarSlot GetIndex(int index) => hotbarSlots[index];
}
