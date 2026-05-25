using UnityEngine;

public class MovimentoChao : MonoBehaviour
{
    void Update()
    {
        // 1. Faz o chão andar para a esquerda
        // Usamos Vector3.left para ele vir na direção do jogador
        transform.Translate(Vector3.left * GameManager.velocidade * Time.deltaTime);

        // 2. Limpeza de memória (Otimização)
        // Se o chão já passou pelo jogador e sumiu da tela (ex: posição X menor que -30)
        // a gente destrói ele para o jogo não ficar pesado.
        if (transform.position.x < -35f)
        {
            Destroy(gameObject);
        }
    }
}