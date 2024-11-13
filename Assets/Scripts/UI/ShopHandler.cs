using System.Collections.Generic;
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
    private IHpProvider _hpProvider;

    [Inject]
    private void Construct(ICardsGenerator cardsGenerator, IProgressService progressService, IHpProvider hpProvider)
    {
        _hpProvider = hpProvider;
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
            _progressService.ProgressData.PurchasedCardCount[_cards[id].CardId] += 1;
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
            
            foreach (var stat in normalCard.Stats)
            {
                _progressService.AddStat(stat.Key, stat.Value);
            }
            
            //_hpProvider.AddHeroMaxAndCurrentHp(normalCard.MaxHealthBoost);
            
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
            Card newCard = _cardsGenerator.GenerateNormalCard(_rarenessOfCard);
            if (_progressService.ProgressData.PurchasedCardCount[newCard.CardId] >= newCard.PoolLimit)
            {
                GenerateCard(index);
            }
            _cards.Add(newCard);
            _cardHandlers[index].SetupNormalCard((NormalCardConfig)newCard, _rarenessOfCard);
        }
        else if(_cardType == CardType.Unique)
        {
            Card nc = _cardsGenerator.GenerateUniqueCard(_rarenessOfCard);
            if (nc.PoolLimit <= 0)
            {
                GenerateCard(index);
            }
            _cards.Add(nc);
            _cardHandlers[index].SetupUniqueCard((UniqueCardConfig)nc, _rarenessOfCard);
        }
    }
    private PlayerProgressData GetProgressData()
    {
        return _progressService.ProgressData;
    }
}