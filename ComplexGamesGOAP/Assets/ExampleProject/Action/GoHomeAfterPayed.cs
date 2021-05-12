using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GOAP;

public class GoHomeAfterPayed : Action
{
    public TextMeshProUGUI aboveHeadText;

    public override bool OnEnter()
    {
        target = GameObject.FindGameObjectWithTag(T.Home);
        if (target == null)
            return false;
        if (target != null)
            destination = target.transform.position;

        agent.SetDestination(destination);

        aboveHeadText.text = "Going Home";


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
        aboveHeadText.text = "";
        Destroy(gameObject, 1);
        return true;
    }


}
