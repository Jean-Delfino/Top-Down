using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;

using TMPro;

/*
    If a minute is 60 seconds and minutesDilution is 20 seconds its mean that every
    20 seconds in the real life 1 minute in the game passes
*/

public class Clock : Subject{
    [SerializeField] Day daySubject = default;

    [SerializeField] TextMeshProUGUI clockReference = default;

    [SerializeField] int minutesDilution = default;

    //More memory, but easier to work, and no cast
    private int actualSeconds = 0;
    private int actualMinutes = 0; 
    private int actualHours = 0; 

    public void addSeconds(int time){
        actualSeconds += time;
        int countMinutes = UtilInt.checkBound(ref actualSeconds, minutesDilution);

        if(countMinutes < 1) return;

        addMinutes(countMinutes);
    }

    public void addMinutesVisual(int time){
        actualMinutes += time;
        actualHours += UtilInt.checkBound(ref actualMinutes, 60);

        ChangeActualTime();
    }

    public void addMinutes(int time){
        if(time < 1) return;

        addMinutesVisual(time);

        Notify(time);
    }

    public async void fastFowardTime(int clockTimeToPassInMin, int realWorldTimeInSec){
        int tick = clockTimeToPassInMin / realWorldTimeInSec;
        int i;

        print("TICK = " + tick);

        for(i = 0; i < realWorldTimeInSec; i++){
            addMinutes(tick);
            await Task.Delay(1 * 1000);
        }
    }

    public void SetTime(int hours, int minutes){
        actualHours = hours;
        actualMinutes = minutes;

        ChangeActualTime();
    }

    public void ChangeDay(){
        daySubject.DayChange(1);
    }

    public void ChangeDay(int dayCount){
        daySubject.DayChange(dayCount);
    }

    private void ChangeActualTime(){
        if(actualHours > 24){
            actualHours = 0;
            ChangeDay();
        }

        clockReference.text = 
            UtilStrings.ConvertPositiveNumberToFixedSize(actualHours, 2) + ":" + 
            UtilStrings.ConvertPositiveNumberToFixedSize(actualMinutes, 2);
    }

    //Observer pattern
    protected override void Notify(int value){
        foreach (Observer ob in observers){
            ob.OnNotify(value);
        }
    }
} 
