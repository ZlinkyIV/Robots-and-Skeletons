namespace Simulation.Internal
{
    /// <summary>
    ///     <para>Represents a thing that exists in the world that can be spawned, damaged, etc.</para>
    /// </summary>
    public class Entity
    {
        public Behaviors.IEntityBehavior behavior;
    }
}