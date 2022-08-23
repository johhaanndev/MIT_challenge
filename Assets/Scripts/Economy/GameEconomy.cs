using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Economy
{
    public class GameEconomy : MonoBehaviour
    {
        [SerializeField] int money = 500;
        [SerializeField] Text moneyText; 

        void Start()
        {
            moneyText.text = $"Money: {money}";
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
            if (money >= 100)
                moneyText.text = $"Money: {money}";
            else if (money >= 10)
                moneyText.text = $"Money: 0{money}";
            else 
                moneyText.text = $"Money: 00{money}";
        }
    }
}