using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AgentDebugger : ScriptableWizard
{
    Vector2 scrollPos = Vector2.zero;

    [MenuItem("Window/Agent Debugger")]
    public static void ShowWindow()
    {
        GetWindow<AgentDebugger>();
    }

    private void OnGUI()
    {
        GameObject agent = Selection.activeGameObject;

        if (Selection.activeGameObject == null || agent.GetComponent<Agent>() == null)
            return;




        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.ExpandHeight(true));

        EditorGUILayout.LabelField("Agent Name: ", agent.name);


        if (agent.gameObject.GetComponent<Agent>().currentAction != null)
            EditorGUILayout.LabelField("Current Action: ", agent.gameObject.GetComponent<Agent>().currentAction.ToString());



        GUILayout.Label("Agents Goals: ");


        foreach (KeyValuePair<SubGoal, int> goal in agent.gameObject.GetComponent<Agent>().goalsDictionary)
        {
            EditorGUILayout.BeginVertical("box");
            foreach (KeyValuePair<string, int> subGoal in goal.Key.sGoals)
            {
                GUILayout.Label(subGoal.Key);
            }
            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.Space(25);

        GUI.color = Color.green;
        EditorGUILayout.LabelField("Current Action Plan: ", EditorStyles.boldLabel);

        foreach (var s in agent.GetComponent<Agent>().actionPlan)
        {
            EditorGUILayout.LabelField(s.actionName);
        }
        GUI.color = Color.white;
        EditorGUILayout.Space(50);


        EditorGUILayout.LabelField("List Of Possible Actions:", EditorStyles.boldLabel);
        foreach (Action action in agent.gameObject.GetComponent<Agent>().actions)
        {
            EditorGUILayout.BeginVertical("box");
            string requiredConditions = "";
            string effectsOnCompletion = "";

            if (action.requiredConditionsDictionary.Count > 0)
                foreach (KeyValuePair<string, int> requiredCondition in action.requiredConditionsDictionary)
                {
                    requiredConditions += requiredCondition.Key + ", ";
                }
            else
                requiredConditions = "None";

            if (action.effectsOnCompletionDictionary.Count > 0)
                foreach (KeyValuePair<string, int> effect in action.effectsOnCompletionDictionary)
                {
                    effectsOnCompletion += effect.Key + ", ";
                }
            else
                effectsOnCompletion = "None";


            EditorGUILayout.LabelField("Action Name: ", action.actionName);
            if (action.costOfAction.ToString() != null)
                EditorGUILayout.LabelField("Action Cost: ", action.costOfAction.ToString());
            if (action.durationToCompleteAction.ToString() != null)
                EditorGUILayout.LabelField("Duration Of Action: ", action.durationToCompleteAction.ToString());
            if (action.target != null)
                EditorGUILayout.LabelField("tag Of Destination Target: ", action.target.name);
            if (requiredConditions != null)
                EditorGUILayout.LabelField("Required Conditions: ", requiredConditions);
            if (effectsOnCompletion != null)
                EditorGUILayout.LabelField("Effects On Completion: ", effectsOnCompletion);

            EditorGUILayout.EndVertical();
        }





        EditorGUILayout.Space(25);


        GUI.color = Color.green;

        EditorGUILayout.LabelField(" Agent Personal States: ", EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical("box");
        if (agent.gameObject.GetComponent<Agent>().personalState.GetStates().Count <= 0)
            EditorGUILayout.LabelField("Agent has no current personal states");
        else
        {
            foreach (KeyValuePair<string, int> personalState in agent.gameObject.GetComponent<Agent>().personalState.GetStates())
            {
                EditorGUILayout.LabelField(personalState.Key);
            }
        }
        EditorGUILayout.EndVertical();




        EditorGUILayout.LabelField("Agent Inventory: ", EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical("box");
        if (agent.gameObject.GetComponent<Agent>().inventory.objectsInInventoryList.Count <= 0)
            EditorGUILayout.LabelField("Agent has no current items in inventory");
        else
        {
            foreach (GameObject gameObject in agent.gameObject.GetComponent<Agent>().inventory.objectsInInventoryList)
            {
                EditorGUILayout.LabelField(gameObject.tag);
            }
        }
        EditorGUILayout.EndVertical();



        EditorGUILayout.LabelField("Current World States: ", EditorStyles.boldLabel);
        foreach (KeyValuePair<string, int> s in World.Instance.GetWorldStates().GetStates())
        {
            EditorGUILayout.LabelField(s.Key + "  " + s.Value.ToString());
        }

        EditorGUILayout.EndScrollView();

    }
}
