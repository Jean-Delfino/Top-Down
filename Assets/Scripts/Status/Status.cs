using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class Status : MonoBehaviour{
    [SerializeField] TypeStatus status = default; //Always, always build the list in order
    [SerializeField] int minutesDilution = default;
    [SerializeField] int killerStatus = default; //100 is the max, 0 is no kill
    [SerializeField] int actionDeficiencyStatus = default; //100 is the max, 0 is no affect

    private int actualValueStatus = 100;
    private int limit = 100;

    private int countLastTimeStatusIncreaced = 0;

    public bool IncreaceTime(int time){
        countLastTimeStatusIncreaced += time;

        int statusDecreace = UtilInt.checkBound(countLastTimeStatusIncreaced, minutesDilution);

        return IncreaceStatus(-statusDecreace);
    }

    public bool IncreaceStatus(int statusIncreace){
        actualValueStatus += statusIncreace;

        if(actualValueStatus < 1){
            actualValueStatus = 0; //No negative increace
            return true;
        }

        if(actualValueStatus > limit){
            actualValueStatus = 100;
        }

        return false;
    }

    public int KillerStatusVerify(){
        return killerStatus;
    }

    public int ActionDeficiencyStatusVerify(){
        return actionDeficiencyStatus;
    }
}
