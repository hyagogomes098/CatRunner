using UnityEngine;

public class SpawnerMoeda : MonoBehaviour
{
    public GameObject[] gruposDeMoedas; // seus 4 grupos

    public float tempoSpawn = 3f; // tempo fixo
    private float contador;

    public float posX = 12f; // na frente da tela
    public float alturaMin = -2f;
    public float alturaMax = 2f;

    void Start()
    {
        contador = tempoSpawn;
    }

    void Update()
    {
        contador -= Time.deltaTime;

        if (contador <= 0f)
        {
            SpawnarGrupo();
            contador = tempoSpawn; // reinicia o tempo
        }
    }

    void SpawnarGrupo()
    {
        if (gruposDeMoedas == null || gruposDeMoedas.Length == 0)
            return;

        int index = Random.Range(0, gruposDeMoedas.Length);

        if (gruposDeMoedas[index] == null)
            return;

        float posY = Random.Range(alturaMin, alturaMax);

        Vector3 posicao = new Vector3(posX, posY, 0);

        Instantiate(gruposDeMoedas[index], posicao, Quaternion.identity);
    }
}