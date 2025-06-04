using System;

public class HPProvider : IHpProvider
{
    private readonly IConfigProvider _configProvider;
    private readonly IProgressService _progressService;

    private float _currentHealth;

    public event Action PlayerHpChanged;
    public event Action PlayerDead;
    
    public HPProvider(IConfigProvider configProvider, IProgressService progressService)
    {
        _progressService = progressService;
        _configProvider = configProvider;
    }
    
    public void InitHeroHp()
    {
        _currentHealth = _configProvider.GetHeroConfig().MaxHealth + _progressService.GetHeroData().HeroStatsData[StatId.MaxHealth];
        _progressService.HPChanged += UpdateHeroHP;
        PlayerHpChanged?.Invoke();
    }

    public void AddHeroMaxAndCurrentHp(float hp)
    {
        _progressService.GetHeroData().HeroStatsData[StatId.MaxHealth] += hp;
        _currentHealth += hp;
        PlayerHpChanged?.Invoke();
    }
    public void AddHeroCurrentHp(float hp)
    {
        if(_currentHealth >= GetHeroMaxHp()) return;
        if (GetHeroMaxHp() - _currentHealth >= hp)
        {
            _currentHealth += hp;
        }
        else
        {
            _currentHealth = GetHeroMaxHp();
        }
        PlayerHpChanged?.Invoke();
    }
    public void RemoveHeroMaxHp(float hp)
    {
        _progressService.GetHeroData().HeroStatsData[StatId.MaxHealth] -= hp;
        PlayerHpChanged?.Invoke();
    }
    public void RemoveHeroCurrentHp(float hp)
    {
        _currentHealth -= hp;
        PlayerHpChanged?.Invoke();
        if(_currentHealth <= 0) PlayerDead?.Invoke();
    }

    private void UpdateHeroHP(float value)
    {
        _currentHealth += value;
        PlayerHpChanged?.Invoke();
    }

    public float GetHeroCurrentHp()
    {
        return _currentHealth;
    }
    
    public float GetHeroMaxHp()
    {
        return _configProvider.GetHeroConfig().MaxHealth + _progressService.GetHeroData().HeroStatsData[StatId.MaxHealth];
    }
}