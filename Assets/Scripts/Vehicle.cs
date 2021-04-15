using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

public abstract class Vehicle : MovingObject
{
     public readonly int wage;
     public readonly int capacity;
     
     public Vehicle(int wage, int capacity){
          this.wage = wage;
          this.capacity = capacity;
     }
}
