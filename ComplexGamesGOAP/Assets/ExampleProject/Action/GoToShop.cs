using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToShop : Action
{
    public override bool OnEnter()
    {
        target = GameObject.FindGameObjectWithTag("GoToDoor");
        if (target == null)
            return false;
        World.Instance.GetWorldStates().ModifyState("Customers", 1);

        return true;
    }

    public override bool OnExit()
    {
        agentPersonalState.ModifyState("AtShop", 1);
        return true;
    }

}
