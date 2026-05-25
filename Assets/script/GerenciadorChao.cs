using UnityEngine;

public class GerenciadorChao : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject chaoPrefab;     

    [Header("Configurações de Tempo")]
    public float tempoParaNovoChao = 1.2f; 
    private float cronometro;

    void Update()
    {
        cronometro += Time.deltaTime;

        // Mantendo a lógica de velocidade que você já validou
        float vel = (GameManager.velocidade > 0) ? GameManager.velocidade : 5f;
        float tempoAjustado = tempoParaNovoChao / (vel / 5f);

        if (cronometro >= tempoAjustado)
        {
            SpawnarChao();
            cronometro = 0; 
        }
    }

    void SpawnarChao()
    {
        // Mantendo a posição de spawn que funciona no seu projeto
        Vector3 posicaoSpawn = new Vector3(30f, transform.position.y, 0f);
        GameObject novoChao = Instantiate(chaoPrefab, posicaoSpawn, Quaternion.identity);

        SpriteRenderer renderer = novoChao.GetComponent<SpriteRenderer>();
        if (renderer != null) 
        {
            renderer.enabled = true; // Mantendo sua correção para não ficar invisível
        }
    }
}