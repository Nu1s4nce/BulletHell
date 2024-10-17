using System;
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

    private int _cardCost;

    private Card _card;
    //private UniqueCardConfig _uniqueCard;

    private ICardsGenerator _cardsGenerator;
    private IProgressService _progressService;

    public event Action<int> CardClicked;

    [Inject]
    public void Construct(ICardsGenerator cardsGenerator, IProgressService progressService)
    {
        _progressService = progressService;
        _cardsGenerator = cardsGenerator;
    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _rectTransform.transform.DOScale(1.1f, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _rectTransform.transform.DOScale(1f, 0.2f);
    }

    private void UpdateScale(float progress)
    {
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

        _cardCostIcon.sprite = cardConfig.CardCostImage;
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

        _cardCostIcon.sprite = cardConfig.CardCostImage;
    }


    private void HandleNormalCardStatsToShow(NormalCardConfig normalCardConfig, TMP_Text descriptionText)
    {
        descriptionText.text = "";
        if (normalCardConfig.DamageBoost != 0)
        {
            descriptionText.text += "Damage + " + normalCardConfig.DamageBoost + "\n";
        }

        if (normalCardConfig.MaxHealthBoost != 0)
        {
            descriptionText.text += "Health + " + normalCardConfig.MaxHealthBoost + "\n";
        }

        if (normalCardConfig.AttackRangeBoost != 0)
        {
            descriptionText.text += "Attack range + " + normalCardConfig.AttackRangeBoost + "\n";
        }

        if (normalCardConfig.AttackRateBoost != 0)
        {
            descriptionText.text += "Attack rate + " + normalCardConfig.AttackRateBoost + "\n";
        }

        if (normalCardConfig.ProjectileSpeedBoost != 0)
        {
            descriptionText.text += "Projectile speed + " + normalCardConfig.ProjectileSpeedBoost + "\n";
        }

        if (normalCardConfig.MultiShotBoost != 0)
        {
            descriptionText.text += "Multishot targets + " + normalCardConfig.MultiShotBoost + "\n";
        }

        if (normalCardConfig.MoveSpeedBoost != 0)
        {
            descriptionText.text += "Move speed + " + normalCardConfig.MoveSpeedBoost + "\n";
        }

        if (normalCardConfig.CollectablesPickRangeBoost != 0)
        {
            descriptionText.text += "Pick-up range + " + normalCardConfig.CollectablesPickRangeBoost + "\n";
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