using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedCustomer : Action
{
    GameObject food;
    public override bool OnEnter()
    {
        target = World.Instance.GetQueue(Q.CustomerWaitingForFood).RemoveAndReturnResource();
        if (target == null)
            return false;

        food = inventory.FindItemWithTag(T.Food);

        return true;
    }

    public override bool OnExit()
    {
        if (target == null)
            return false;
        target.GetComponent<Agent>().inventory.AddItem(inventory.FindItemWithTag(T.Food));
        inventory.RemoveItem(food);
        return true;
    }
}
