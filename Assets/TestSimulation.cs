using UnityEngine;
using Simulation.Facade;
using Simulation.Behaviors;

public class TestSimulation : MonoBehaviour
{
    void Start()
    {
        Facade.Initiate();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Facade.SpawnEntity(new TestEntity());
        }
    }
}

public struct TestEntity : IEntityBehavior
{
    public void OnHealDamage(EntityHealDamageArgs args, EntityActions actions)
    {
        return;
    }

    public void OnDestroy(EntityDestroyArgs args, EntityActions actions)
    {
        Debug.Log("Entity Destroyed!");
    }

    public void OnSpawn(EntitySpawnArgs args, EntityActions actions)
    {
        Debug.Log("Entity Spawned!");
    }
}
