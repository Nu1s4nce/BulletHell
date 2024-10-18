using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class ShopHandler : MonoBehaviour
{
    [SerializeField] private List<CardHandler> _cardHandlers;
    
    [SerializeField] private RefreshShopButton _refreshButton;
    
    private List<Card> _cards = new();

    private CardType _cardType;
    private RarenessOfCard _rarenessOfCard;
    
    private NormalCardConfig _normalCardConfig;
    private UniqueCardConfig _uniqueCardConfig;
    
    private ICardsGenerator _cardsGenerator;
    private IProgressService _progressService;

    [Inject]
    private void Construct(ICardsGenerator cardsGenerator, IProgressService progressService)
    {
        _progressService = progressService;
        _cardsGenerator = cardsGenerator;
    }

    private void Awake()
    {
        _refreshButton.RefreshButtonClicked += RefreshShopWithCost;
        foreach (var cardHandler in _cardHandlers)
        {
            cardHandler.CardClicked += OnCardClick;
        }
    }

    private void Start()
    {
        RefreshShop();
    }

    private void RefreshShopWithCost()
    {
        if (_progressService.GetMainCurrency() >= GetProgressData().RefreshButtonCost)
        {
            _progressService.RemoveMainCurrency(GetProgressData().RefreshButtonCost);
            RefreshShop();
            GetProgressData().RefreshButtonCost += GetProgressData().RefreshButtonCostMultiplier;
            _refreshButton.UpdateButtonCost(GetProgressData().RefreshButtonCost);
        }
    }

    private void RefreshShop()
    {
        _cards.Clear();
        for (int i = 0; i < _cardHandlers.Count; i++)
        {
            GenerateCard(i);
        }
    }
    private void OnCardClick(int id)
    {
        int cardCost = _cards[id].CardCost;
        if (cardCost <= _progressService.GetMainCurrency())
        {
            _progressService.RemoveMainCurrency(cardCost);
            AddStats(id);
            _cards.Clear();
            RefreshShop();
        }
    }
    private void AddStats(int id)
    {
        Card card = _cards[id];
        if (card.GetType() == typeof(NormalCardConfig))
        {
            NormalCardConfig normalCard = (NormalCardConfig)card;
            
            _progressService.AddProgressDamage(normalCard.DamageBoost);
            _progressService.AddProgressAttackRange(normalCard.AttackRangeBoost);
            _progressService.AddProgressAttackRate(normalCard.AttackRateBoost);
            _progressService.AddProgressMaxHealth(normalCard.MaxHealthBoost);
            _progressService.AddProgressMoveSpeed(normalCard.MoveSpeedBoost);
            _progressService.AddProgressCollectablesPickRange(normalCard.CollectablesPickRangeBoost);
            _progressService.AddProgressCollectablesValueBoost(normalCard.CollectablesValueBoost);
            _progressService.AddProgressProjectileSpeed(normalCard.ProjectileSpeedBoost);
            _progressService.AddProgresstMultiShot(normalCard.MultiShotBoost);
        }
        if (card.GetType() == typeof(UniqueCardConfig))
        {
            UniqueCardConfig uniqueCard = (UniqueCardConfig)card;
        }
    }
    

    private void GenerateCard(int index)
    {
        _cardType = _cardsGenerator.GetTypeOfCardToGenerate();
        _rarenessOfCard = _cardsGenerator.GetRandomRarenessOfCard();
        
        if (_cardType == CardType.Normal)
        {
            Card nc = _cardsGenerator.GenerateNormalCard(_rarenessOfCard);
            if(nc.CardsInPool <= 0) GenerateCard(index);
            _cards.Add(nc);
            _cardHandlers[index].SetupNormalCard((NormalCardConfig)nc, _rarenessOfCard);
        }
        else if(_cardType == CardType.Unique)
        {
            Card nc = _cardsGenerator.GenerateUniqueCard(_rarenessOfCard);
            if(nc.CardsInPool <= 0) GenerateCard(index);
            _cards.Add(nc);
            _cardHandlers[index].SetupUniqueCard((UniqueCardConfig)nc, _rarenessOfCard);
        }
    }
    private PlayerProgressData GetProgressData()
    {
        return _progressService.ProgressData;
    }
}