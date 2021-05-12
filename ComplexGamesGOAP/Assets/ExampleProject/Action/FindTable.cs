using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GOAP;

public class FindTable : Action
{



    public override bool OnEnter()
    {
        target = World.Instance.GetQueue(Q.FreeTable).RemoveAndReturnResource();
        if (target == null)
            return false;

        if (target != null)
            destination = target.transform.position;

        agent.SetDestination(destination);

        World.Instance.GetWorldStates().ModifyState(Q.FreeTable, -1);



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
        inventory.AddItem(target);
        World.Instance.GetQueue(Q.CustomerWaitingForFood).AddResource(gameObject);
        World.Instance.GetWorldStates().ModifyState(Q.CustomerWaitingForFood, 1);



        return true;
    }
}
