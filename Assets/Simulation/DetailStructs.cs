namespace Simulation
{
    public struct EntitySpawnDetails
    {
        public Internal.Entity entity;
    }

    public struct EntityDestroyDetails
    {
        public Internal.Entity entity;
    }

    public struct EntityHealDamageDetails
    {
        public Internal.Entity entity;
        public int amount;
    }

    public struct EntitySpawnRequest
    {
        public Behaviors.IEntityBehavior behavior;
    }
}