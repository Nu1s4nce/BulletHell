using UnityEngine;
using Zenject;

public class CameraMovement : MonoBehaviour
{
    private IHeroProvider _heroProvider;
    
    [Inject]
    public void Construct(IHeroProvider heroProvider)
    {
        _heroProvider = heroProvider;
    }

    public void Update()
    {
        transform.position = new Vector3(_heroProvider.GetHeroPosition().x, _heroProvider.GetHeroPosition().y, -10) ;
    }
}