using Simulation.Behaviors;

namespace Simulation.Facade
{
    /// <summary>
	///     <para>Provides an easy interface to the entire simulation.</para>
	/// </summary>
    public class Facade
    {
        public static void SpawnEntity(IEntityBehavior behavior)
        {
            OnEntitySpawn += behavior.OnSpawn;
            OnEntityDestroy += behavior.OnDestroy;
            OnEntityDamage += behavior.OnDamage;
            OnEntityHeal += behavior.OnHeal;
        }

        /// <summary>
        ///     <para>A custom event handler for triggering Entity behavior.</para>
        /// </summary>
        /// <param name="args">An struct (or class) for context, piped directly from the EntityManager events.</param>
        /// <param name="actions">A struct of methods the Entity Behavior script can call in response.</param>
        public delegate void EntityBehaviorEventHandler<TArgs>(TArgs args, EntityActions actions);

        public static event EntityBehaviorEventHandler<OnEntitySpawnArgs> OnEntitySpawn;
        public static event EntityBehaviorEventHandler<OnEntityDestroyArgs> OnEntityDestroy;
        public static event EntityBehaviorEventHandler<OnEntityDamageArgs> OnEntityDamage;
        public static event EntityBehaviorEventHandler<OnEntityHealArgs> OnEntityHeal;
    }
    
    public struct EntityActions
    {

    }

    public struct OnEntitySpawnArgs
    {

    }

    public struct OnEntityDestroyArgs
    {

    }

    public struct OnEntityDamageArgs
    {

    }

    public struct OnEntityHealArgs
    {

    }
}