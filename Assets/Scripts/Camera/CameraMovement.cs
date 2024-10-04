using UnityEngine;
using Zenject;

public class CameraMovement : MonoBehaviour
{
    private IHeroProvider _heroProvider;
    private Vector3 _heroPosition;
    
    [Inject]
    public void Construct(IHeroProvider heroProvider)
    {
        _heroProvider = heroProvider;
    }

    public void Update()
    {
        _heroPosition = _heroProvider.GetHeroPosition();
        transform.position = new Vector3(_heroPosition.x, _heroPosition.y, -10) ;
    }
    
    
}