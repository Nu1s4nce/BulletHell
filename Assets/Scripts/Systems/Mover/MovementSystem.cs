using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class MovementSystem : IEcsRunSystem
{
    private EcsFilterInject<Inc<Unit>> _unitsToMove;
    
    private EcsCustomInject<InputService> _inputService;
    
    public void Run(IEcsSystems systems)
    {
        foreach (var unit in _unitsToMove.Value)
        {
            ref var unitToMove = ref _unitsToMove.Pools.Inc1.Get(unit);

            unitToMove.Transform.position += 
                new Vector3(_inputService.Value.Horizontal, _inputService.Value.Vertical, 0) * unitToMove.MoveSpeed * Time.deltaTime;

        }
    }
}
