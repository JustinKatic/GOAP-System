using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoHomeWithoutPaying : Action
{
    public TextMeshProUGUI aboveHeadText;

    public override bool OnEnter()
    {
        target = GameObject.FindGameObjectWithTag(T.Home);
        if (target == null)
            return false;

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

        aboveHeadText.text = "Going Home Without Paying";


        return true;
    }

    public override bool OnExit()
    {
        aboveHeadText.text = "";
        Destroy(gameObject, 1);
        return true;
    }


}
