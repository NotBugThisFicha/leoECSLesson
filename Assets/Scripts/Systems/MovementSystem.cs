using Assets.Scripts.Components;
using Client;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    internal struct MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<WaypointComponent, BlockViewComponent>> _filterBlock;
        private readonly EcsPoolInject<WaypointComponent> _poolWaypoints;
        private readonly EcsPoolInject<BlockViewComponent> _poolView;
        public void Run(IEcsSystems systems)
        {
            foreach(int entity in _filterBlock.Value)
            {
                ref WaypointComponent wc = ref _poolWaypoints.Value.Get(entity);
                ref BlockViewComponent bv = ref _poolView.Value.Get(entity);

                bv.view.transform.position = Vector3.MoveTowards(bv.view.transform.position, wc.targetPos, Time.deltaTime);
                if ((bv.view.transform.position - wc.startPos).sqrMagnitude < 0.1f)
                    wc.targetPos = wc.endPos;
                if ((bv.view.transform.position - wc.endPos).sqrMagnitude < 0.1f)
                    wc.targetPos = wc.startPos;
            }
        }
    }
}
