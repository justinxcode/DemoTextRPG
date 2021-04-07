using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRpg
{
  public class ItemDatabase : MonoBehaviour
  {
    public List<string> Items { get; set; } = new List<string>();

    public static ItemDatabase Instance { get; private set; }

    private void Awake()
    {

      // singleton
      if(Instance != null && Instance != this)
      {
        Destroy(this.gameObject);
      }
      else
      {
        Instance = this;
      }

      Items.Add("Emerald Slipper");
      Items.Add("Empty Chalice");
      Items.Add("Bowtie");
    }
  }
}
