using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform _rectTransform;


    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _rectTransform.transform.DOScale(1.1f, 0.2f);
        //DOVirtual.Float(0f, 1f, 0.4f, UpdateScale);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DOTween.KillAll();
        _rectTransform.transform.DOScale(1f, 0.2f);
    }

    private void UpdateScale(float progress)
    {
        
    }
}