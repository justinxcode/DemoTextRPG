using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRpg
{
  public class Racoon : Enemy
  {
    // Start is called before the first frame update
    void Start()
    {
      Energy = 10;
      Attack = 5;
      Defence = 3;
      Gold = 20;
      Inventory.Add("Bandit Mask");
    }

  }
}
