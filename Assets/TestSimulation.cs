using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Simulation;
using Simulation.Behaviors;

public class TestSimulation : MonoBehaviour
{
    [SerializeField] private GameObject testCreaturePrefab;

    Dictionary<Simulation.Internal.Entity, GameObject> entities = new();


    void Start()
    {
        Facade.Initiate();

        Facade.OnEntitySpawn += (EntitySpawnDetails details) =>
        {
            GameObject newEntity = Instantiate(testCreaturePrefab, Random.insideUnitCircle * Random.Range(1f, 5f), Quaternion.identity);
            entities.Add(details.entity, newEntity);
        };
        Facade.OnEntityDestroy += (EntityDestroyDetails details) =>
        {
            Destroy(entities[details.entity]);
            entities.Remove(details.entity);
        };
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

        if (Input.GetKeyDown(KeyCode.Period) && entities.Keys.Count > 0)
        {
            Debug.Log("Entity Destroy Requested");

            Facade.DestroyEntity(new EntityDestroyDetails
            {
                entity = entities.First().Key
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
