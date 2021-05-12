using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GOAP;


public class PayRegister : Action
{
    public IntSO money;
    public int foodCost;

    public TextMeshProUGUI aboveHeadText;


    public override bool OnEnter()
    {
        target = GameObject.FindGameObjectWithTag(T.ShopRegister);
        if (target == null)
            return false;

        if (target != null)
            destination = target.transform.position;

        agent.SetDestination(destination);

        aboveHeadText.text = "Paying At Register";


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
        World.Instance.GetWorldStates().ModifyState(WS.CustomersInStore, -1);
        aboveHeadText.text = "";
        agentPersonalState.ModifyState(PS.AtShop, -1);
        money.value += foodCost;
        return true;
    }
}
