using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRpg
{
  public class Player : Character
  {
    public int Floor { get; set; }
    public Room Room { get; set; }

    [SerializeField] Encounter encounter;

    public World world;

    // Start is called before the first frame update
    void Start()
    {

      Floor = 0;
      Energy = 30;
      Attack = 10;
      Defence = 5;
      Gold = 0;
      Inventory = new List<string>();
      RoomIndex = new Vector2(2, 2);

      // initialize room location
      this.Room = world.Dungeon[(int)RoomIndex.x, (int)RoomIndex.y];

      // ensure spawn inside empty room
      this.Room.Empty = true;

      UIController.OnPlayerStatChange();
      UIController.OnPlayerInventoryChange();

      encounter.ResetDynamicControls();

    }

    public void Move(int direction)
    {
      // check if room has enemy
      if (this.Room.Enemy)
      {
        return;
      }
      // north
      if (direction == 0 && RoomIndex.y > 0)
      {
        RoomIndex -= Vector2.up;
        Journal.Instance.Log("You move north.");
      }

      // east
      if (direction == 1 && RoomIndex.x < world.Dungeon.GetLength(0) - 1)
      {
        RoomIndex += Vector2.right;
        Journal.Instance.Log("You move east.");
      }

      // south
      if (direction == 2 && RoomIndex.y < world.Dungeon.GetLength(1) - 1)
      {
        RoomIndex -= Vector2.down;
        Journal.Instance.Log("You move south.");

      }

      // west
      if (direction == 3 && RoomIndex.x > 0)
      {
        RoomIndex += Vector2.left;
        Journal.Instance.Log("You move west.");
      }

      // only investigate when you enter a new room
      if (this.Room.RoomIndex != RoomIndex)
      {
        // enter next room
        Investigate();
      }
    }

    public void Investigate()
    {
      // reset location when entering a new room
      this.Room = world.Dungeon[(int)RoomIndex.x, (int)RoomIndex.y];

      encounter.ResetDynamicControls();

      // log info to journal
      if (this.Room.Empty)
      {
        Journal.Instance.Log("You find yourself in an empty room.");
      }
      else if (this.Room.Chest != null)
      {
        encounter.StartChest();
        Journal.Instance.Log("You find a chest! What would you like to do?");
      }
      else if (this.Room.Enemy != null)
      {
        encounter.StartCombat();
        Journal.Instance.Log("You are attacked by a " + Room.Enemy.Description + "! What would you like to do?");
      }
      else if (this.Room.Exit)
      {
        encounter.StartExit();
        Journal.Instance.Log("You've found the exit to the next floor. Would you like to continue?");
      }
    }

    public void AddItem(string item)
    {   
      Inventory.Add(item);
      UIController.OnPlayerInventoryChange();
      Journal.Instance.Log("You recieved item: " + item);
    }

    public override void TakeDamage(int amount)
    {
      base.TakeDamage(amount);
      UIController.OnPlayerStatChange();
      Debug.Log("Player TakeDamage.");
    }

    public override void Die()
    {
      base.Die();
      Debug.Log("Player died. Game over!");
    }
  }
}
