using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public static UIInventory Instance;

    public Transform itemParent;
    public GameObject itemPrefab;

    void Awake()
    {
        Instance = this;
    }

    public void RefreshUI()
    {
        foreach (Transform child in itemParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in InventoryManager.Instance.items)
        {
            GameObject obj = Instantiate(itemPrefab, itemParent);

            Text text = obj.GetComponent<Text>();
            text.text = item.itemName + " x" + item.quantity;
        }
    }
}
