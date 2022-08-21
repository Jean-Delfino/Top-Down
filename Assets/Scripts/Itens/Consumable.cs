using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Portable food, water and other items
*/

[CreateAssetMenu(fileName = "Consumable", menuName = "Items/Consumable", order = 0)]

public class Consumable : Item{
    [SerializeField] List<StatusChange> affectedStatus = default;

    public override void UseItem(PlayerStatus pS){
        foreach(StatusChange sC in affectedStatus){
            pS.ChangeStatus(sC);
        }

        RemoveItem(pS, 1);
    }

    public void RemoveItem(PlayerStatus pS, int qtd){
        GetFatherOfItem().TakesItemInBag(this, qtd);
        //Changes the position in the inventory if the usage is 0
    }

    public override void RemoveItem(PlayerStatus pS){
        GetFatherOfItem().RemoveItemInBag(this);
        //Changes the position in the inventory if the usage is 0
    }
}
