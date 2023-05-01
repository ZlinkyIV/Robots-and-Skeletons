using System.Collections.Generic;
using Simulation.Internal;
using Simulation.Behaviors;

namespace Simulation.Facade
{
    /// <summary>
	///     <para>Provides an easy interface to the entire simulation.</para>
	/// </summary>
    public static class Facade
    {
        private static IDManager entityIDManager = new();
        private static Dictionary<uint, Entity> idToEntity = new();
        private static Dictionary<Entity, uint> entityToID = new();

        /// <summary>
        ///     <para>Creates a new simulation and facade to interact with it.</para>
        /// </summary>
        public static void Initiate()
        {
            EntityManager.OnSpawn += (Internal.EntitySpawnArgs args) =>
            {
                uint newID = entityIDManager.GetNewID();
                idToEntity.Add(newID, args.entity);
                entityToID.Add(args.entity, newID);

                OnEntitySpawn?.Invoke(new()
                {
                    id = newID
                });
            };

            EntityManager.OnSpawn += (Internal.EntitySpawnArgs args) => OnEntitySpawnBehavior?.Invoke(args, new EntityActions());
            EntityManager.OnDestroy += (Internal.EntityDestroyArgs args) => OnEntityDestroyBehavior?.Invoke(args, new EntityActions());
            EntityManager.OnHealDamage += (Internal.EntityHealDamageArgs args) => OnEntityHealDamageBehavior?.Invoke(args, new EntityActions());
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
        public static void SpawnEntity(IEntityBehavior behavior)
        {
            OnEntitySpawnBehavior += behavior.OnSpawn;
            OnEntityDestroyBehavior += behavior.OnDestroy;
            OnEntityHealDamageBehavior += behavior.OnHealDamage;

            ActionHandler.SpawnEntity(new SpawnEntityRequest
            {
                
            });
        }

        /// <summary>
        ///     <para>Destroys Entity of corresponding ID.</para>
        /// </summary>
        /// <param name="id">ID of Entity to destroy.</param>
        public static void DestroyEntity(uint id)
        {
            EntityManager.Destroy(idToEntity[id]);
            idToEntity.Remove(id);
            entityIDManager.FreeID(id);
        }


        public delegate void FacadeEventHandler<TArgs>(TArgs args);

        public static event FacadeEventHandler<OnEntitySpawnArgs> OnEntitySpawn;
        public static event FacadeEventHandler<OnEntityDestroyArgs> OnEntityDestroy;
        public static event FacadeEventHandler<OnEntityHealDamageArgs> OnEntityHealDamage;


        /// <summary>
        ///     <para>A custom event handler for triggering Entity behavior.</para>
        /// </summary>
        /// <param name="args">An struct (or class) for context, piped directly from the EntityManager events.</param>
        /// <param name="actions">A struct of methods the Entity Behavior script can call in response.</param>
        private delegate void EntityBehaviorEventHandler<TArgs>(TArgs args, EntityActions actions);

        private static event EntityBehaviorEventHandler<Behaviors.EntitySpawnArgs> OnEntitySpawnBehavior;
        private static event EntityBehaviorEventHandler<Behaviors.EntityDestroyArgs> OnEntityDestroyBehavior;
        private static event EntityBehaviorEventHandler<Behaviors.EntityHealDamageArgs> OnEntityHealDamageBehavior;
    }

    public struct OnEntitySpawnArgs
    {
        public uint id;
    }

    public struct OnEntityDestroyArgs
    {
        public uint id;
    }

    public struct OnEntityHealDamageArgs
    {
        public uint id;
    }
}