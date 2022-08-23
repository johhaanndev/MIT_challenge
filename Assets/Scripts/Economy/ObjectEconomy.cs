using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Economy
{
    public class ObjectEconomy : MonoBehaviour
    {
        [SerializeField] int price = 10;

        public int GetPrice() => price;
    }
}