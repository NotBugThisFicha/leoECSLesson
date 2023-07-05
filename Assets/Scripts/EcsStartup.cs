using Assets.Scripts.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System;
using UnityEngine;

namespace Client {
    sealed class EcsStartup : MonoBehaviour {
        EcsWorld _world;        
        IEcsSystems _systems;
        [SerializeField] private SharedData _data;

        void Start () {
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
            _systems
                // register your systems here, for example:
                .Add(new BlockInitializer())
                .Add(new WaypointSystem())
                .Add(new BlockSpawner())
                .Add(new MovementSystem())
                .Add(new ColorizeSystem())
                .Add(new DamageSystem())
                .Add(new DestroySystemL())
                // register additional worlds here, for example:
                // .AddWorld (new EcsWorld (), "events")
#if UNITY_EDITOR
                // add debug systems for custom worlds here, for example:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
#endif
                .Inject()
                .Inject(_data)
                .Init ();
        }

        void Update () {
            // process systems here.
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_systems != null) {
                // list of custom worlds will be cleared
                // during IEcsSystems.Destroy(). so, you
                // need to save it here if you need.
                _systems.Destroy ();
                _systems = null;
            }
            
            // cleanup custom worlds here.
            
            // cleanup default world.
            if (_world != null) {
                _world.Destroy ();
                _world = null;
            }
        }
    }
}

[Serializable]
public class SharedData
{
    public float borderX;
    public float borderY;

    public int countSpawn;
    public string path;

    public int damage;
    public int health;
}