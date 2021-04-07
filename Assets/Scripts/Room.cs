using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRpg
{
  public class Room
  {
    public Chest Chest { get; set; }
    public Enemy Enemy { get; set; }
    public bool Exit { get; set; }
    public bool Empty { get; set; }
    public Vector2 RoomIndex { get; set; }

    public Room(Chest chest, Enemy enemy, bool empty, bool exit)
    {
      this.Chest = chest;
      this.Enemy = enemy;
      this.Empty = empty;
      this.Exit = exit;
    }
    public Room()
    {
      int roll = Random.Range(0,30);

      // get random enemy
      if (roll > 0 && roll < 6)
      {
        // set enemy
        Enemy = EnemyDatabase.Instance.GetRandomEnemy();

        // place enemy somewhere in room
        Enemy.RoomIndex = RoomIndex;
      }
      else if (roll > 15 && roll < 20)
      {
        // create chest
        Chest = new Chest();
      }
      else
      {
        Empty = true;
      }
    }
  }
}
