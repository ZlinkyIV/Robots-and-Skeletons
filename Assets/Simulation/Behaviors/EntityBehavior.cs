namespace Simulation.Behaviors
{
    public interface IEntityBehavior
    {
        public void OnSpawn(EntitySpawnDetails args, EntityBehaviorActions actions);

        public void OnDestroy(EntityDestroyDetails args, EntityBehaviorActions actions);

        public void OnHealDamage(EntityHealDamageDetails args, EntityBehaviorActions actions);
    }

    public struct EntityBehaviorActions
    {

    }
}