using Simulation.Internal;
using Simulation.Behaviors;

namespace Simulation
{
    /// <summary>
	///     <para>Provides an easy interface to the entire simulation.</para>
	/// </summary>
    public static class Facade
    {
        /// <summary>
        ///     <para>Creates a new simulation and facade to interact with it.</para>
        /// </summary>
        public static void Initiate()
        {
            EntityManager.OnSpawn += (EntitySpawnDetails args) => OnEntitySpawn?.Invoke(args);
            EntityManager.OnDestroy += (EntityDestroyDetails args) => OnEntityDestroy?.Invoke(args);
            EntityManager.OnHealDamage += (EntityHealDamageDetails args) => OnEntityHealDamage?.Invoke(args);

            EntityManager.OnSpawn += (EntitySpawnDetails args) => OnEntitySpawnBehavior?.Invoke(args, new EntityBehaviorActions());
            EntityManager.OnDestroy += (EntityDestroyDetails args) => OnEntityDestroyBehavior?.Invoke(args, new EntityBehaviorActions());
            EntityManager.OnHealDamage += (EntityHealDamageDetails args) => OnEntityHealDamageBehavior?.Invoke(args, new EntityBehaviorActions());

            EntityManager.OnDestroy += (EntityDestroyDetails details) =>
            {
                OnEntitySpawnBehavior -= details.entity.behavior.OnSpawn;
                OnEntityDestroyBehavior -= details.entity.behavior.OnDestroy;
                OnEntityHealDamageBehavior -= details.entity.behavior.OnHealDamage;
            };
        }

        public static void Step()
        {
            ActionHandler.EnforceChanges();
        }

        /// <summary>
        ///     <para>Spawns an entity with specified behavior.</para>
        /// </summary>
        /// <param name="behavior">Behavior of Entity.</param>
        /// <returns>A unique ID to reference the Entity later.</returns>
        public static void SpawnEntity(EntitySpawnRequest request)
        {
            OnEntitySpawnBehavior += request.behavior.OnSpawn;
            OnEntityDestroyBehavior += request.behavior.OnDestroy;
            OnEntityHealDamageBehavior += request.behavior.OnHealDamage;

            ActionHandler.SpawnEntity(request);
        }

        /// <summary>
        ///     <para>Destroys Entity of corresponding ID.</para>
        /// </summary>
        /// <param name="id">ID of Entity to destroy.</param>
        public static void DestroyEntity(EntityDestroyDetails details)
        {
            ActionHandler.DestroyEntity(details);
        }


        public delegate void FacadeEventHandler<TArgs>(TArgs args);

        public static event FacadeEventHandler<EntitySpawnDetails> OnEntitySpawn;
        public static event FacadeEventHandler<EntityDestroyDetails> OnEntityDestroy;
        public static event FacadeEventHandler<EntityHealDamageDetails> OnEntityHealDamage;


        /// <summary>
        ///     <para>A custom event handler for triggering Entity behavior.</para>
        /// </summary>
        /// <param name="args">An struct (or class) for context, piped directly from the EntityManager events.</param>
        /// <param name="actions">A struct of methods the Entity Behavior script can call in response.</param>
        private delegate void EntityBehaviorEventHandler<TArgs>(TArgs args, EntityBehaviorActions actions);

        private static event EntityBehaviorEventHandler<EntitySpawnDetails> OnEntitySpawnBehavior;
        private static event EntityBehaviorEventHandler<EntityDestroyDetails> OnEntityDestroyBehavior;
        private static event EntityBehaviorEventHandler<EntityHealDamageDetails> OnEntityHealDamageBehavior;
    }
}