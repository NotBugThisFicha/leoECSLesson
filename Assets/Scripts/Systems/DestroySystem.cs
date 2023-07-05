using Assets.Scripts.Components;
using Client;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    internal struct DestroySystemL: IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<BlockViewComponent, HealthComponent>> _filterHealth;
        private readonly EcsPoolInject<BlockViewComponent> _poolView;
        private readonly EcsPoolInject<HealthComponent> _poolHealth;
        private readonly EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach(var entity in _filterHealth.Value)
            {
                ref var viewC = ref _poolView.Value.Get(entity);
                var healthC = _poolHealth.Value.Get(entity);

                if(healthC.health <= 0)
                {
                    Object.DestroyImmediate(viewC.view);
                    _world.Value.DelEntity(entity);
                }
            }
        }
    }
}
