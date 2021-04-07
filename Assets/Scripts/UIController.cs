using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TextRpg
{
  public class UIController : MonoBehaviour
  {

    [SerializeField] Player player;
    [SerializeField] Text playerStatsText, enemyStatsText, playerInventoryText;

    public delegate void OnPlayerUpdateHandler();
    public static OnPlayerUpdateHandler OnPlayerStatChange;
    public static OnPlayerUpdateHandler OnPlayerInventoryChange;

    public delegate void OnEnemyUpdateHandler(Enemy enemy);
    public static OnEnemyUpdateHandler OnEnemyUpdate;

    void Start()
    {
      OnPlayerStatChange += UpdatePlayerStats;
      OnPlayerInventoryChange += UpdatePlayerInventory;
      OnEnemyUpdate += UpdateEnemyStats;
    }

    public void UpdatePlayerStats()
    {
      playerStatsText.text = string.Format("Player: {0} Energy, {1} Attack, {2} Defence, {3} Gold"
                                          , player.Energy, player.Attack,player.Defence,player.Gold);
    }

    public void UpdatePlayerInventory()
    {
      playerInventoryText.text = "Items: ";

      foreach (string item in player.Inventory)
      {
        if (playerInventoryText.text == "Items: ")
        {
          playerInventoryText.text += item;
        }
        else
        {
          playerInventoryText.text += ", " + item;
        }
      }
    }

    public void UpdateEnemyStats(Enemy enemy)
    {
      if (enemy)
      {
        enemyStatsText.text = string.Format("{0}: {1} Energy, {2} Attack, {3} Defence"
                                          , enemy.Description, enemy.Energy, enemy.Attack, enemy.Defence);
      }
      else
      {
        enemyStatsText.text = "";
      }
    }

  }
}