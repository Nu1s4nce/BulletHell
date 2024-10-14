using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShopHandler : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> _cardNameTexts;
    [SerializeField] private List<TMP_Text> _cardDescriptionTexts;
    [SerializeField] private List<TMP_Text> _cardCostTexts;
    [SerializeField] private List<Image> _cardIcons;
    [SerializeField] private List<Image> _cardBorderIcons;
    [SerializeField] private List<Image> _cardCostIcons;
    
    [SerializeField] private RectTransform _refreshButton;
    
    private ICardsGenerator _cardsGenerator;

    private NormalCardConfig _normalCardConfig;
    private UniqueCardConfig _uniqueCardConfig;

    [Inject]
    private void Construct(ICardsGenerator cardsGenerator)
    {
        _cardsGenerator = cardsGenerator;
    }

    private void Start()
    {
        RefreshShop();
    }

    private void RefreshShop()
    {
        for (int i = 0; i < _cardNameTexts.Count; i++)
        {
            GenerateCard(i);
        }
    }

    private void GenerateCard(int index)
    {
        //Получаю тип карты
        CardType cardType = _cardsGenerator.GetTypeOfCardToGenerate();
        
        //В зависимости от типа получаю соответствующий конфиг
        if (cardType == CardType.Normal)
        {
            SetupNormalCardUI(_cardsGenerator.GenerateNormalCard(), index);
        }
        else if(cardType == CardType.Unique)
        {
            HandleUniqueCardStatsToShow(_cardsGenerator.GenerateUniqueCard());
        }
    }

    private void SetupNormalCardUI(NormalCardConfig cardConfig, int ind)
    {
        _cardNameTexts[ind].text = cardConfig.CardName;
        HandleNormalCardStatsToShow(cardConfig, _cardDescriptionTexts[ind]);
        _cardCostTexts[ind].text = cardConfig.CardCost.ToString();
        
        _cardIcons[ind].sprite = cardConfig.CardImage;
        _cardBorderIcons[ind].sprite = cardConfig.CardBorder;
        _cardCostIcons[ind].sprite = cardConfig.CardCostImage;
    }

    private void HandleNormalCardStatsToShow(NormalCardConfig normalCardConfig, TMP_Text descriptionText)
    {
        if (normalCardConfig.DamageBoost != 0)
        {
            descriptionText.text += "Damage + " + normalCardConfig.DamageBoost + "\n";
        }
        if (normalCardConfig.HealthBoost != 0)
        {
            descriptionText.text += "Health + " + normalCardConfig.HealthBoost + "\n";
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
    }
    private void HandleUniqueCardStatsToShow(UniqueCardConfig uniqueCardConfig)
    {
        if (uniqueCardConfig.WeaponType != 0)
        {
            
        }
    }
    
}