using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TextRpg
{
  public class Encounter : MonoBehaviour
  {
    public Enemy Enemy { get; set; }

    [SerializeField] Player player;

    [SerializeField] Button[] dynamicControls;

    public delegate void OnEnemyDieHandler();
    public static OnEnemyDieHandler OnEnemyDie;

    private void Start()
    {
      // reference delegate to determine if enemy died, then loot
      OnEnemyDie += Loot;
    }

    public void ResetDynamicControls()
    {
      // disable all dynamic controls
      foreach (Button button in dynamicControls)
      {
        button.interactable = false;
      }
    }

    public void StartCombat()
    {
      // load enemy and enable buttons to start combat
      this.Enemy = player.Room.Enemy;
      dynamicControls[0].interactable = true;
      dynamicControls[1].interactable = true;
      UIController.OnEnemyUpdate(this.Enemy);
    }

    public void StartChest()
    {
      // enable chest controls
      dynamicControls[3].interactable = true;
    }

    public void StartExit()
    {
      // enable exit room controls
      dynamicControls[2].interactable = true;
    }

    public void OpenChest()
    {

      Chest chest = player.Room.Chest;

      if (chest.Trap)
      {
        // it was a trap, player takes damage
        player.TakeDamage(5);
        UIController.OnPlayerStatChange();
        Journal.Instance.Log("It was a trap! You took 5 damage.");
      }
      else if (chest.Heal)
      {
        // healing chest
        player.TakeDamage(-7);
        UIController.OnPlayerStatChange();
        Journal.Instance.Log("The chest containt a healing potion! You gain 7 health.");
      }
      else if (chest.Enemy)
      {
        // contained enemy
        // set enemy, delete chest, then investigate to refresh room and detect the new monster
        player.Room.Enemy = chest.Enemy;
        player.Room.Chest = null;
        Journal.Instance.Log("The chest containt a monster!");
        player.Investigate();
      }
      else
      {
        // get treasure
        player.Gold += chest.Gold;
        player.AddItem(chest.Item);
        UIController.OnPlayerStatChange();
        UIController.OnPlayerInventoryChange();
        Journal.Instance.Log("The chest containt treasure! You found " + chest.Item + " and <color=#FFE556FF>" + chest.Gold + " gold.</color>");
      }
      // delete chest from room
      player.Room.Chest = null;
      // reset controls
      dynamicControls[3].interactable = false;
    }

    public void Attack()
    {

      // damage calculations
      int playerDamageAmount = (int)(Random.value * (player.Attack - Enemy.Defence));
      int enemyDamageAmount = (int)(Random.value * (Enemy.Attack - player.Defence));

      // ensure attacks cannot heal
      if (playerDamageAmount < 0) playerDamageAmount = 0;
      if (enemyDamageAmount < 0) enemyDamageAmount = 0;

      // log combat
      Journal.Instance.Log("<color=#59ffa1> You attacked, dealing <b>" + playerDamageAmount + "</b> damage!</color>");
      Journal.Instance.Log("<color=#59ffa1> The enemy attacked, dealing <b>" + enemyDamageAmount + "</b> damage!</color>");

      // characters take damage
      player.TakeDamage(enemyDamageAmount);
      Enemy.TakeDamage(playerDamageAmount);

    }

    public void Flee()
    {

      // damage calculation. defence reduced by half when fleeing
      int enemyDamageAmount = (int)(Random.value * (Enemy.Attack - (player.Defence*.5f)));

      // player takes damage
      player.TakeDamage(enemyDamageAmount);

      // clear room
      player.Room.Enemy = null;
      player.Room.Empty = true;

      // log combat
      Journal.Instance.Log("<color=#59ffa1> The fled the enemy, but took <b>" + enemyDamageAmount + "</b> damage!</color>");

      // reset room
      player.Investigate();

    }

    public void ExitFloor()
    {

      // reset floor map
      StartCoroutine(player.world.GenerateFloor());

      // increase player floor level
      player.Floor += 1;

      // log
      Journal.Instance.Log("You found the exit to the next floor! Advancing to floor " + player.Floor + ".");

      // reset controls
      dynamicControls[2].interactable = false;

    }

    public void Loot()
    {

      // log results via string format variables
      Journal.Instance.Log(string.Format("<color=#56ffc7ff> You've slain {0}! Searching the enemy, you find a {1} and {2} gold!</color>"
                           , this.Enemy.Description, this.Enemy.Inventory[0], this.Enemy.Gold));

      // loot the enemy
      player.AddItem(this.Enemy.Inventory[0]);
      player.Gold += this.Enemy.Gold;

      // update player
      UIController.OnPlayerStatChange();
      UIController.OnPlayerInventoryChange();

      // clear room
      this.Enemy = null;
      player.Room.Enemy = null;
      player.Room.Empty = true;

      // re-investigate
      player.Investigate();

      // update enemy
      UIController.OnEnemyUpdate(this.Enemy);

    }
  }
}