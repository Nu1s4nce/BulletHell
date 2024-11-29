using System;

public interface IHpProvider
{
    public float GetHeroCurrentHp();
    public float GetHeroMaxHp();
    public void InitHeroHp();
    public void AddHeroMaxAndCurrentHp(float hp);
    public void AddHeroCurrentHp(float hp);
    public void RemoveHeroCurrentHp(float hp);
    public void RemoveHeroMaxHp(float hp);
    public event Action PlayerHpChanged;
}