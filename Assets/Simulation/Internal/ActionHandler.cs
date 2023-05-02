using System.Collections.Generic;

namespace Simulation.Internal
{
    public static class ActionHandler
    {
        private static List<EntityHealDamageDetails> healthChangesQueue = new();
        private static List<EntitySpawnRequest> spawnQueue = new();
        private static List<EntityDestroyDetails> destroyQueue = new();

        private static Dictionary<Entity, int> compiledHealthChanges = new();


        public static void SpawnEntity(EntitySpawnRequest details) => spawnQueue.Add(details);
        public static void DestroyEntity(EntityDestroyDetails details) => destroyQueue.Add(details);
        public static void HealDamageEntity(EntityHealDamageDetails details) => healthChangesQueue.Add(details);


        public static void EnforceChanges()
        {
            CompileHealthChanges();

            ExcecuteDestroyQueue();
            ExcecuteSpawnQueue();
            ExcecuteCompiledHealthChanges();
        }


        private static void CompileHealthChanges()
        {
            foreach (var d in healthChangesQueue)
            {
                try
                {
                    compiledHealthChanges[d.entity] += d.amount;
                }
                catch (KeyNotFoundException)
                {
                    compiledHealthChanges[d.entity] = d.amount;
                }
            }

            healthChangesQueue.Clear();
        }


        private static void ExcecuteDestroyQueue()
        {
            foreach (var request in destroyQueue)
            {
                EntityManager.Destroy(request);
            }

            destroyQueue.Clear();
        }

        private static void ExcecuteSpawnQueue()
        {
            foreach (var request in spawnQueue)
            {
                EntityManager.Spawn(request);
            }
            
            spawnQueue.Clear();
        }

        private static void ExcecuteCompiledHealthChanges()
        {
            foreach (var keyValuePair in compiledHealthChanges)
            {
                EntityManager.HealDamage(new EntityHealDamageDetails()
                {
                    entity = keyValuePair.Key,
                    amount = keyValuePair.Value
                });
            }

            compiledHealthChanges.Clear();
        }
    }
}