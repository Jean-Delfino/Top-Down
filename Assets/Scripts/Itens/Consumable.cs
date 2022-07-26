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

        RemoveItem(pS);
    }

    public override void RemoveItem(PlayerStatus pS){
        GetFatherOfItem().TakesItemInBag(this, 1);
        //Changes the position in the inventory if the usage is 0
    }
}
