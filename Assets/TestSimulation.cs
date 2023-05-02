using System.Collections.Generic;
using UnityEngine;
using Simulation;
using Simulation.Behaviors;

public class TestSimulation : MonoBehaviour
{
    List<Simulation.Internal.Entity> entities = new();

    void Start()
    {
        Facade.Initiate();

        Facade.OnEntitySpawn += (EntitySpawnDetails details) => entities.Add(details.entity);
        Facade.OnEntityDestroy += (EntityDestroyDetails details) => entities.Remove(details.entity);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Facade.Step();
        }

        if (Input.GetKeyDown(KeyCode.Comma))
        {
            Debug.Log("Entity Spawn Requested");

            Facade.SpawnEntity(new EntitySpawnRequest()
            {
                behavior = new TestEntity()
            });
        }

        if (Input.GetKeyDown(KeyCode.Period) && entities.ToArray().Length > 0)
        {
            Debug.Log("Entity Destroy Requested");

            Facade.DestroyEntity(new EntityDestroyDetails
            {
                entity = entities[0]
            });
        }
    }
}

public class TestEntity : IEntityBehavior
{
    public void OnSpawn(EntitySpawnDetails details, EntityBehaviorActions actions)
    {
        if (details.entity.behavior == this) Debug.Log("Entity Spawned!");
    }

    public void OnDestroy(EntityDestroyDetails details, EntityBehaviorActions actions)
    {
        if (details.entity.behavior == this) Debug.Log("Entity Destroyed!");
    }

    public void OnHealDamage(EntityHealDamageDetails details, EntityBehaviorActions actions)
    {
        
    }
}
