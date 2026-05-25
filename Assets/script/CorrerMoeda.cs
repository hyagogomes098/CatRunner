using UnityEngine;

public class CorrerMoeda : MonoBehaviour
{
    public float velocidadeFixa = 5f; // Ajuste aqui a velocidade da moeda

    void Update()
    {
        // ACRESCIMO: Agora ela multiplica pela velocidade global do GameManager
        // Dividimos por 7 (sua velocidade base) para manter a proporção que você definiu
        float velocidadeReal = velocidadeFixa * (GameManager.velocidade / 7f);

        // Move para a esquerda de forma independente do cenário
        // Mantive o Vector3.right que você estava usando
        transform.Translate(Vector3.right * velocidadeReal * Time.deltaTime);

        // Se a moeda sair muito da tela (lá no fundo), ela se apaga
        if (transform.position.x < -35f) 
        {
            Destroy(gameObject);
        }
    }
}