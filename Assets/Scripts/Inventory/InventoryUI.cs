using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour{
    [SerializeField] PlayerStatus playerStatus = default;

    [Space]
    [Header("Inventory system pure attributes")]
    [Space]

    [SerializeField] TextMeshProUGUI bagName;

    [SerializeField] GameObject inventory; 
    [SerializeField] Transform itemSpawn = default;
    [SerializeField] InventorySlot itemPrefab = default;

    private void Start(){
        SpawnAllItensMain(playerStatus.GetInventoryBag(), 
                          playerStatus.GetInitialBagName());

        StartCoroutine(CheckInventory());
    }

    public void SpawnAllItensMain(Bag mainBag, string name){
        InventorySlot invS;

        foreach(KeyValuePair<Item, int> kvp in mainBag.GetContentDictionary()){
            invS = Instantiate<InventorySlot>(itemPrefab, itemSpawn);

            invS.SetupSlot(kvp.Key, kvp.Value, this);
        }

        bagName.text = name;
    }

    public void UseItem(Item item){
        item.UseItem(playerStatus);
    }

    public void RemoveItem(Item item){
        item.RemoveItem(playerStatus);
    }
    
    private IEnumerator CheckInventory(){
        while(true){
            if(Input.GetAxis("Inventory") != 0){
                inventory.SetActive(!inventory.activeSelf);
                yield return new WaitForSeconds(0.2f);
            }
            yield return null;
        }
    }   
}
