using Assets.Scripts.Components;
using Client;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    internal struct WaypointSystem : IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<WaypointComponent>> filterWaypoints;
        private readonly EcsCustomInject<SharedData> _data;

        public void Init(IEcsSystems systems)
        {

            var poolWayp = filterWaypoints.Pools.Inc1;
            foreach(var entity in filterWaypoints.Value)
            {
                ref WaypointComponent wc = ref poolWayp.Get(entity);
                wc.startPos = RandomizePos();
                wc.endPos = RandomizePos();
                wc.targetPos = wc.startPos;
            }
        }

        private Vector3 RandomizePos()
        {
            return new Vector3(
                Random.Range(-_data.Value.borderX, _data.Value.borderX),
                Random.Range(-_data.Value.borderY, _data.Value.borderY), 0);
        }
    }
}
