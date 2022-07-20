using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public enum TypeStatus{
    Hunger,
    Thirst,
    Cleanliness,
    Sleep,
    Happiness
}

/*
    Every minute in game * minutesDilution takes 1 point from actualValueStatus,
    This is counted in countLastTimeStatusIncreaced

    Happiness is actionDeficiency but not killer (the game don't have suicide).
    Cleanliness is nothing but later will affect interactions or happiness.

    Return true in IncreaceTime and IncreaceStatus if actualValueStatus == 0
    So the player can get this value and use it
    He verify if killerStatus is true, if it's then he gets the sum of the bad values
*/

[System.Serializable]

public class Status{
    [SerializeField] TypeStatus status = default; //Always, always build the list in order
    [SerializeField] Sprite icon = default;

    [SerializeField] int minutesDilution = default; //X minutes takes 1 value
    [SerializeField] float killerStatus = default; //100 is the max, 0 is no kill
    [SerializeField] float actionDeficiencyStatus = default; //100 is the max, 0 is no affect

    [SerializeField] int actualValueStatus = 100;
    private int limit = 100;

    private int countLastTimeStatusIncreaced = 0;

    public bool IncreaceTime(int time){
        countLastTimeStatusIncreaced += time;

        int statusDecreace = UtilInt.checkBound(ref countLastTimeStatusIncreaced, minutesDilution);

        return IncreaceStatus(-statusDecreace);
    }

    public bool IncreaceStatus(int statusIncreace){
        if(statusIncreace == 0) return false;

        actualValueStatus += statusIncreace;

        if(actualValueStatus < 1){
            actualValueStatus = 0; //No negative increace
            return true;
        }

        if(actualValueStatus > limit){
            actualValueStatus = 100;
        }

        return true;
    }

    public string GetStatusName(){
        return status.ToString();
    }

    public int GetActualValueStatus(){
        return actualValueStatus;
    }

    public Sprite GetIcon(){
        return icon;
    } 

    public float GetKillerStatusVerify(){
        return killerStatus;
    }

    public float GetActionDeficiencyStatusVerify(){
        return actionDeficiencyStatus;
    }
}
