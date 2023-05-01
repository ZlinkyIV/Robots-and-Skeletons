using NUnit.Framework;
using Simulation.Facade;
using Simulation.Behaviors;

public class Test_EntireSimulation
{
    [Test]
    public void Test_EntireSimulationSimplePasses()
    {
        Facade.Initiate();

        Facade.SpawnEntity(new TestEntity());
    }

    private class TestEntity : IEntityBehavior
    {
        public void OnHealDamage(EntityHealDamageArgs args, EntityActions actions)
        {
            return;
        }

        public void OnDestroy(EntityDestroyArgs args, EntityActions actions)
        {

        }

        public void OnSpawn(EntitySpawnArgs args, EntityActions actions)
        {
            
        }
    }
}
