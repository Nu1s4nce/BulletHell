using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class UserKeyboardInputSystem  : IEcsRunSystem
{
    EcsFilter _inputComponent = default;
    //private EcsFilterInject<Inc<Unit, ControlledByPlayer>> _units = default;
    
    public void Run(IEcsSystems systems)
    {
        
        foreach (var unit in _inputComponent)
        {
            var vertInput = Input.GetAxisRaw ("Vertical");
            var horizInput = Input.GetAxisRaw ("Horizontal");
            
            
        }
    }
}