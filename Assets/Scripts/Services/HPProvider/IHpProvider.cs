using System;

public interface IHpProvider
{
    public float GetHeroCurrentHp();
    public float GetHeroMaxHp();
    public void SetHeroHp(float hp);
    public event Action PlayerHpChanged;
}