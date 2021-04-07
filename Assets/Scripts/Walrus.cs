using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRpg
{
  public class Walrus : Enemy
  {
    // Start is called before the first frame update
    void Start()
    {
      Energy = 15;
      MaxEnergy = 15;
      Attack = 6;
      Defence = 5;
      Gold = 30;
      Description = "Walrus";
      Inventory.Add("Fatty Flab");
    }
  }
}
