using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int health;
    public int damage;
    
    public float speed;
    
    public EnemyAttackType attackType;
    
    public void SetupEnemyStats(int enemyHealth, int enemyDamage, float enemySpeed, EnemyAttackType enemyAttackType)
    {
        health = enemyHealth;
        damage = enemyDamage;
        speed = enemySpeed;
        attackType = enemyAttackType;
    }
}