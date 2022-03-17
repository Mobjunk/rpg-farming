using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Npcs/New Enemy")]
public class EnemyData : NpcData
{
    [Header("Attack Distance"), Tooltip("The attack range for the enemy")] public float attackRange;
    [Header("Follow Range"), Tooltip("The follow range between the enemy and player")] public float followRange;
}
