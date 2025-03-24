using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public ItemData itemData; // Referência ao ScriptableObject que contém dados do item
    public GameObject keyPrefab; // Referência ao prefab da chave
    public Transform keyHolder; // Referência ao transform onde a chave deve aparecer no personagem

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

                // Instancia a chave no personagem
                Instantiate(keyPrefab, keyHolder.position, keyHolder.rotation, keyHolder);

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