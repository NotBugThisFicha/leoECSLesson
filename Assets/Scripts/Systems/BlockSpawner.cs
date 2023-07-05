using Assets.Scripts.Components;
using Client;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    internal struct BlockSpawner : IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<WaypointComponent, BlockViewComponent, ColorComponent>> _filterBlock;
        private readonly EcsCustomInject<SharedData> _data;
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var poolWayP = _filterBlock.Pools.Inc1;
            var poolBlockView = _filterBlock.Pools.Inc2;
            var poolBlockColor = _filterBlock.Pools.Inc3;

            foreach(int entity in _filterBlock.Value)
            {
                ref var wayPointC = ref poolWayP.Get(entity);
                ref var viewC = ref poolBlockView.Get(entity);
                ref var colorC = ref poolBlockColor.Get(entity);

                var GO = 
                    Object.Instantiate(
                        Resources.Load<ECSMonoObject>(_data.Value.path),
                        wayPointC.targetPos, 
                        Quaternion.identity);

                GO.Init(world);
                GO.PackEntity(entity);
                viewC.view = GO.gameObject;
                colorC.renderer = GO.GetComponent<MeshRenderer>();
            }
        }
    }
}
