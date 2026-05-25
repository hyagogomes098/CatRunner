using UnityEngine;

public class SpawnerPowerUp : MonoBehaviour
{
    public GameObject powerUp;
    public float tempoSpawn = 10f;
    public float posX = 12f;
    public float alturaMin = -2f;
    public float alturaMax = 2f;

    float contador;

    void Start()
    {
        contador = tempoSpawn;
    }

    void Update()
    {
        contador -= Time.deltaTime;

        if (contador <= 0f)
        {
            Spawnar();
            contador = tempoSpawn;
        }
    }

    void Spawnar()
    {
        float posY = Random.Range(alturaMin, alturaMax);
        Vector3 pos = new Vector3(posX, posY, 0);

        Instantiate(powerUp, pos, Quaternion.identity);
    }
}