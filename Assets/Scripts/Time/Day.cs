using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Day : Subject{
    [SerializeField] TextMeshProUGUI daysReference = default;
    
    private int daysCount = 1;

    public void DayChange(int addition){
        daysCount += addition;
        daysReference.text = daysCount.ToString();

        Notify(addition);
    }

    //Observer pattern
    protected override void Notify(int addition){
        foreach (Observer ob in observers){
            ob.OnNotify(addition);
        }
    }
}
