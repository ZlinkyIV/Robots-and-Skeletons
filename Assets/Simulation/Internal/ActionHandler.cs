using System.Collections.Generic;

namespace Simulation.Internal
{
    public static class ActionHandler
    {
        private static List<KeyValuePair<Entity, int>> healthChangesQueue = new();
        private static List<SpawnEntityRequest> spawnQueue = new();

        private static Dictionary<Entity, int> compiledHealthChanges = new();

        public static void EnforceChanges()
        {
            CompileHealthChangesQueue();

            ExcecuteSpawnQueue();
            ExcecuteCompiledHealthChanges();
        }

        public static void SpawnEntity(SpawnEntityRequest request)
        {
            spawnQueue.Add(request);
        }

        public static void HealDamage(Entity entity, int amount)
        {
            healthChangesQueue.Add(new KeyValuePair<Entity, int>(entity, amount));
        }


        private static void CompileHealthChangesQueue()
        {
            foreach (var keyValuePair in healthChangesQueue)
            {
                Entity entity = keyValuePair.Key;
                int amount = keyValuePair.Value;

                try
                {
                    compiledHealthChanges[entity] += amount;
                }
                catch (KeyNotFoundException)
                {
                    compiledHealthChanges[entity] = amount;
                }
            }

            healthChangesQueue.Clear();
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
                Entity entity = keyValuePair.Key;
                int amount = keyValuePair.Value;

                EntityManager.HealDamage(entity, amount);
            }

            compiledHealthChanges.Clear();
        }
    }
}