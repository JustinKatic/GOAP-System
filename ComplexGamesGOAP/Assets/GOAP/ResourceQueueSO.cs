using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "queue",menuName = "ResourceQueues")]
public class ResourceQueueSO : ScriptableObject,ISerializationCallbackReceiver
{
    public enum Queues
    {
        Table,
    }

    private void Awake()
    {
        
    }

    [HideInInspector] public Queues[] queueList;

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        queueList = (Queues[])Enum.GetValues(typeof(Queues));           
    }
}
