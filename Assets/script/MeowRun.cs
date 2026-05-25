using UnityEngine;
using TMPro;

public class MeowRun : MonoBehaviour
{
    [Header("Configurações do Placar")]
    public TextMeshProUGUI textoScore;   // Aqui você já arrastou o da distância
    public TextMeshProUGUI textoMoedas;  // <--- NOVO: Arraste o texto das PAWCOINS pra cá!
    
    public float velocidadeDoJogo = 7f; 

    private float distanciaAcumulada = 0f;
    
    // O cofre estático para as moedas
    public static int quantidadePawcoins = 0; 

    void Start()
    {
        quantidadePawcoins = 0; // Zera as moedas quando o jogo começa
    }

    void Update()
    {
        if (Time.timeScale > 0)
        {
            // --- PARTE DA DISTÂNCIA (O que já funcionava) ---
            if (textoScore != null)
            {
                distanciaAcumulada += Time.deltaTime * velocidadeDoJogo;
                int metros = Mathf.FloorToInt(distanciaAcumulada);
                textoScore.text = "MEOW RUN: " + metros.ToString() + "m";
            }

            // --- PARTE DAS MOEDAS (A novidade) ---
            if (textoMoedas != null)
            {
                textoMoedas.text = "PAWCOINS: " + quantidadePawcoins.ToString();
            }
        }
    }
}