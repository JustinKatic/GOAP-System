using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class FeedCustomer : Action
{
    GameObject food;
    public override bool OnEnter()
    {
        target = World.Instance.GetQueue(Q.CustomerWaitingForFood).RemoveAndReturnResource();
        if (target == null)
            return false;

        if (target != null)
            destination = target.transform.position;

        agent.SetDestination(destination);

        food = inventory.FindItemWithTag(T.Food);

        return true;
    }

    public override void OnTick()
    {
        
    }

    public override bool ConditionToExit()
    {
        if (target == null)
            return true;

        float distToDest = Vector3.Distance(transform.position, target.transform.position);

        if (distToDest <= 4)
            return true;
        else
            return false;
    }

    public override bool OnExit()
    {
        if (target == null)
            return false;
        target.GetComponent<Agent>().inventory.AddItem(inventory.FindItemWithTag(T.Food));
        inventory.RemoveItem(food);
        agentPersonalState.RemoveState("HasFood");
        return true;
    }
}
