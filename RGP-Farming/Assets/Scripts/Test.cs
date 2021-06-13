using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public HealthManager healthManager;

    public void TakeDamage(int dmg)
    {
        healthManager.TakeDamage(dmg);
    }

    public void Heal(int dmg)
    {
        healthManager.Heal(dmg);
    }

    public void TestAdding()
    {
        ItemReceiverManager.Instance().Add(new Item(ItemManager.Instance().ForName("Hoe"), 5));
    }
}
