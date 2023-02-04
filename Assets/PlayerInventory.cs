using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, InterfaceInventory
{
   public int Item { get => item; set => item = value; }

   public int item = 0;
}
