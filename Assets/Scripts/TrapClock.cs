using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapClock : MonoBehaviour
{
    public static TrapClock current;
    private bool shouldActivate;

    public void Awake()
    {
        current = this;
    }

    public void Start()
    {
        InvokeRepeating("TickMethod", 1, 1);
    }

    public event Action tick;
    public event Action<bool> onOfftick;
    private void TickMethod()
    {
        shouldActivate = !shouldActivate;
        tick();
        onOfftick(shouldActivate);

        //doors turn on and off
        //arrow traps turn on and off
        //arrowas shoot everytime

    }
}
