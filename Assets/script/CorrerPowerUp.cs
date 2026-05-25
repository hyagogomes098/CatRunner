using UnityEngine;
using System.Collections;

public class CorrerPowerUp : MonoBehaviour
{
    [Header("Configurações do Turbo")]
    public float multiplicador = 1.35f;
    public float duracao = 3f;

    private bool jaAtivado = false;

    void Update()
    {
        // Se ainda não pegou, ele vem vindo
        if (!jaAtivado)
        {
            transform.Translate(Vector3.left * GameManager.velocidade * Time.deltaTime);
        }

        if (transform.position.x < -35f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !jaAtivado)
        {
            jaAtivado = true;
            Debug.Log("COLETEI O ITEM!");

            // --- TRUQUE PARA O ITEM "SUMIR" NA HORA ---
            // Movemos o item para muito longe da tela instantaneamente
            transform.position = new Vector3(1000, 1000, 0);
            
            // Desativa o colisor para garantir que não pegue de novo
            var col = GetComponent<Collider2D>();
            if (col) col.enabled = false;

            StartCoroutine(AplicarTurbo());
        }
    }

    IEnumerator AplicarTurbo()
    {
        float velocidadeOriginal = GameManager.velocidade;

        // Aumenta a velocidade
        GameManager.velocidade = velocidadeOriginal * multiplicador;

        // Espera os 3 segundos da carreira
        yield return new WaitForSeconds(duracao);

        // Volta ao normal
        GameManager.velocidade = velocidadeOriginal;

        // Agora que tudo acabou, deleta o objeto de vez
        Destroy(gameObject);
    }
}