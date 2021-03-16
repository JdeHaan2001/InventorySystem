using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class ItemSlotUI : MonoBehaviour, IDropHandler
{
    [SerializeField] protected Image itemIcon = null;

    public int SlotIndex { get; private set; }

    public abstract Item SlotItem { get; set; }

    private void OnEnable() => UpdateSlotUI();

    public virtual void Start()
    {
        SlotIndex = transform.GetSiblingIndex();
        UpdateSlotUI();
    }

    public abstract void OnDrop(PointerEventData eventData);

    public abstract void UpdateSlotUI();

    protected virtual void EnableSlotUI(bool enable) => itemIcon.enabled = enable;
}
