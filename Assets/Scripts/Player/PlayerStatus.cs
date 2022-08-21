using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerStatus : Observer{
    protected float health = 0;

    [Space]
    [Header("Status variables")]
    [Space]

    [SerializeField] Subject clock = default;
    [SerializeField] PlayerStatusUIManager pSM = default;

    [SerializeField] List<Status> needs = default;

    [Space]
    [Header("Inventory variables")]
    [Space]

    [SerializeField] InventoryUI playerInventoryUI = default;

    private void Start() {
        clock.RegisterObserver(this);

        pSM.SpawnStatus(needs);
        ChangeLife(100);
    }

    public override void OnNotify(int timeChange){
        bool changeValue = false;
        int i;

        for(i = 0; i < needs.Count; i++){
            changeValue = needs[i].IncreaceTime(timeChange);

            CheckStatus(changeValue, i);
        }
    }

    public void ChangeStatus(StatusChange sC){
        if(sC.GetTypeStatus() != TypeStatus.Life){
            ChangeStatus(sC.GetStatus(), sC.GetValueChange());
            return;
        }

        ChangeLife(sC.GetValueChange());
    }

    public void ChangeStatus(int index, int value){
        bool changeValue = needs[index].IncreaceStatus(value);

        CheckStatus(changeValue, index);
    }

    private void CheckStatus(bool change, int index){
        if(change){
            pSM.ChangeStatus(needs[index], index);
        }
    }

    public void ChangeLife(float amount){
        health += amount;

        pSM.ChangeSliderHealth(health);
    }

    public void InventoryUISetup(PlayerInventory playerInventory){
        playerInventoryUI.Setup(playerInventory);
    }

    public void InventoryUIAddItem(Item item, int qtd){
        playerInventoryUI.AddItem(item, qtd);
    }

}
