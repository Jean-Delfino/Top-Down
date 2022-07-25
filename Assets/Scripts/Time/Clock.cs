using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

/*
    If a minute is 60 seconds and minutesDilution is 20 seconds its mean that every
    20 seconds in the real life 1 minute in the game passes
*/

public class Clock : Subject{
    [SerializeField] TextMeshProUGUI clockReference = default;
    [SerializeField] TextMeshProUGUI daysReference = default;

    [SerializeField] int minutesDilution; 

    //More memory, but easier to work, and no cast
    private int actualSeconds = 0;
    private int actualMinutes = 0; 
    private int actualHours = 0; 

    private int daysCount = 0;

    public void addSeconds(int time){
        actualSeconds += time;
        int countMinutes = UtilInt.checkBound(ref actualSeconds, minutesDilution);

        if(countMinutes < 1) return;

        addMinutes(countMinutes);
    }

    public void addMinutesOnlyVisual(int time){
        
    }

    public void addMinutes(int time){
        if(time < 1) return;

        actualMinutes += time;
        actualHours += UtilInt.checkBound(ref actualMinutes, 60);

        ChangeActualTime();
        Notify(time);
    }

    public void SetTime(int hours, int minutes){
        actualHours = hours;
        actualMinutes = minutes;

        ChangeActualTime();
    }

    public void ChangeDay(){
        DayChange(1);
    }

    public void ChangeDay(int dayCount){
        DayChange(dayCount);
    }

    private void ChangeActualTime(){
        if(actualHours > 24){
            actualHours = 0;
            DayChange(1);
        }

        clockReference.text = 
            UtilStrings.ConvertPositiveNumberToFixedSize(actualHours, 2) + ":" + 
            UtilStrings.ConvertPositiveNumberToFixedSize(actualMinutes, 2);
    }

    private void DayChange(int addiction){
        daysCount += addiction;
        daysReference.text = daysCount.ToString();
    }

    //Observer pattern
    protected override void Notify(int value){
        foreach (Observer ob in observers){
            ob.OnNotify(value);
        }
    }
} 
