using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100; // Vida inicial do jogador

    public void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log("Vida do jogador: " + health);

        // Toca o som de dano
        AudioManagerInGame.Instance.PlayDamage();

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Jogador morreu!");
        gameObject.SetActive(false); // Desativa o jogador ao morrer
    }
}
