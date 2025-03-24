using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData; // Referência ao ScriptableObject que contém dados do item

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto colidido tem a tag "Player"
        if (other.CompareTag("Player"))
        {
            // Obtém o componente Inventory3D do jogador
            Inventory inventory = other.GetComponent<Inventory>();

            if (inventory != null)
            {
                // Adiciona o item ao inventário
                inventory.AddItem(itemData);
                Debug.Log($"Item '{itemData.itemName}' coletado!");

                // Destrói o item após ser coletado
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("O jogador não tem o componente Inventory3D.");
            }
        }
    }
}