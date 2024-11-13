using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class CardHandler : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    private RectTransform _rectTransform;

    [SerializeField] private TMP_Text _cardNameText;
    [SerializeField] private TMP_Text _cardDescriptionText;
    [SerializeField] private TMP_Text _cardCostText;
    [SerializeField] private Image _cardIcon;
    [SerializeField] private Image _cardIconBackground;
    [SerializeField] private Image _cardBorderIcon;
    [SerializeField] private Image _cardCostIcon;

    [SerializeField] private int _cardId;

    private Dictionary<StatId, string> _statsTextPresentation = new()
    {
        {StatId.Damage, "Урон"},
        {StatId.MaxHealth, "Макс. HP"},
        {StatId.MoveSpeed, "Скорость"},
        {StatId.AttackRange, "Радиус атаки"},
        {StatId.AttackRate, "Скорость атаки"},
        {StatId.ProjectileSpeed, "Скорость снарядов"},
        {StatId.CollectablesPickRange, "Дальность сбора"},
        {StatId.CollectablesValue, "Валюта"},
        {StatId.MultiShotTargets, "Количество целей"},
    };

    private int _cardCost;

    private Card _card;

    private ICardsGenerator _cardsGenerator;
    private IConfigProvider _configProvider;

    public event Action<int> CardClicked;

    [Inject]
    public void Construct(ICardsGenerator cardsGenerator, IConfigProvider configProvider)
    {
        _configProvider = configProvider;
        _cardsGenerator = cardsGenerator;
    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _rectTransform.transform.DOScale(1.1f, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _rectTransform.transform.DOScale(1f, 0.2f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CardClicked?.Invoke(_cardId);
    }

    public void SetupNormalCard(NormalCardConfig cardConfig, RarenessOfCard rarenessOfCard)
    {
        _card = cardConfig;
        SetupNormalCardUI(cardConfig, rarenessOfCard);
    }

    public void SetupUniqueCard(UniqueCardConfig cardConfig, RarenessOfCard rarenessOfCard)
    {
        _card = cardConfig;
        SetupUniqueCardUI(cardConfig, rarenessOfCard);
    }

    private void SetupNormalCardUI(NormalCardConfig cardConfig, RarenessOfCard rarenessOfCard)
    {
        _cardNameText.text = cardConfig.CardName;
        HandleNormalCardStatsToShow(cardConfig, _cardDescriptionText);

        _cardCostText.text = _card.CardCost.ToString();


        _cardIcon.sprite = cardConfig.CardImage;
        _cardIconBackground.color = _cardsGenerator.GetColorByRareness(rarenessOfCard).PrimaryColor;
        _cardNameText.color = _cardsGenerator.GetColorByRareness(rarenessOfCard).PrimaryColor;

        if (cardConfig.CardBorder != null)
        {
            _cardBorderIcon.sprite = cardConfig.CardBorder;
            _cardBorderIcon.color = _cardsGenerator.GetColorByRareness(rarenessOfCard).SecondaryColor;
            _cardBorderIcon.gameObject.SetActive(true);
        }
        else _cardBorderIcon.gameObject.SetActive(false);

        
        _cardCostIcon.sprite = _configProvider.GetCurrenciesConfig().CurrenciesConfig[cardConfig.CurrencyType];
    }

    private void SetupUniqueCardUI(UniqueCardConfig cardConfig, RarenessOfCard rarenessOfCard)
    {
        _cardNameText.text = cardConfig.CardName;
        HandleUniqueCardStatsToShow(cardConfig, _cardDescriptionText);

        _cardCostText.text = _card.CardCost.ToString();

        _cardIcon.sprite = cardConfig.CardImage;
        _cardIconBackground.color = _cardsGenerator.GetColorByRareness(rarenessOfCard).PrimaryColor;
        _cardNameText.color = _cardsGenerator.GetColorByRareness(rarenessOfCard).PrimaryColor;

        if (cardConfig.CardBorder != null)
        {
            _cardBorderIcon.sprite = cardConfig.CardBorder;
            _cardBorderIcon.color = _cardsGenerator.GetColorByRareness(rarenessOfCard).SecondaryColor;
            _cardBorderIcon.gameObject.SetActive(true);
        }
        else _cardBorderIcon.gameObject.SetActive(false);

        _cardCostIcon.sprite = _configProvider.GetCurrenciesConfig().CurrenciesConfig[cardConfig.CurrencyType];
    }


    private void HandleNormalCardStatsToShow(NormalCardConfig normalCardConfig, TMP_Text descriptionText)
    {
        descriptionText.text = "";
        foreach (var stat in normalCardConfig.Stats)
        {
            descriptionText.text += _statsTextPresentation[stat.Key] + " + " + stat.Value + "\n";
        }
    }

    private void HandleUniqueCardStatsToShow(UniqueCardConfig uniqueCardConfig, TMP_Text descriptionText)
    {
        descriptionText.text = "";
        if (uniqueCardConfig.WeaponType != 0)
        {
        }
    }
}