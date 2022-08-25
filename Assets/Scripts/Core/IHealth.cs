namespace Game.Core
{
    public interface IHealth
    {
        /// <summary>
        /// Initialize all components and references
        /// </summary>
        void Initialize();

        /// <summary>
        /// Parameters: damage as float
        /// Substract the damage to the current health.
        /// </summary>
        void TakeDamage(float damage);

        /// <summary>
        /// Method activated when object has 0 health
        /// Three layers to check:
        ///     - Nexus
        ///     - Turret
        ///     - Enemy
        /// </summary>
        void Die();
    }
}
