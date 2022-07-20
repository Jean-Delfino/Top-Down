using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerStatus : Observer{
    private int health = 0;

    [Space]
    [Header("Status variables")]
    [Space]

    [SerializeField] Subject toSubscribe = default;
    [SerializeField] PlayerStatusUIManager pSM = default;

    [SerializeField] List<Status> needs = default;


    private void Start() {
        toSubscribe.RegisterObserver(this);

        pSM.SpawnStatus(needs);
        ChangeLife(100);
    }

    public override void OnNotify(int timeChange){
        bool changeValue = false;
        int i;

        for(i = 0; i < needs.Count; i++){
            changeValue = needs[i].IncreaceTime(timeChange);

            if(changeValue){
                pSM.ChangeStatus(needs[i], i);
            }
        }
    }

    private void ChangeLife(int amount){
        health += amount;

        pSM.ChangeSliderHealth(health);
    }

}
