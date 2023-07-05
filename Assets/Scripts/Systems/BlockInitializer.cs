using Assets.Scripts.Components;
using Client;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public struct BlockInitializer : IEcsInitSystem
{
    private readonly EcsCustomInject<SharedData> _data;
    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        EcsPool<WaypointComponent> poolWaypoints = world.GetPool<WaypointComponent>();
        EcsPool<DamageComponent> poolDamage = world.GetPool<DamageComponent>();
        EcsPool<HealthComponent> poolHealth = world.GetPool<HealthComponent>();
        EcsPool<BlockViewComponent> poolView = world.GetPool<BlockViewComponent>();
        EcsPool<ColorComponent> poolColor = world.GetPool<ColorComponent>();

        for (int i = 0; i < _data.Value.countSpawn; i++)
        {
            int entity = world.NewEntity();
            poolWaypoints.Add(entity);
            poolDamage.Add(entity).damageValue = _data.Value.damage;
            poolHealth.Add(entity).health = _data.Value.health;
            poolView.Add(entity);
           ref ColorComponent cc = ref poolColor.Add(entity);
            cc.colorOrigin = Color.black;
            cc.colorTarget = Color.red;
        }
    }
}
