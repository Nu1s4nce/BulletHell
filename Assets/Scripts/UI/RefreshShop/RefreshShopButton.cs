using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RefreshShopButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text _refreshButtonCostText;
    private RectTransform _icon;
    
    public event Action RefreshButtonClicked;

    private void Awake()
    {
        _icon = gameObject.GetComponent<RectTransform>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        RefreshButtonClicked?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _icon.transform.DOScale(1.1f, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _icon.transform.DOScale(1.0f, 0.2f);
    }

    public void UpdateButtonCost(int cost)
    {
        _refreshButtonCostText.text = cost.ToString();
    }
}
