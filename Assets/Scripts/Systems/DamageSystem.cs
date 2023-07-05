using Assets.Scripts.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Systems
{
    internal struct DamageSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HitComponent>> _filterHits;
        private readonly EcsPoolInject<HitComponent> _poolHits;
        private readonly EcsPoolInject<DamageComponent> _poolDamage;
        private readonly EcsPoolInject<HealthComponent> _poolHealth;
        private readonly EcsWorldInject _world;
        public void Run(IEcsSystems systems)
        {
           foreach(var entity in _filterHits.Value)
            {
                ref HitComponent hitC = ref _poolHits.Value.Get(entity);

                (int, int) entitiesCollide =
                        PackerEntityUtils.UnpackEntities(_world.Value, hitC.firstCollide.ecsPacked, hitC.secondCollide.ecsPacked);

                ref DamageComponent damageC = ref _poolDamage.Value.Get(entitiesCollide.Item2);
                ref HealthComponent healthC = ref _poolHealth.Value.Get(entitiesCollide.Item1);

                healthC.health -= damageC.damageValue;

                _world.Value.DelEntity(entity);
            }
        }
    }
}
