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
    [SerializeField] private TMP_Text _cardRarenessText;
    [SerializeField] private Image _cardIcon;
    [SerializeField] private Image _cardIconBackground;
    [SerializeField] private Image _cardBorderIcon;
    [SerializeField] private Image _cardCostIcon;
    [SerializeField] private Image _cardEffect;

    [SerializeField] private int _cardId;

    private Dictionary<StatId, string> _statsTextPresentation = new()
    {
        {StatId.Damage, "Урон"},
        {StatId.MaxHealth, "Макс. HP"},
        {StatId.MoveSpeed, "Скорость передвижения"},
        {StatId.AttackRange, "Радиус атаки"},
        {StatId.AttackRate, "Базовый интервал атак"},
        {StatId.AttackSpeed, "Скорость атаки"},
        {StatId.ProjectileSpeed, "Скорость снарядов"},
        {StatId.CollectablesPickRange, "Дальность сбора"},
        {StatId.CollectablesValue, "Валюта"},
        {StatId.MultiShotTargets, "Количество целей"},
    };
    private Dictionary<RarenessOfCard, string> _rarenessTextPresentation = new()
    {
        {RarenessOfCard.Common, "Обычная"},
        {RarenessOfCard.Rare, "Редкая"},
        {RarenessOfCard.Mythic, "Мистическая"},
        {RarenessOfCard.Legendary, "Легендарная"},
        {RarenessOfCard.Rainbow, "Радужная"},
    };

    private int _cardCost;

    private Card _card;

    private ICardsGenerator _cardsGenerator;
    private IConfigProvider _configProvider;
    private IProgressService _progressService;

    public event Action<int> CardClicked;

    [Inject]
    public void Construct(ICardsGenerator cardsGenerator, IConfigProvider configProvider, IProgressService progressService)
    {
        _progressService = progressService;
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

    public void SetupNormalCard(NormalCardConfig cardConfig)
    {
        _card = cardConfig;
        RarenessOfCard rarenessOfCard = default;
        foreach (var kvp in _configProvider.GetCardsConfig().AllNormalCardsByRareness)
        {
            if (kvp.Value.Contains(cardConfig))
            {
                rarenessOfCard = kvp.Key;
                break; 
            }
        }
        SetupNormalCardUI(cardConfig, rarenessOfCard);
    }

    public void SetupUniqueCard(UniqueCardConfig cardConfig)
    {
        _card = cardConfig;
        RarenessOfCard rarenessOfCard = default;
        foreach (var kvp in _configProvider.GetCardsConfig().AllUniqueCardsByRareness)
        {
            if (kvp.Value.Contains(cardConfig))
            {
                rarenessOfCard = kvp.Key;
                break; 
            }
        }
        SetupUniqueCardUI(cardConfig);
    }

    private void SetupNormalCardUI(NormalCardConfig cardConfig, RarenessOfCard rarenessOfCard)
    {
        HandleNormalCardStatsToShow(cardConfig, _cardDescriptionText);
        
        CardsRarenessColors cardsColors = _cardsGenerator.GetColorByRareness(rarenessOfCard);
        
        //текст редкости
        _cardRarenessText.text = _rarenessTextPresentation[rarenessOfCard];
        _cardRarenessText.color = cardsColors.PrimaryColor;
        //иконка
        _cardIcon.sprite = cardConfig.CardImage;
        //задний фон иконки
        _cardIconBackground.color = cardsColors.PrimaryColor;
        //название карточки и цвет
        _cardNameText.text = cardConfig.CardName;
        _cardNameText.color = cardsColors.SecondaryColor;
        //шейдер
        _cardEffect.material = new Material(cardsColors.RarenessMaterial)
        {
            color = cardsColors.MaterialColor
        };
        //бордер
        if (cardConfig.CardBorder)
        {
            _cardBorderIcon.sprite = cardConfig.CardBorder;
            _cardBorderIcon.color = cardsColors.BorderColor;
            _cardBorderIcon.gameObject.SetActive(true);
        }
        else _cardBorderIcon.gameObject.SetActive(false);

        //стоимость
        
        _cardCostText.text = (_card.CardCost - _card.CardCost * _progressService.ProgressData.shopDiscount / 100).ToString();
        _cardCostIcon.sprite = _configProvider.GetCurrenciesConfig().CurrenciesConfig[cardConfig.CurrencyType];
    }

    private void SetupUniqueCardUI(UniqueCardConfig cardConfig)
    {
        HandleUniqueCardStatsToShow(cardConfig, _cardDescriptionText);
        CardsRarenessColors cardsColors = _cardsGenerator.GetUniqueCardColor();
        
        //текст редкости
        _cardRarenessText.text = "Unique";
        _cardRarenessText.color = cardsColors.PrimaryColor;
        //иконка
        _cardIcon.sprite = cardConfig.CardImage;
        //задний фон иконки
        _cardIconBackground.color = cardsColors.PrimaryColor;
        //название карточки и цвет
        _cardNameText.text = cardConfig.CardName;
        _cardNameText.color = cardsColors.SecondaryColor;

        //шейдер
        _cardEffect.material = new Material(cardsColors.RarenessMaterial)
        {
            color = cardsColors.MaterialColor
        };
        
        if (cardConfig.CardBorder)
        {
            _cardBorderIcon.sprite = cardConfig.CardBorder;
            _cardBorderIcon.color = cardsColors.BorderColor;
            _cardBorderIcon.gameObject.SetActive(true);
        }
        else _cardBorderIcon.gameObject.SetActive(false);

        //стоимость
        _cardCostText.text = _card.CardCost.ToString();
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
        descriptionText.text = uniqueCardConfig.Description;
    }
}