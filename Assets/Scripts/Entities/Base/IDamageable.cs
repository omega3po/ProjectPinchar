namespace Entities.Base
{
    public interface IDamageable
    {
        int CurrentHealth { get; }
        
        void TakeDamage(int amount);
        
        void Heal(int amount);
    }
}

