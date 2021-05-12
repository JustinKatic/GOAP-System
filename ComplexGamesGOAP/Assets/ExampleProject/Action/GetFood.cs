using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class GetFood : Action
{
    public override bool OnEnter()
    {

        if (agentPersonalState.HasState("HasFood"))
            return false;

        target = World.Instance.GetQueue(Q.FreeFood).RemoveAndReturnResource();
        if (target == null)
            return false;

        if (target != null)
            destination = target.transform.position;

        agent.SetDestination(destination);

        //Remove 1 freeFood fromWorldStates
        World.Instance.GetWorldStates().ModifyState(Q.FreeFood, -1);

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
        //add 1 to FreeFoodServeArea world states
        World.Instance.GetWorldStates().ModifyState(Q.FreeFoodServeArea, 1);

        //Add FreeFoodServeArea back to Queue.
        World.Instance.GetQueue(Q.FreeFoodServeArea).AddResource(target.transform.parent.gameObject);

        target.gameObject.SetActive(false);


        //if food isnt in inventory add it to inventory
        if (!inventory.FindItemWithTag(T.Food))
        {
            inventory.AddItem(target);
            agentPersonalState.ModifyState("HasFood", 0);
        }




        return true;
    }
}
