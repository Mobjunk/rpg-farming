using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Npcs/New Enemy")]
public class EnemyData : NpcData
{
    [Header("Attack Distance"), Tooltip("The attack range for the enemy")] public float attackRange;
    [Header("Follow Range"), Tooltip("The follow range between the enemy and player")] public float followRange;
    [Header("Attack Speed"), Tooltip("The speed of which the enemy attacks with")] public float attackSpeed;
    [Header("Attack damage"), Tooltip("The amount of damage the enemy does to the player")]public int attackDamage;
}
