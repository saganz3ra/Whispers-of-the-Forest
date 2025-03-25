using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [Header("Respiração")]
    public float bobSpeed = 1.5f;   // Velocidade da respiração
    public float bobAmount = 0.1f;  // Intensidade do movimento ao respirar

    [Header("Movimento Natural")]
    public float noiseSpeed = 0.5f;  // Velocidade do ruído para oscilação da cabeça
    public float noiseAmount = 0.05f; // Intensidade do movimento aleatório

    private Vector3 originalPosition;
    private float time;

    void Start()
    {
        originalPosition = transform.localPosition; // Salva a posição inicial
    }

    void Update()
    {
        time += Time.deltaTime * bobSpeed;

        // Movimento de respiração (seno para efeito de sobe e desce)
        float verticalBob = Mathf.Sin(time) * bobAmount;

        // Pequenos movimentos de cabeça com Perlin Noise
        float noiseX = (Mathf.PerlinNoise(time * noiseSpeed, 0) - 0.5f) * noiseAmount;
        float noiseY = (Mathf.PerlinNoise(0, time * noiseSpeed) - 0.5f) * noiseAmount;

        // Aplicando os movimentos na posição original da câmera
        transform.localPosition = originalPosition + new Vector3(noiseX, verticalBob + noiseY, 0);
    }
}
