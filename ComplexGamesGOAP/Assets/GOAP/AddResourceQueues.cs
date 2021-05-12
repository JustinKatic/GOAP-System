using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    [System.Serializable]
    public class ResourceData
    {
        public string TagOfQueueItem;
        public string NameOfQueue;
    }

    public class AddResourceQueues : MonoBehaviour
    {
        public ResourceData[] worldResourcesInScene;
        private void Awake()
        {
            foreach (ResourceData resource in worldResourcesInScene)
            {
                World.Instance.AddResourceQueue(resource.TagOfQueueItem, resource.NameOfQueue, World.Instance.GetWorldStates());
            }
        }
    }
}
