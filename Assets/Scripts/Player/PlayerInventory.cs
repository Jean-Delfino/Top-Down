using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class PlayerInventory : Purse{
    [SerializeField] TextMeshProUGUI moneyOnScreen = default;
    [SerializeField] float initialMoney = default;

    private void Start() {
        ChangeMoney(initialMoney);
    }

    //Check the key used to open the inventory
    private void Update(){}

    public void ChangeMoney(float amount){
        IncreaceMoney(amount);

        moneyOnScreen.text = GetMoney().ToString();
    }
}
