using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DancingLineFanmade.Animated
{
    public class LocalAnimatorTrigger : MonoBehaviour
    {
        public GameObject[] objects;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                foreach (GameObject obj in objects)
                {
                    if(obj.GetComponent<LocalPosAnimator>() != null)
                    {
                        obj.GetComponent<LocalPosAnimator>().Trigger();
                    }
                    if (obj.GetComponent<LocalRotAnimator>() != null)
                    {
                        obj.GetComponent<LocalRotAnimator>().Trigger();
                    }
                    if (obj.GetComponent<LocalScaleAnimator>() != null)
                    {
                        obj.GetComponent<LocalScaleAnimator>().Trigger();
                    }
                }
            }
        }
    }
}