namespace Simulation.Internal
{
    /// <summary>
    ///     <para>Represents a thing that exists in the world that can be spawned, damaged, etc.</para>
    /// </summary>
    public interface IEntity
    {
        public void Spawn();

        public void Destroy();

        public void Damage();

        public void Heal();
    }
}