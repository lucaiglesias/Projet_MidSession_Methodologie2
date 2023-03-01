using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//reference https://www.youtube.com/watch?v=Yy7Dt2usGy0&list=PLEWxpJUw6sFkcUsypxWTpCIvJBgaLWxtp&index=9&ab_channel=JasonWeimann

public abstract class Observer : MonoBehaviour
{
    public abstract void OnNotify(object value);
}

public abstract class Subject : MonoBehaviour
{
    public static Subject Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private List<Observer> _observers = new List<Observer>();



    public void RegisterObserver(Observer observer)
    {
        _observers.Add(observer);
    }

    public void Notify(object value)
    {
        foreach (var observer in _observers)
            observer.OnNotify(value);
    }
}
