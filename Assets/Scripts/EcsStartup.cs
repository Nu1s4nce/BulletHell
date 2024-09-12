using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


sealed class EcsStartup : MonoBehaviour
{
    [SerializeField] SceneData _sceneData;
    [SerializeField] Configuration _configuration;
    EcsWorld _world;
    IEcsSystems _systems;

    void Start()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);
        
        var inputService = new InputService();
        
        _systems
            // register your systems here, for example:
            .Add (new HeroInitSystem())
            .Add (new MovementSystem())
            // .Add (new TestSystem2 ())

            // register additional worlds here, for example:
            // .AddWorld (new EcsWorld (), "events")
#if UNITY_EDITOR
            // add debug systems for custom worlds here, for example:
            // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
            .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
            .Inject (_sceneData, inputService)
            .Init();
    }

    void Update()
    {
        _systems?.Run();
    }

    void OnDestroy()
    {
        if (_systems != null)
        {
            // list of custom worlds will be cleared
            // during IEcsSystems.Destroy(). so, you
            // need to save it here if you need.
            _systems.Destroy();
            _systems = null;
        }

        // cleanup custom worlds here.

        // cleanup default world.
        if (_world != null)
        {
            _world.Destroy();
            _world = null;
        }
    }
}