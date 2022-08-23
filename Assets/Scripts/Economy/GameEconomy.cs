using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Economy
{
    public class GameEconomy : MonoBehaviour
    {
        [SerializeField] int money = 500;

        void Start()
        {
            Debug.Log($"Money: {money}");
        }

        public void SpendMoney(int price)
        {
            money -= price;
            UpdateEconomyUI();
        }

        public void RefundMoney(int price)
        {
            money += price;
            UpdateEconomyUI();
        }

        public bool CanBuy(int price) => price <= money;

        private void UpdateEconomyUI()
        {
            Debug.Log($"Current money: {money}");
        }
    }
}