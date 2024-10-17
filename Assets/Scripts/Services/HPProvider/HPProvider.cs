using System;

public class HPProvider : IHpProvider
{
    private IConfigProvider _configProvider;
    private IProgressService _progressService;

    public event Action PlayerHpChanged;
    
    public HPProvider(IConfigProvider configProvider, IProgressService progressService)
    {
        _progressService = progressService;
        _configProvider = configProvider;
    }
    
    public void SetHeroHp(float hp)
    {
        _progressService.GetHeroData().CurrentHealth = hp;
        PlayerHpChanged?.Invoke();
    }

    public float GetHeroCurrentHp()
    {
        return _progressService.GetHeroData().CurrentHealth;
    }
    
    public float GetHeroMaxHp()
    {
        return _configProvider.GetHeroConfig().MaxHealth + _progressService.GetHeroData().MaxHealthBonus;
    }
}