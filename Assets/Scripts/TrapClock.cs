using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapClock : MonoBehaviour
{
    public static TrapClock current;

    public void Awake()
    {
        current = this;
    }

    public void Start()
    {
        InvokeRepeating("TickMethod", 1, 1);
    }

    public event Action tick;
    private void TickMethod()
    {
        tick();
    }
}
