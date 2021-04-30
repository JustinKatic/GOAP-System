using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToTable : Action
{
    public override bool OnEnter()
    {
        target = World.Instance.GetQueue(ResourceQueueSO.Queues.Table.ToString()).RemoveAndReturnResource();
        if (target == null)
            return false;

        World.Instance.GetWorldStates().ModifyState(ResourceQueueSO.Queues.Table.ToString(), -1);
        return true;
    }

    public override bool OnExit()
    {
        World.Instance.GetQueue(ResourceQueueSO.Queues.Table.ToString()).AddResource(target);
        World.Instance.GetWorldStates().ModifyState(ResourceQueueSO.Queues.Table.ToString(), 1);
        //int value = World.Instance.GetWorldStates().worldStatesDictionary[ResourceQueueSO.Queues.Table.ToString()];
        //    Debug.Log(value);
        return true;
    }
}
