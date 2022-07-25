using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] string initialBagName = default;

    private void Start(){
        //mainBag.SpawnAllItensMain(GetBag(), initialBagName);
        ChangeMoney(initialMoney);
    }

    public void ChangeMoney(float amount){
        IncreaceMoney(amount);

        moneyOnScreen.text = GetMoney().ToString();
    }

    public string GetInitialBagName(){
        return initialBagName;
    }
}
