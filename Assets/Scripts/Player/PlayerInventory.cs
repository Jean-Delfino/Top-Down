using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

using TMPro;

public class PlayerInventory : Purse{
    [Space]
    [Header("Money attributes")]
    [Space]

    [SerializeField] TextMeshProUGUI moneyOnScreen = default;
    [SerializeField] float initialMoney = default;

    [Space]
    [Header("Inventory attributes")]
    [Space]

    [SerializeField] PlayerStatus playerStatus = default;

    private void Start(){
        //mainBag.SpawnAllItensMain(GetBag(), initialBagName);
        ChangeMoney(initialMoney);

        playerStatus.InventoryUISetup(this);
        AddItem("Chocolate-Bar");
        AddItem("Chocolate-Bar");
        AddItem("BottleOfWater");
    }

    public bool AddItem(string uniqueIDitem){
        var result = AssetDatabase.FindAssets(uniqueIDitem);
        var path = AssetDatabase.GUIDToAssetPath(result[0]);
        var item = (Item) AssetDatabase.LoadAssetAtPath(path, typeof(Item));

        var addItemResult = base.AddItem((Item)item);
        playerStatus.InventoryUIAddItem(item, GetItemQtd(item));

        return addItemResult;
    }

    public int GetItemQtd(Item item){
        return GetBag().GetItemQtd(item);
    }


    public void ChangeMoney(float amount){
        IncreaceMoney(amount);

        moneyOnScreen.text = GetMoney().ToString();
    }

    public Bag GetInventoryBag(){
        return this.GetBag();
    }

    public string GetInitialBagName(){
        return GetBagName();
    }

    public new bool AddItem(Item item){
        return base.AddItem(item);
    }

    public PlayerStatus GetPlayerStatus(){
        return this.playerStatus;
    }
}
