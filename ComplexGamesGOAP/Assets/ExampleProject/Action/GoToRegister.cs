using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoToRegister : Action
{
    public TextMeshProUGUI aboveHeadText;

    public override bool OnEnter()
    {
        target = GameObject.FindGameObjectWithTag(T.ShopRegister);
        if (target == null)
            return false;


            aboveHeadText.text = "Going To Register";


        return true;
    }

    public override bool OnExit()
    {
        aboveHeadText.text = "";

        agentPersonalState.ModifyState(PS.AtShop, 1);
        return true;
    }

}
