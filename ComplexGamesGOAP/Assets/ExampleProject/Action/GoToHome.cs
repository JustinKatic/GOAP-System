using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToHome : Action
{
    public override bool OnEnter()
    {
        target = GameObject.FindGameObjectWithTag("GoToHome");
        if (target == null)
            return false;
        agentPersonalState.ModifyState("AtShop", -1);

        return true;
    }

    public override bool OnExit()
    {
        Destroy(gameObject, 1);
        return true;
    }


}
