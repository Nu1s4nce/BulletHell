using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

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
    private IScoreService _scoreService;
    private ITimeService _time;
    private ISoundManager _soundManager;
    private IConfigProvider _configProvider;

    [Inject]
    private void Construct(ICardsGenerator cardsGenerator, IProgressService progressService, IScoreService scoreService,
        ITimeService timeService, ISoundManager soundManager, IConfigProvider configProvider)
    {
        _configProvider = configProvider;
        _soundManager = soundManager;
        _time = timeService;
        _scoreService = scoreService;
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

    private void OnEnable()
    {
        _time.PauseGame();
    }

    private void OnDisable()
    {
        _time.ResumeGame();
    }

    private void Start()
    {
        RefreshShop();
        _refreshButton.UpdateButtonCost(GetLevelConfigData().rerollCost +
                                        _progressService.ProgressData.rerollCostMultiplier);
    }

    private void RefreshShopWithCost()
    {
        int rerollCost = GetLevelConfigData().rerollCost + _progressService.ProgressData.rerollCostMultiplier;
        if (_progressService.GetMainCurrency() >= rerollCost)
        {
            _progressService.RemoveMainCurrency(rerollCost);
            RefreshShop();
            GetProgressData().rerollCostMultiplier += 2;
            _refreshButton.UpdateButtonCost(rerollCost);
        }
    }

    private void RefreshShop()
    {
        _soundManager.PlayRefreshShop();
        _cards.Clear();
        for (int i = 0; i < _cardHandlers.Count; i++)
        {
            GenerateCard(i);
        }
    }

    private void OnCardClick(int id)
    {
        int cardCost = 0;
        if (_cardType == CardType.Normal)
        {
            cardCost = _cards[id].CardCost - _cards[id].CardCost * _progressService.ProgressData.shopDiscount / 100;
        }
        else if (_cardType == CardType.Unique)
        {
            cardCost = _cards[id].CardCost;
        }

        if (cardCost <= _progressService.GetMainCurrency())
        {
            _progressService.RemoveMainCurrency(cardCost);
            _progressService.ProgressData.PurchasedCardCount[_cards[id].CardId] += 1;

            _scoreService.AddScore(_cards[id].ScorePoints);

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
            NormalCardConfig normalCard = (NormalCardConfig) card;

            foreach (var stat in normalCard.Stats)
            {
                _progressService.AddStat(stat.Key, stat.Value);
            }
        }

        if (card.GetType() == typeof(UniqueCardConfig))
        {
            UniqueCardConfig uniqueCard = (UniqueCardConfig) card;
            if (uniqueCard.CardId == 400) //Карта на скидку
            {
                _progressService.ProgressData.shopDiscount += 5;
            }

            if (uniqueCard.CardId == 401) //Mask of madness
            {
                int damage = Random.Range(-50, 51);
                int attackSpeed = Random.Range(-100, 101);

                _progressService.AddStat(StatId.Damage, damage);
                _progressService.AddStat(StatId.AttackSpeed, attackSpeed);
            }
        }
    }

    private void GenerateCard(int index)
    {
        _cardType = _cardsGenerator.GetTypeOfCardToGenerate();

        if (_cardType == CardType.Normal)
        {
            Card newCard = _cardsGenerator.GenerateNormalCard();

            _cards.Add(newCard);
            _cardHandlers[index].SetupNormalCard((NormalCardConfig) newCard);
        }
        else if (_cardType == CardType.Unique)
        {
            Card newCard = _cardsGenerator.GenerateUniqueCard(_rarenessOfCard);

            _cards.Add(newCard);
            _cardHandlers[index].SetupUniqueCard((UniqueCardConfig) newCard);
        }
    }

    private PlayerProgressData GetProgressData()
    {
        return _progressService.ProgressData;
    }
    private LevelConfigData GetLevelConfigData()
    {
        return _configProvider.LevelConfig;
    }
}