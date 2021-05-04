using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : Agent
{
    public int minTimeBeforeLeave;
    public int MaxTimeBeforeLeave;


    public override void Start()
    {
        base.Start();
        Invoke(nameof(GoHomeEarly), Random.Range(minTimeBeforeLeave, MaxTimeBeforeLeave));
    }
    public void GoHomeEarly()
    {
        personalState.ModifyState("WaitedToLong", 1);
    }
}
