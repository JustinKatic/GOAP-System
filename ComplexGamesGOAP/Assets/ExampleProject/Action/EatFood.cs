using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GOAP;

public class EatFood : Action
{

    public override bool OnEnter()
    {
  

        target = inventory.FindItemWithTag(T.Table);

        if (!inventory.FindItemWithTag(T.Food) || target == null)
            return false;

        if (target != null)
            destination = target.transform.position;

        agent.SetDestination(destination);

  

        World.Instance.GetQueue(Q.CustomerWaitingForFood).RemoveResource(gameObject);
        World.Instance.GetWorldStates().ModifyState(Q.CustomerWaitingForFood, -1);

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
        agentPersonalState.ModifyState("FinishedEating", 1);
        World.Instance.GetQueue(Q.FreeTable).AddResource(target);
        World.Instance.GetWorldStates().ModifyState(Q.FreeTable, 1);
        inventory.RemoveItem(target);

        inventory.RemoveItem(inventory.FindItemWithTag(T.Food));
        return true;
    }
}
