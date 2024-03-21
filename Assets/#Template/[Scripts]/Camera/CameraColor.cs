using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DancingLineFanmade.Level
{
    public class CameraColor : MonoBehaviour
    {
        public bool canBeTriggered = true;
        public Color color;
        public float duration = 1f;
        public Ease ease = Ease.InOutSine;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && canBeTriggered)
            {
                Camera.main.DOColor(color, duration).SetEase(ease);
            }
        }
    }
}
