using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    [System.Serializable]
    public class Resource
    {
        public string TagOfQueueItem;
        public string NameOfQueue;
    }

    public class AddQueues : MonoBehaviour
    {
        public Resource[] worldResourcesToAdd;
        private void Awake()
        {
            foreach (Resource resource in worldResourcesToAdd)
            {
                World.Instance.AddResourceQueue(resource.TagOfQueueItem, resource.NameOfQueue, World.Instance.GetWorldStates());
            }
        }
    }
}
