using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRpg
{
  public class Enemy : Character
  {
    public string Description { get; set; }

    public override void TakeDamage(int amount)
    {

      Energy -= amount;

      if (Energy <= 0)
      {
        Die();
      }
      else
      {
        UIController.OnEnemyUpdate(this);
      }

    }
    public override void Die()
    {
      Encounter.OnEnemyDie();
      Energy = MaxEnergy;
    }
  }
}
