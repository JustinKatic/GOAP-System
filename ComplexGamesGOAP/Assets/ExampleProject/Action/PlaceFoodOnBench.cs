using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceFoodOnBench : Action
{
    public override bool OnEnter()
    {
        //target = FreeFoodServieArea
        target = World.Instance.GetQueue(Q.FreeFoodServeArea).RemoveAndReturnResource();
        if (target == null)
            return false;

        return true;
    }

    public override bool OnExit()
    {
        //set the food object to active
        target.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        //remove 1 from freeFoodServiceArea world state
        World.Instance.GetWorldStates().ModifyState(Q.FreeFoodServeArea, -1);
        //Add 1 food Obj to FreeFood queue
        World.Instance.GetQueue(Q.FreeFood).AddResource(target.gameObject.transform.GetChild(0).gameObject);
        //add 1 free food to world states
        World.Instance.GetWorldStates().ModifyState(Q.FreeFood, 1);

        return true;
    }

}
