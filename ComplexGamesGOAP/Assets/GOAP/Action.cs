using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Action : MonoBehaviour
{
    // Name of the action
    [HideInInspector] public string actionName;
    // Cost of the action
    public float costOfAction = 1.0f;
    // Duration the action should take
    public float durationToCompleteAction = 0.0f;
    // Target where the action is going to take place
    [HideInInspector] public GameObject target;
    // checks if this action is the current active action
    [HideInInspector] public bool IsActionActive = false;


    // nav agent attached to gameobject
    [HideInInspector] public NavMeshAgent agent;

    //array of WorldStates requiredConditions
    public WorldState[] requiredConditions;
    //array of WorldStates of effectOnCompletion
    public WorldState[] effectOnCompletion;

    // Dictionary of requiredConditions
    public Dictionary<string, int> requiredConditionsDictionary;
    // Dictionary of effectsOnCompletion
    public Dictionary<string, int> effectsOnCompletionDictionary;
    // Access our inventory
    public Inventory inventory;
    //agents personalState 
    public WorldStates agentPersonalState;



    // Constructor
    public Action()
    {
        // creates dictionaries that hold our requiredConditions and EffectsOnCompleteion
        requiredConditionsDictionary = new Dictionary<string, int>();
        effectsOnCompletionDictionary = new Dictionary<string, int>();
    }


    private void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        //add requiredConditions[] to the requiredConditionsDictionary.
        if (requiredConditions != null)
        {
            foreach (WorldState w in requiredConditions)
            {
                requiredConditionsDictionary.Add(w.action, w.cost);
            }
        }

        //add effectsOnCompletion to the effectsOnCompletionDictionary
        if (effectOnCompletion != null)
        {
            foreach (WorldState w in effectOnCompletion)
            {
                effectsOnCompletionDictionary.Add(w.action, w.cost);
            }
        }


        // gets refrence to inventory on agent
        inventory = GetComponent<Agent>().inventory;

        // Gets refrence to agents personal states
        agentPersonalState = GetComponent<Agent>().personalState;
    }


    //checks if conditions passed in match all the required conditions
    public bool IsAhievableGiven(Dictionary<string, int> conditions)
    {
        foreach (KeyValuePair<string, int> requiredCondition in requiredConditionsDictionary)
        {
            if (!conditions.ContainsKey(requiredCondition.Key))
            {
                return false;
            }
        }
        return true;
    }


    //Abstract classes that are required when we inherit from this class
    public abstract bool OnEnter(); // called when we enter this action
    public abstract bool OnExit(); //called when we exit this action
}

