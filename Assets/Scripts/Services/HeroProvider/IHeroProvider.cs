using UnityEngine;

public interface IHeroProvider
{
    public GameObject Hero { get; set; }
    public Vector3 GetHeroPosition();
}