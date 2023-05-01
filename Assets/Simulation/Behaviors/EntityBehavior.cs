namespace Simulation.Behaviors
{
    public interface IEntityBehavior
    {
        public void OnSpawn(EntitySpawnArgs args, EntityActions actions);

        public void OnDestroy(EntityDestroyArgs args, EntityActions actions);

        public void OnHealDamage(EntityHealDamageArgs args, EntityActions actions);
    }

    public struct EntityActions
    {

    }

    public struct EntitySpawnArgs
    {
        public static implicit operator EntitySpawnArgs(Internal.EntitySpawnArgs e) => new();
    }

    public struct EntityDestroyArgs
    {
        public static implicit operator EntityDestroyArgs(Internal.EntityDestroyArgs e) => new();
    }

    public struct EntityHealDamageArgs
    {
        public static implicit operator EntityHealDamageArgs(Internal.EntityHealDamageArgs e) => new();
    }
}