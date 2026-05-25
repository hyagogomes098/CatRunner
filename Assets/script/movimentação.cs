using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimentação (Endless Runner)")]
    public float velocidadeCorrida = 7f; 
    public float forcaPulo = 16f;

    [Header("Sons do Pulo (NOVO)")]
    public AudioClip somPulo1; 
    public AudioClip somPulo2; 
    private AudioSource meuAudio;
    private bool usarPrimeiroSom = true;

    [Header("Componentes Visuais (Arraste aqui)")]
    public Rigidbody2D rb;
    public Transform corpoVisual;
    public Transform pernaEsq;
    public Transform pernaDir;
    public Transform bracoEsq;
    public Transform bracoDir;

    [Header("Detecção de Chão")]
    public Transform feetPos;
    public LayerMask groundLayer;
    public float raioChao = 0.2f;

    [Header("Ajustes da Animação Automática")]
    public float velocidadeGeral = 12f;
    public float inclinacaoCorpo = 5f;
    public float balancoMembros = 25f;

    private bool estaNoChao;

    // 🔴 já existente
    public GameObject telaPreta;
    private bool morreu = false;

    // 🔥 NOVO (fade)
    public FadeTela fadeTela;

    void Start()
    {
        // Pega o componente de áudio que está no Player
        meuAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(velocidadeCorrida, rb.linearVelocity.y);

        estaNoChao = Physics2D.OverlapCircle(feetPos.position, raioChao, groundLayer);

        AnimarGatinho();

        if (estaNoChao && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo);

            // --- LÓGICA DO SOM DO PULO ---
            if (meuAudio != null)
            {
                if (usarPrimeiroSom)
                {
                    meuAudio.PlayOneShot(somPulo1);
                }
                else
                {
                    meuAudio.PlayOneShot(somPulo2);
                }
                // Inverte para o próximo pulo ser o outro som
                usarPrimeiroSom = !usarPrimeiroSom;
            }
        }
    }

    void AnimarGatinho()
    {
        if (estaNoChao)
        {
            float rotacaocorpo = Mathf.Sin(Time.time * velocidadeGeral) * inclinacaoCorpo;
            corpoVisual.localRotation = Quaternion.Euler(0, 0, rotacaocorpo);

            float movimentoMembros = Mathf.Sin(Time.time * velocidadeGeral) * balancoMembros;

            if (pernaEsq != null && pernaDir != null)
            {
                pernaEsq.localRotation = Quaternion.Euler(0, 0, movimentoMembros);
                pernaDir.localRotation = Quaternion.Euler(0, 0, -movimentoMembros);
            }

            if (bracoEsq != null && bracoDir != null)
            {
                bracoEsq.localRotation = Quaternion.Euler(0, 0, -movimentoMembros);
                bracoDir.localRotation = Quaternion.Euler(0, 0, movimentoMembros);
            }
        }
        else
        {
            Quaternion targetRotation = Quaternion.identity;
            corpoVisual.localRotation = Quaternion.Lerp(corpoVisual.localRotation, targetRotation, Time.deltaTime * 10f);

            if (pernaEsq != null) pernaEsq.localRotation = Quaternion.Lerp(pernaEsq.localRotation, targetRotation, Time.deltaTime * 10f);
            if (pernaDir != null) pernaDir.localRotation = Quaternion.Lerp(pernaDir.localRotation, targetRotation, Time.deltaTime * 10f);
            if (bracoEsq != null) bracoEsq.localRotation = Quaternion.Lerp(bracoEsq.localRotation, targetRotation, Time.deltaTime * 10f);
            if (bracoDir != null) bracoDir.localRotation = Quaternion.Lerp(bracoDir.localRotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    private void OnDrawGizmos()
    {
        if (feetPos != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(feetPos.position, raioChao);
        }
    }

    // 🔴 já existente (NÃO alterei nada)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstaculo") && !morreu)
        {
            morreu = true;
            Time.timeScale = 0f;
            telaPreta.SetActive(true);

            // 🔥 NOVO (não interfere no seu código)
            rb.linearVelocity = Vector2.zero;
            this.enabled = false;

            if (fadeTela != null)
            {
                fadeTela.IniciarFade();
            }
        }
    }
}