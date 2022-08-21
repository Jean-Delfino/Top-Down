using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour{
    [Space]
    [Header("Inventory system pure attributes")]
    [Space]

    [SerializeField] TextMeshProUGUI bagName;

    [SerializeField] GameObject inventory; 
    [SerializeField] Transform itemSpawn = default;
    [SerializeField] InventorySlot itemPrefab = default;
    
    private PlayerInventory playerInventory = default;

    private Dictionary<Item, InventorySlot> itemPerSlot;

    public void Setup(PlayerInventory playerInventory){
        itemPerSlot = new Dictionary<Item, InventorySlot>();

        this.playerInventory = playerInventory;

        bagName.text = playerInventory.GetInitialBagName();

        StartCoroutine(CheckInventory());
    }

    public void SpawnAllItensMain(Bag mainBag, string name){
        InventorySlot invS;

        foreach(KeyValuePair<Item, int> kvp in mainBag.GetContentDictionary()){
            print("TEM UM ITEM");
            CreateSlot(kvp.Key, kvp.Value);
        }

        bagName.text = name;
    }

    public void UseItem(Item item){
        item.UseItem(playerInventory.GetPlayerStatus());
        int itemQtd = playerInventory.GetItemQtd(item);

        if(itemQtd < 1){
            RemoveItem(item);
            return;
        }

        itemPerSlot[item].ChangeSlotQtd(itemQtd);
    }   

    public void RemoveItem(Item item){
        item.RemoveItem(playerInventory.GetPlayerStatus());
        
        Destroy(itemPerSlot[item].gameObject);
        itemPerSlot.Remove(item);
    }

    public void AddItem(Item item, int qtd){
        if(itemPerSlot.ContainsKey(item)){
            //Just change the quantity in the slot
            itemPerSlot[item].ChangeSlotQtd(qtd);
        }else{
            CreateSlot(item, qtd);
        }
    }

    private void CreateSlot(Item item, int qtd){
        InventorySlot invS = Instantiate<InventorySlot>(itemPrefab, itemSpawn);

        invS.SetupSlot(item, qtd, this);
        itemPerSlot.Add(item, invS);
    }
    
    private IEnumerator CheckInventory(){
        bool unpressedButton = true;
        float getAxis;

        while(true){
            getAxis = Input.GetAxisRaw("Inventory");

            if(unpressedButton && getAxis != 0){
                    unpressedButton = false;
                    inventory.SetActive(!inventory.activeSelf);
                    yield return new WaitForSeconds(0.2f);
            }else if(getAxis == 0){
                unpressedButton = true;
            }
            yield return null;
        }
    }   
}
