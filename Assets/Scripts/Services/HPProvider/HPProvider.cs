using System;

public class HPProvider : IHpProvider
{
    private IConfigProvider _configProvider;

    public event Action PlayerHpChanged;
    
    public HPProvider(IConfigProvider configProvider)
    {
        _configProvider = configProvider;
    }
    
    public void SetHeroHp(float hp)
    {
        _configProvider.GetHeroConfig().CurrentHealth = hp;
        PlayerHpChanged?.Invoke();
    }

    public float GetHeroCurrentHp()
    {
        return _configProvider.GetHeroConfig().CurrentHealth;
    }
    
    public float GetHeroMaxHp()
    {
        return _configProvider.GetHeroConfig().MaxHealth;
    }
}