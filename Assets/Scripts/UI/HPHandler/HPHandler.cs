using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HPHandler : MonoBehaviour
{

    [SerializeField] private RectMask2D _rectMask2D;
    [SerializeField] private RectTransform _healthBarRect;
    [SerializeField] private TMP_Text _healthBarText;

    private float _maxRightMask;
    private float _initialRightMask;
    
    private IConfigProvider _configProvider;
    private IHpProvider _hpProvider;

    [Inject]
    public void Construct(IConfigProvider configProvider, IHpProvider hpProvider)
    {
        _hpProvider = hpProvider;
        _configProvider = configProvider;
    }
    
    private void Start()
    {
        _hpProvider.PlayerHpChanged += UpdateHp;
        
        _maxRightMask = _healthBarRect.rect.width - _rectMask2D.padding.x - _rectMask2D.padding.z;
        _healthBarText.SetText(_hpProvider.GetHeroMaxHp().ToString());
        _initialRightMask = _rectMask2D.padding.z;
    }

    private void SetValue(float value)
    {
        var targetWidth = value * _maxRightMask / GetHeroStats().MaxHealth;
        var newRightMask = _maxRightMask + _initialRightMask - targetWidth;
        var padding = _rectMask2D.padding;
        padding.z = newRightMask;
        _rectMask2D.padding = padding;
        _healthBarText.SetText(value.ToString(CultureInfo.InvariantCulture) + "/" + _hpProvider.GetHeroMaxHp());
    }

    private void UpdateHp()
    {
        SetValue(_hpProvider.GetHeroCurrentHp());
    }

    private HeroConfigData GetHeroStats()
    {
        return _configProvider.GetHeroConfig();
    }
}
