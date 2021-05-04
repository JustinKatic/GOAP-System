using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookFood : Action
{
    public override bool OnEnter()
    {
        target = World.Instance.GetQueue(Q.FreeStove).RemoveAndReturnResource();

        if (target == null)
            return false;

        World.Instance.GetWorldStates().ModifyState(Q.FreeStove, -1);

        return true;
    }

    public override bool OnExit()
    {       
        World.Instance.GetQueue(Q.FreeStove).AddResource(target);
        World.Instance.GetWorldStates().ModifyState(Q.FreeStove, 1);
        return true;
    }
}
