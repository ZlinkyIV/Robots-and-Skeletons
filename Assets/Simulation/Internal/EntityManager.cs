using System.Collections.Generic;

namespace Simulation.Internal
{
    /// <summary>
    ///     <para>Tracks all Entities in existence and provides functions such as getting the nearest one to a given point.</para>
    /// </summary>
    public static class EntityManager
    {
        private static List<Entity> allEntities = new();

        public static void Spawn(EntitySpawnRequest request)
        {
            Entity newEntity = new() { behavior = request.behavior};

            allEntities.Add(newEntity);

            OnSpawn?.Invoke(new EntitySpawnDetails()
            {
                entity = newEntity
            });
        }

        public static void Destroy(EntityDestroyDetails details)
        {
            allEntities.Remove(details.entity);

            OnDestroy?.Invoke(details);
        }

        public static void HealDamage(EntityHealDamageDetails details)
        {
            OnHealDamage?.Invoke(details);
        }


        public delegate void EntityEventHandler<TArgs>(TArgs args);

        public static event EntityEventHandler<EntitySpawnDetails> OnSpawn;
        public static event EntityEventHandler<EntityDestroyDetails> OnDestroy;
        public static event EntityEventHandler<EntityHealDamageDetails> OnHealDamage;
    }
}