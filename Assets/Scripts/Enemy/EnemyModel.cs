public class EnemyModel
{
    private EnemyDescription _description;
    private float _currentHealth;

    public EnemyDescription Description => _description;

    public EnemyModel(EnemyDescription description){
        _description = description;
        _currentHealth = _description.MaxHealth;
    }

    // Логика монстра
}

