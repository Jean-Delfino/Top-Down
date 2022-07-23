using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public abstract class Item{
    private Bag fatherInventory;
    private int position;

    [SerializeField] float value = default;
    [SerializeField] float weight = default;

    public float GetWeight(){
        return weight;
    }

    public void SetWeight(float weight){
        this.weight = weight;
    }

    public float GetValue(){
        return value;
    }

    public void SetValue(float value){
        this.value = value;
    }

    public Bag GetFatherOfItem(){
        return fatherInventory;
    }

    public void SetFatherOfItem(Bag fatherInventory, int position){
        this.fatherInventory = fatherInventory;
        SetPosition(position);
    }

    public int GetPosition(){
        return position;
    }

    public void SetPosition(int position){
        this.position = position;
    }

    public abstract void UseItem(PlayerStatus pS);
    public abstract void RemoveItem();

}
