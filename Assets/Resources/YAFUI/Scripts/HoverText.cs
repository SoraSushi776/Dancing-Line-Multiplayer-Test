using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using ExitGames.Client.Photon.StructWrapping;

public class HoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hoverText;

    private void Start()
    {
        if (hoverText.GetComponent<CanvasGroup>() == null) hoverText.AddComponent<CanvasGroup>();

        hoverText.GetComponent<CanvasGroup>().alpha = 0;
        hoverText.GetComponent<CanvasGroup>().blocksRaycasts = false;
        hoverText.SetActive(false);
        hoverText.transform.localPosition = new Vector3(hoverText.transform.localPosition.x, hoverText.transform.localPosition.y - 3f, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverText.SetActive(true);

        hoverText.transform.DOLocalMoveY(hoverText.transform.localPosition.y + 3f, 0.2f);
        hoverText.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverText.transform.DOLocalMoveY(hoverText.transform.localPosition.y - 3f, 0.2f);
        hoverText.GetComponent<CanvasGroup>().DOFade(0, 0.2f).OnComplete(() => hoverText.SetActive(false));
    }
}
