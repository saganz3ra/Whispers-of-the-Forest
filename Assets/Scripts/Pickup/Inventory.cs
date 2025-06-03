using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Pickup
{
    public class Inventory : MonoBehaviour
    {
        private Dictionary<ItemData, int> itemDictionary = new Dictionary<ItemData, int>();

        public void AddItem(ItemData newItem)
        {
            if (newItem == null)
            {
                Debug.LogError("Tentativa de adicionar um item nulo ao inventário!");
                return;
            }

            if (itemDictionary.ContainsKey(newItem))
            {
                itemDictionary[newItem]++;
            }
            else
            {
                itemDictionary.Add(newItem, 1);
            }

            Debug.Log(
                $"Item '{newItem.itemName}' adicionado ao inventário. Quantidade: {itemDictionary[newItem]}"
            );
        }

        public bool HasItem(ItemData item)
        {
            if (item == null)
            {
                Debug.LogError("Tentativa de verificar um item nulo no inventário!");
                return false;
            }

            return itemDictionary.ContainsKey(item);
        }

        public int GetItemCount(ItemData item)
        {
            if (item == null)
            {
                Debug.LogError("Tentativa de verificar a quantidade de um item nulo no inventário!");
                return 0;
            }

            return itemDictionary.TryGetValue(item, out int count) ? count : 0;
        }

        public void ShowInventory()
        {
            Debug.Log("Inventário:");
            foreach (KeyValuePair<ItemData, int> item in itemDictionary)
            {
                Debug.Log($"{item.Key.itemName} - Quantidade: {item.Value}");
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                ShowInventory();
            }
        }
    }
}