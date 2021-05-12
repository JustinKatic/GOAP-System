using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class CookFood : Action
{
    public override bool OnEnter()
    {
        target = World.Instance.GetQueue(Q.FreeStove).RemoveAndReturnResource();

        if (target == null)
            return false;

        if (target != null)
            destination = target.transform.position;

        agent.SetDestination(destination);

        World.Instance.GetWorldStates().ModifyState(Q.FreeStove, -1);

        return true;
    }
    public override void OnTick()
    {
    }

    public override bool ConditionToExit()
    {
        float distToDest = Vector3.Distance(transform.position, target.transform.position);

        if (distToDest <= 2)
            return true;
        else
            return false;
    }
    public override bool OnExit()
    {       
        World.Instance.GetQueue(Q.FreeStove).AddResource(target);
        World.Instance.GetWorldStates().ModifyState(Q.FreeStove, 1);
        return true;
    }
}
