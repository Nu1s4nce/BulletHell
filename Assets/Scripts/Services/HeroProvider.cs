using UnityEngine;

public class HeroProvider : IHeroProvider
{
    public GameObject Hero { get; set; }

    public Vector3 GetHeroPosition()
    {
        return Hero.transform.position;
    }
}