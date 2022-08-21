using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Contains the money and the itens

    Used for selling and buying stuff (NPCs)
*/

public class Purse : MonoBehaviour{
    private float money = 0;

    [SerializeField] Bag bag = default;

    public void IncreaceMoney(float amount){
        money += amount;
    }

    public bool AddItem(Item item){
        return bag.AddItem(item);
    }

    public float GetMoney(){
        return money;
    }

    public Bag GetBag(){
        return bag;
    }

    public string GetBagName(){
        return bag.GetNameItem();
    }
}
