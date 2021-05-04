using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


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

        aboveHeadText.text = "Paying At Register";


        return true;
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
