using UnityEngine;

public class SpawnerObstaculo : MonoBehaviour
{
    public GameObject[] obstaculos;

    public float tempoSpawn = 5f;

    public float posX = 10f; // só controla o X agora

    private float tempo;

    void Update()
    {
        tempo += Time.deltaTime;

        if (tempo >= tempoSpawn)
        {
            Spawnar();
            tempo = 0f;
        }
    }

    void Spawnar()
    {
        int index = Random.Range(0, obstaculos.Length);

        GameObject prefab = obstaculos[index];

        // pega altura original do prefab
        float altura = prefab.transform.position.y;

        Vector3 pos = new Vector3(posX, altura, 0f);

        Instantiate(prefab, pos, Quaternion.identity);
    }
}