using Assets.Scripts.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    internal struct ColorizeSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HitComponent>> _filterHits;
        private readonly EcsPoolInject<HitComponent> _poolHits;
        private readonly EcsPoolInject<ColorComponent> _poolColor;
        private readonly EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach(var entity in _filterHits.Value)
            {
                ref HitComponent hitC = ref _poolHits.Value.Get(entity);
                int entitiesCollide =
                    PackerEntityUtils.UnpackEntities(_world.Value, hitC.firstCollide.ecsPacked);

                ref ColorComponent cc = ref _poolColor.Value.Get(entitiesCollide);
                ColorizeCor(cc.colorOrigin, cc.colorTarget, cc.renderer.material);
            }
        }

        private async void ColorizeCor(Color cO, Color cT, Material mat)
        {
            mat.color = cT;
            await Task.Delay(500);
            mat.color = cO;
        }
    }
}
