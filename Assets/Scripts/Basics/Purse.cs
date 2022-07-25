using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Contains the money and the itens

    Used for selling and buying stuff (NPCs)
*/

public class Purse : MonoBehaviour{
    private float money = 0;

    private Bag bag = new Bag();

    public void IncreaceMoney(float amount){
        money += amount;
    }

    public float GetMoney(){
        return money;
    }

    public Bag GetBag(){
        return bag;
    }
}
