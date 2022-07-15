using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

/*
    If a minute is 60 seconds and minutesDilution is 20 seconds its mean that every
    20 seconds in the real life 1 minute in the game passes
*/

public class Clock : MonoBehaviour{
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
        int countMinutes = UtilInt.checkBound(actualSeconds, minutesDilution);

        if(countMinutes < 1) return;

        addMinutes(countMinutes);
    }

    public void addMinutes(int time){
        actualMinutes += time;
        
        actualHours += UtilInt.checkBound(actualMinutes, 60);
        ChangeActualTime();
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

        clockReference.text = actualHours.ToString() + ":" + actualMinutes.ToString();
    }

    private void DayChange(int addiction){
        daysCount += addiction;
        daysReference.text = daysCount.ToString();
    }
} 
