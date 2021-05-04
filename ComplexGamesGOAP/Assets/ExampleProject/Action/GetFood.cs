using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFood : Action
{
    public override bool OnEnter()
    {

        target = World.Instance.GetQueue(Q.FreeFood).RemoveAndReturnResource();
        if (target == null)
            return false;

        //Remove 1 freeFood fromWorldStates
        World.Instance.GetWorldStates().ModifyState(Q.FreeFood, -1);

        return true;
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
            inventory.AddItem(target);




        return true;
    }
}
