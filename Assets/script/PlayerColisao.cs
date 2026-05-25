using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerColisao : MonoBehaviour
{
    [Header("Vidas")]
    public int vidasMax = 3;
    private int vidas;

    [Header("UI Vidas")]
    public GameObject[] coracoes; 

    [Header("Invencibilidade")]
    public float tempoInvencivel = 1f;
    private bool podeTomarDano = true;

    [Header("Visual")]
    public SpriteRenderer sprite;
    public float tempoPiscar = 0.1f;

    [Header("Som")]
    public AudioSource audioSource;
    public AudioClip somDano;
    public AudioClip somGameOver;

    [Header("Volume")]
    [Range(0f, 1f)] public float volumeDano = 1f;
    [Range(0f, 1f)] public float volumeGameOver = 1f;

    [Header("UI")]
    public GameObject textoGameOver;

    private Rigidbody2D rb;

    void Start()
    {
        vidas = vidasMax;

        if (sprite == null)
            sprite = GetComponentInChildren<SpriteRenderer>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody2D>();

        audioSource.ignoreListenerPause = true;

        if (textoGameOver != null)
            textoGameOver.SetActive(false);

        Time.timeScale = 1f;
        AudioListener.pause = false;

        AtualizarCoracoes(); 
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstaculo") && podeTomarDano)
        {
            TomarDano();
        }
    }

    void TomarDano()
    {
        vidas--;

        AtualizarCoracoes(); 

        if (vidas <= 0)
        {
            Morrer();
        }
        else
        {
            audioSource.PlayOneShot(somDano, volumeDano);
            StartCoroutine(Invencibilidade());
        }
    }

    void AtualizarCoracoes() 
    {
        for (int i = 0; i < coracoes.Length; i++)
        {
            coracoes[i].SetActive(i < vidas);
        }
    }

    void Morrer()
    {
        podeTomarDano = false;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }

        if (textoGameOver != null)
            textoGameOver.SetActive(true);

        Time.timeScale = 0f;
        AudioListener.pause = true;

        audioSource.PlayOneShot(somGameOver, volumeGameOver);

        StartCoroutine(ReiniciarDepois());
    }

    System.Collections.IEnumerator ReiniciarDepois()
    {
        // Espera o tempo do som de Game Over acabar
        yield return new WaitForSecondsRealtime(somGameOver.length);

        AudioListener.pause = false;
        Time.timeScale = 1f;

        // A ÚNICA MUDANÇA: Agora ele carrega o Menu (cena 0) em vez de reiniciar a fase
        SceneManager.LoadScene(0);
    }

    System.Collections.IEnumerator Invencibilidade()
    {
        podeTomarDano = false;

        float tempo = 0f;

        while (tempo < tempoInvencivel)
        {
            sprite.enabled = !sprite.enabled;
            yield return new WaitForSeconds(tempoPiscar);
            tempo += tempoPiscar;
        }

        sprite.enabled = true;
        podeTomarDano = true;
    }
}