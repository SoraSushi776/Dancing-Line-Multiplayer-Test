
using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ScrollRect))]
public class ShowHideScrollBar : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{

    private ScrollRect thisScrollRect;
    private Scrollbar HScrollBar;
    private CanvasGroup HCanvasGroup;
    private Scrollbar VScrollBar;
    private CanvasGroup VCanvasGroup;
    
    private float alpha = 0f;
    public float activeSpeed = 1f;
    public float waitTime = 1f;
    public float inactiveSpeed = 2f;

    void Start()
    {
        thisScrollRect = GetComponent<ScrollRect>();

        HScrollBar = (thisScrollRect.horizontal) ? thisScrollRect.horizontalScrollbar : null;
        VScrollBar = (thisScrollRect.vertical) ? thisScrollRect.verticalScrollbar : null;
        
        if (HScrollBar != null)
        {

            HCanvasGroup = HScrollBar.GetComponent<CanvasGroup>();
            if (HCanvasGroup == null)
            {
                HCanvasGroup = HScrollBar.gameObject.AddComponent<CanvasGroup>();
            }
        }

        if (VScrollBar != null)
        {
            VCanvasGroup = VScrollBar.GetComponent<CanvasGroup>();
            if (VCanvasGroup == null)
            {
                VCanvasGroup = VScrollBar.gameObject.AddComponent<CanvasGroup>();
            }
        }

        InitalCoroutine();
    }

    
    public void InitalCoroutine()
    {
        if (!thisScrollRect.IsActive())
        {
            return;
        }

        StopCoroutine("ShowHideSB");
        StartCoroutine("ShowHideSB");
    }


    /// <summary>
    /// Raises the drag event.
    /// </summary>
    /// <param name="data">Data.</param>
    public void OnDrag(PointerEventData data)
    {
        if (!thisScrollRect.IsActive())
        {
            return;
        }

        StopCoroutine("ShowHideSB");
        StartCoroutine("ShowHideSB");
    }

    /// <summary>
    /// Raises the pointer up event.
    /// </summary>
    /// <param name="data">Data.</param>
    public void OnPointerUp(PointerEventData data)
    {
        if (!thisScrollRect.IsActive())
        {
            return;
        }

        StopCoroutine("ShowHideSB");
        StartCoroutine("ShowHideSB");
    }

    /// <summary>
    /// Raises the pointer down event.
    /// </summary>
    /// <param name="data">Data.</param>
    public void OnPointerDown(PointerEventData data)
    {
        if (!thisScrollRect.IsActive())
        {
            return;
        }

        StopCoroutine("ShowHideSB");
        StartCoroutine("ShowHideSB");
    }
    
    private IEnumerator ShowHideSB()
    {

        while (alpha < 1f)
        {
            alpha += (activeSpeed * 0.02f);
            if (HCanvasGroup != null)
            {
                HCanvasGroup.alpha = alpha;
            }
            if (VCanvasGroup != null)
            {
                VCanvasGroup.alpha = alpha;
            }
            yield return null;
        }
        alpha = 1f;
        if (HCanvasGroup != null)
        {
            HCanvasGroup.alpha = alpha;
        }
        if (VCanvasGroup != null)
        {
            VCanvasGroup.alpha = alpha;
        }

        yield return new WaitForSeconds(waitTime);

        while (alpha > 0f)
        {
            alpha -= (inactiveSpeed * 0.02f);
            if (HCanvasGroup != null)
            {
                HCanvasGroup.alpha = alpha;
            }
            if (VCanvasGroup != null)
            {
                VCanvasGroup.alpha = alpha;
            }
            yield return null;
        }
        alpha = 0f;
        if (HCanvasGroup != null)
        {
            HCanvasGroup.alpha = alpha;
        }
        if (VCanvasGroup != null)
        {
            VCanvasGroup.alpha = alpha;
        }
    }



}