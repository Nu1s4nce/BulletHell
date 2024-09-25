using UnityEngine;

public interface IHeroProvider
{
    public GameObject HeroPosition { get; set; }
    public Vector3 GetHeroPosition();
}