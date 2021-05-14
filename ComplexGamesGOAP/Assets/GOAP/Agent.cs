﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GOAP
{
    public class SubGoal
    {
        // Dictionary to store our goals
        public Dictionary<string, int> sGoals;

        // Bool to store if goal should be removed after it has been achieved
        public bool remove;
        // Constructor
        public SubGoal(string goalName, int goalValue, bool shouldRemove)
        {
            sGoals = new Dictionary<string, int>();
            sGoals.Add(goalName, goalValue);
            remove = shouldRemove;
        }
    }

    [System.Serializable]
    public class Goals
    {
        [Tooltip("name of the goal agent want to acheive")]
        public string goal;
        [Tooltip("value of the goal can be used as a reward for complete etc..")]
        public int goalValue;
        [Tooltip("if the agent should remove this goal if it completes it once or keep this goal")]
        public bool shouldRemoveOnComplete;
        [Tooltip("the priority the agent will pick this goal. higher priority goals are attempted to complete first")]
        public int priority;
    }

    [ExecuteInEditMode]
    public class Agent : MonoBehaviour
    {
        // Store our list of actions
        [HideInInspector] public List<Action> actions = new List<Action>();
        // Dictionary of subgoals
        public Dictionary<SubGoal, int> goalsDictionary = new Dictionary<SubGoal, int>();
        // Our inventory
        public Inventory inventory = new Inventory();
        // Our beliefs
        public WorldStates agentPersonalState = new WorldStates();

        // Access the planner
        GPlanner planner;
        // Action Queue
        public Queue<Action> actionQueue;

        //used to create a list to display our current plan of actions to get to goal.
        [HideInInspector] public List<Action> actionPlan = new List<Action>();

        // Our current action
        [HideInInspector] public Action currentAction;
        // Our subgoal
        SubGoal currentGoal;


        [SerializeField] Goals[] myGoals;



        public virtual void Start()
        {
            Action[] acts = GetComponents<Action>();

            actions.Clear();
            foreach (Action a in acts)
            {
                actions.Add(a);
                a.actionName = a.GetType().Name;
            }

            for (int i = 0; i < myGoals.Length; i++)
            {
                Goals g = myGoals[i];
                SubGoal sg = new SubGoal(g.goal, g.goalValue, g.shouldRemoveOnComplete);
                goalsDictionary.Add(sg, g.priority);
            }
        }




        //an invoked fuction to allow the agent to be perform task for a set duration
        void CompleteAction()
        {
            if (currentAction != null)
            {
                currentAction.IsActionActive = false;
                currentAction.OnExit();
            }
        }


        void LateUpdate()
        {
            //if there's a current action and it is still running
            if (currentAction != null && currentAction.IsActionActive)
            {
                // Find the distance to the target
                // Check the agent has a goal and has reached that goal
                if (currentAction.ConditionToExit())
                {
                    CompleteAction();
                    return;
                }

                if (currentAction.IsActionActive == true)
                {
                    currentAction.OnTick();
                }

                else
                    planner = null;
                return;
            }

            // Check we have a planner and an actionQueue
            if (planner == null || actionQueue == null)
            {
                planner = new GPlanner();

                // Sort the goals in descending order and store them in sortedGoals
                var sortedGoals = from entry in goalsDictionary orderby entry.Value descending select entry;
                //look through each goal to find one that has an achievable plan
                foreach (KeyValuePair<SubGoal, int> sg in sortedGoals)
                {
 
                    // If actionQueue is not = null then we must have a plan
                    actionQueue = planner.plan(actions, sg.Key.sGoals, agentPersonalState);
                    if (actionQueue != null)
                    {
                        // Set the current goal
                        currentGoal = sg.Key;
                        break;
                    }
                }

                actionPlan.Clear();
                if (actionQueue != null)
                {
                    foreach (var action in actionQueue)
                    {
                        actionPlan.Add(action);
                    }
                }
            }


            // Have we an actionQueue
            if (actionQueue != null && actionQueue.Count == 0)
            {
                // Check if currentGoal is removable
                if (currentGoal.remove)
                {
                    // Remove it
                    goalsDictionary.Remove(currentGoal);
                }
                // Set planner = null so it will trigger a new one
                planner = null;
            }


            // Do we still have actions
            if (actionQueue != null && actionQueue.Count > 0)
            {
                // Remove the top action of the queue and put it in currentAction
                currentAction = actionQueue.Dequeue();
                if (currentAction.OnEnter())
                {
                    // Activate the current action
                    currentAction.IsActionActive = true;
                }
                else
                {
                    actionQueue = null;
                }
            }
        }
    }
}
