using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using UntiyEngine.UI;

[System.Serializable]

public abstract class Item : ScriptableObject{
    private Bag fatherInventory;

    [Space]
    [Header("Defining item attribute")]
    [Space]

    [SerializeField] string uniqueID = default;
    [SerializeField] string nameItem = default;
    [SerializeField] Sprite itemIcon = default;

    [Space]
    [Header("Value and Weight")]
    [Space]

    [SerializeField] float value = default;
    [SerializeField] float weight = default;

    public string GetUniqueID(){
        return uniqueID;
    }

    public string GetNameItem(){
        return nameItem;
    }

    public Sprite GetItemIcon(){
        return itemIcon;
    }

    public float GetValue(){
        return value;
    }

    public void SetValue(float value){
        this.value = value;
    }

    public float GetWeight(){
        return weight;
    }

    public void SetWeight(float weight){
        this.weight = weight;
    }


    public Bag GetFatherOfItem(){
        return fatherInventory;
    }

    public void SetFatherOfItem(Bag fatherInventory){
        this.fatherInventory = fatherInventory;
    }

    public abstract void UseItem(PlayerStatus pS);
    public abstract void RemoveItem(PlayerStatus pS);
}
