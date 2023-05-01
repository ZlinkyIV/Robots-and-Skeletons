using System.Collections.Generic;

namespace Simulation.Internal
{
    /// <summary>
    ///     <para>Tracks all Entities in existence and provides functions such as getting the nearest one to a given point.</para>
    /// </summary>
    public static class EntityManager
    {
        private static List<Entity> allEntities = new();

        public static void Spawn(SpawnEntityRequest requestArgs)
        {
            Entity newEntity = null;

            allEntities.Add(newEntity);

            OnSpawn?.Invoke(new()
            {
                entity = newEntity
            });
        }

        public static void Destroy(Entity entity)
        {
            allEntities.Remove(entity);

            OnDestroy?.Invoke(new()
            {
                entity = entity
            });
        }

        public static void HealDamage(Entity entity, int amount)
        {
            OnHealDamage?.Invoke(new()
            {
                entity = entity,
                amount = amount
            });
        }


        public delegate void EntityEventHandler<TArgs>(TArgs args);

        public static event EntityEventHandler<EntitySpawnArgs> OnSpawn;
        public static event EntityEventHandler<EntityDestroyArgs> OnDestroy;
        public static event EntityEventHandler<EntityHealDamageArgs> OnHealDamage;
    }

    public struct EntitySpawnArgs
    {
        public Entity entity;
    }

    public struct EntityDestroyArgs
    {
        public Entity entity;
    }

    public struct EntityHealDamageArgs
    {
        public Entity entity;
        public int amount;
    }
}