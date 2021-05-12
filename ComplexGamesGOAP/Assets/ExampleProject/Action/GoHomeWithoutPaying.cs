using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GOAP;
public class GoHomeWithoutPaying : Action
{

    public override bool OnEnter()
    {
        target = GameObject.FindGameObjectWithTag(T.Home);
        if (target == null)
            return false;

        if (target != null)
            destination = target.transform.position;

        agent.SetDestination(destination);

        GameObject table = inventory.FindItemWithTag(T.Table);
        if (table != null)
        {
            World.Instance.GetQueue(Q.FreeTable).AddResource(table);
            World.Instance.GetWorldStates().ModifyState(Q.FreeTable, 1);
            inventory.RemoveItem(table);
        }
        if (inventory.FindItemWithTag(T.Food) != null)
        {
            inventory.RemoveItem(inventory.FindItemWithTag(T.Food));
        }
        else
        {
            World.Instance.GetWorldStates().ModifyState(Q.CustomerWaitingForFood, -1);
        }


        World.Instance.GetQueue(Q.CustomerWaitingForFood).RemoveResource(gameObject);



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
        Destroy(gameObject, 1);
        return true;
    }


}
