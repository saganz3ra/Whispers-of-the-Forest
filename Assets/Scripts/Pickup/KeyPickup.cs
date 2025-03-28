using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public ItemData itemData; // Referência ao ScriptableObject que contém dados do item
    public GameObject keyPrefab; // Prefab da chave que aparecerá na mão do jogador
    public Transform keyHolder; // Local onde a chave será colocada no personagem

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory inventory = other.GetComponent<Inventory>();

            if (inventory != null)
            {
                // Verifica se o jogador já possui a chave antes de coletá-la
                if (!inventory.HasItem(itemData))
                {
                    inventory.AddItem(itemData);
                    Debug.Log($"Item '{itemData.itemName}' coletado!");

                    // Instancia a chave na mão do jogador
                    if (keyPrefab != null && keyHolder != null)
                    {
                        Instantiate(keyPrefab, keyHolder.position, keyHolder.rotation, keyHolder);
                    }
                    else
                    {
                        Debug.LogError("KeyPrefab ou KeyHolder não foi configurado corretamente!");
                    }

                    // Destrói a chave do chão
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log($"O jogador já possui '{itemData.itemName}' no inventário.");
                }
            }
            else
            {
                Debug.LogError("O jogador não tem um componente Inventory.");
            }
        }
    }
}
