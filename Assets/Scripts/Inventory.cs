using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Dicionário para armazenar os itens e suas quantidades
    private Dictionary<ItemData, int> itemDictionary = new Dictionary<ItemData, int>();

    // Método para adicionar um item ao inventário
    public void AddItem(ItemData newItem)
    {
        if (itemDictionary.ContainsKey(newItem))
        {
            // Se o item já existe, incrementa a quantidade
            itemDictionary[newItem]++;
        }
        else
        {
            // Se o item não existe, adiciona ao dicionário com a quantidade inicial de 1
            itemDictionary.Add(newItem, 1);
        }

        // Exibe no console para debug
        Debug.Log($"Item '{newItem.itemName}' adicionado ao inventário. Quantidade: {itemDictionary[newItem]}");
    }

    // Método para exibir o inventário no Console
    public void ShowInventory()
    {
        Debug.Log("Inventário:");
        foreach (KeyValuePair<ItemData, int> item in itemDictionary)
        {
            Debug.Log($"{item.Key.itemName} - Quantidade: {item.Value}");
        }
    }

    // Método para verificar a quantidade de um item específico
    public int GetItemCount(ItemData item)
    {
        if (itemDictionary.ContainsKey(item))
        {
            return itemDictionary[item];
        }
        return 0; // Retorna 0 se o item não estiver no inventário
    }

    // Exemplo para mostrar o inventário quando pressionar a tecla 'I'
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) // Pressione 'I' para exibir o inventário no console
        {
            ShowInventory();
        }
    }
}