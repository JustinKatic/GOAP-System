using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EatFood : Action
{
    public TextMeshProUGUI aboveHeadText;
    public override bool OnEnter()
    {
        aboveHeadText.text = "Waiting For Food";

        target = inventory.FindItemWithTag(T.Table);

        if (!inventory.FindItemWithTag(T.Food) || target == null)
            return false;

        aboveHeadText.text = "Eating";

        World.Instance.GetQueue(Q.CustomerWaitingForFood).RemoveResource(gameObject);
        World.Instance.GetWorldStates().ModifyState(Q.CustomerWaitingForFood, -1);

        return true;
    }

    public override bool OnExit()
    {
        agentPersonalState.ModifyState("FinishedEating", 1);
        World.Instance.GetQueue(Q.FreeTable).AddResource(target);
        World.Instance.GetWorldStates().ModifyState(Q.FreeTable, 1);
        inventory.RemoveItem(target);
        aboveHeadText.text = "";
        inventory.RemoveItem(inventory.FindItemWithTag(T.Food));
        return true;
    }
}
