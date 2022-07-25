using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class InventorySlot : MonoBehaviour{
    private InventoryUI father;

    [SerializeField] Image icon = default;
    [SerializeField] TextMeshProUGUI qtdItem = default;

    private Item it;

    public void SetupSlot(Item newIt, int qtd, InventoryUI father){
        it = newIt;

        icon.sprite = it.GetItemIcon();
        qtdItem.text = qtd.ToString();

        this.father = father;
    }

    public void UseItem(){
        father.UseItem(it);
    }

    public void RemoveItem(){
        father.RemoveItem(it);
    }

    public int GetPosition(){
        return this.transform.GetSiblingIndex();
    }
}
