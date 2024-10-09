using TMPro;
using UnityEngine;
using Zenject;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _mainCurrencyTextContainer;
    
    
    private IProgressService _progressService;

    [Inject]
    public void Construct(IProgressService progressService)
    {
        _progressService = progressService;
    }
    void Awake()
    {
        _progressService.ProgressData.CurrencyAmountChanged += UpdateMainCurrencyText;
    }
    
    private void UpdateMainCurrencyText()
    {
        _mainCurrencyTextContainer.text = _progressService.GetMainCurrency().ToString();
    }
}
