using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoHomeAfterPayed : Action
{
    public TextMeshProUGUI aboveHeadText;

    public override bool OnEnter()
    {
        target = GameObject.FindGameObjectWithTag(T.Home);
        if (target == null)
            return false;

        aboveHeadText.text = "Going Home";


        return true;
    }

    public override bool OnExit()
    {
        aboveHeadText.text = "";
        Destroy(gameObject, 1);
        return true;
    }


}
