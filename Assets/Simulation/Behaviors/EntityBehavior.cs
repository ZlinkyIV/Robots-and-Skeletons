using Simulation.Facade;

namespace Simulation.Behaviors
{
    public interface IEntityBehavior
    {
        public void OnSpawn(OnEntitySpawnArgs args, EntityActions actions);

        public void OnDestroy(OnEntityDestroyArgs args, EntityActions actions);

        public void OnDamage(OnEntityDamageArgs args, EntityActions actions);

        public void OnHeal(OnEntityHealArgs args, EntityActions actions);
    }
}