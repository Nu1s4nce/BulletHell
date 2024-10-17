using System;
using TMPro;
using UnityEngine;
using Zenject;

public class CurrencyUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _mainCurrencyTextContainer;
    
    private IProgressService _progressService;

    [Inject]
    public void Construct(IProgressService progressService)
    {
        _progressService = progressService;
    }
    private void Awake()
    {
        _progressService.CurrencyAmountChanged += UpdateMainCurrencyText;
    }

    private void Start()
    {
        _mainCurrencyTextContainer.text = _progressService.GetMainCurrency().ToString();
    }

    private void UpdateMainCurrencyText()
    {
        _mainCurrencyTextContainer.text = _progressService.GetMainCurrency().ToString();
    }
}
