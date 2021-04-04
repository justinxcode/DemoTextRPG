using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRpg
{
  public class Enemy : Character
  {
    private void Start()
    {
      Energy = 20;
      Attack = 5;
    }

    public override void TakeDamage(int amount)
    {
      base.TakeDamage(amount);
      Debug.Log("This also happens, but only on enemy! not other characters");
    }
    public override void Die()
    {
      base.Die();
      Debug.Log("Character died, was enemy.");
    }
  }
}
