using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DancingLineFanmade.UI
{
    [DisallowMultipleComponent]
    public class StartPage : MonoBehaviour
    {
        [SerializeField] private List<RectTransform> moveLeft;
        [SerializeField] private List<RectTransform> moveDown;

        public void Hide()
        {
            foreach (RectTransform l in moveLeft)
            {
                if (l.GetComponent<Button>()) l.GetComponent<Button>().interactable = false;
                l.DOAnchorPos(new Vector2(-120f, l.anchoredPosition.y), 0.4f).SetEase(Ease.InSine).OnComplete(() => { Destroy(gameObject); });
            }
            foreach (RectTransform d in moveDown)
            {
                if (d.GetComponent<Button>()) d.GetComponent<Button>().interactable = false;
                d.DOAnchorPos(new Vector2(d.anchoredPosition.y, -250f), 0.4f).SetEase(Ease.InSine);
            }
        }
    }
}