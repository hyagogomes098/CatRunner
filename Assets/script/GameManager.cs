using UnityEngine;

public class GameManager : MonoBehaviour
{
    // O que os outros scripts (chão, moedas) usam
    public static float velocidade; 

    [Header("Ajustes de Velocidade")]
    public float velocidadeInicial = 5f; // Começa com 5 (ou quanto você quiser)
    public float velocidadeLimite = 15f; // Não passa de 10
    
    private float cronometro = 0f;

    void Awake()
    {
        // Garante que o jogo comece na velocidade inicial
        velocidade = velocidadeInicial;
    }

    void Update()
    {
        // Se a velocidade ainda não bateu o teto de 10...
        if (velocidade < velocidadeLimite)
        {
            // O cronômetro conta o tempo real em segundos
            cronometro += Time.deltaTime;

            // Se passou de 10 segundos
            if (cronometro >= 7f)
            {
                velocidade += 1f; // Aumenta 1 na velocidade
                cronometro = 0f;  // Zera o cronômetro pra contar os próximos 10s
                
                // Debug pra você ver no console o aumento acontecendo
                Debug.Log("Velocidade subiu para: " + velocidade);
            }
        }
    }
}