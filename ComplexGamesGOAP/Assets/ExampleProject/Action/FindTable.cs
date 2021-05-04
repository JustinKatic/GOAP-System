using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FindTable : Action
{

    public TextMeshProUGUI aboveHeadText;

    public override bool OnEnter()
    {
        target = World.Instance.GetQueue(Q.FreeTable).RemoveAndReturnResource();
        Debug.Log("checking for table");
        if (target == null)
            return false;

        World.Instance.GetWorldStates().ModifyState(Q.FreeTable, -1);
        aboveHeadText.text = "Finding A Table";


        return true;
    }

    public override bool OnExit()
    {
        inventory.AddItem(target);
        World.Instance.GetQueue(Q.CustomerWaitingForFood).AddResource(gameObject);
        World.Instance.GetWorldStates().ModifyState(Q.CustomerWaitingForFood, 1);
        aboveHeadText.text = "";


        return true;
    }
}
