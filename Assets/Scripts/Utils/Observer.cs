using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Observer : MonoBehaviour{
    public abstract void OnNotify(int value);
}

public abstract class Subject : MonoBehaviour{
    protected List<Observer> observers = new List<Observer>();

    public void RegisterObserver(Observer observer){
        observers.Add(observer);
    }

    protected abstract void Notify(int value);
}
