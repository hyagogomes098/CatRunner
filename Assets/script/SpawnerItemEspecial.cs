using UnityEngine;

public class SpawnerItemEspecial : MonoBehaviour
{
    [Header("Configurações do Item")]
    public GameObject prefabItem; // Arraste seu Prefab de PowerUp pra cá

    [Header("Configurações de Tempo")]
    public float tempoSpawn = 10f; // Itens especiais demoram mais que moedas
    private float contador;

    [Header("Posicionamento")]
    public float posX = 15f; // Um pouco mais à frente da tela
    public float alturaMin = -3f;
    public float alturaMax = 3f;

    void Start()
    {
        // Começa o contador para o primeiro item
        contador = tempoSpawn;
    }

    void Update()
    {
        contador -= Time.deltaTime;

        if (contador <= 0f)
        {
            SpawnarItem();
            contador = tempoSpawn; // Reinicia o ciclo
        }
    }

    void SpawnarItem()
    {
        if (prefabItem == null) return;

        // Calcula a posição aleatória no eixo Y
        float posY = Random.Range(alturaMin, alturaMax);
        Vector3 posicao = new Vector3(posX, posY, 0);

        // O SEGREDO DO TAMANHO: 
        // Usamos o Instantiate guardando a referência do objeto criado
        GameObject novoItem = Instantiate(prefabItem, posicao, Quaternion.identity);

        // Garante que o tamanho (escala) seja idêntico ao do Prefab original
        novoItem.transform.localScale = prefabItem.transform.localScale;
    }
}