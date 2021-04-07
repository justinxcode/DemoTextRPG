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
      MaxEnergy = 10;
      Attack = 8;
      Defence = 3;
      Gold = 20;
      Description = "Raccoon";
      Inventory.Add("Bandit Mask");
    }
  }
}
