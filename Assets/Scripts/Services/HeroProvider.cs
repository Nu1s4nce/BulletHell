using UnityEngine;

public class HeroProvider : IHeroProvider
{
    public GameObject HeroPosition { get; set; }

    public Vector3 GetHeroPosition()
    {
        return HeroPosition.transform.position;
    }
}