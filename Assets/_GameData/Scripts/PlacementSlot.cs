using UnityEngine;

public class PlacementSlot : MonoBehaviour
{
    // Bu slota yerleştirilmiş olan obje (engel veya çoğaltıcı).
    public GameObject placedItem { get; private set; }

    // Bu slota bir obje yerleştirmek için kullanılır.
    public void PlaceItem(GameObject item)
    {
        placedItem = item;
    }

    public void ClearSlot()
    {
        if (placedItem != null)
        {
            Destroy(placedItem);
            placedItem = null;
        }
    }

    public bool IsEmpty()
    {
        return placedItem == null;
    }
}