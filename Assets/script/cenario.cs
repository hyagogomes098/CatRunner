using UnityEngine;

public class GerenciadorFasesFluido : MonoBehaviour
{
    [Header("Configurações de Objetos")]
    public SpriteRenderer fundoA;
    public SpriteRenderer fundoB;
    public Sprite[] listaDeFotos;

    [Header("Configurações de Movimento")]
    public float tempoPorFase = 10f;

    private int indiceAtual = 0;
    private float cronometro = 0f;
    private float larguraCenario;

    void Start()
    {
        if (fundoA == null || fundoB == null || listaDeFotos.Length == 0)
        {
            Debug.LogError("Ei! Falta arrastar o FundoA, FundoB ou as Fotos para o script!");
            return;
        }

        fundoA.sprite = listaDeFotos[0];
        fundoB.sprite = listaDeFotos[0];
        
        AjustarTamanho(fundoA);
        AjustarTamanho(fundoB);

        larguraCenario = fundoA.bounds.size.x;

        // Posicionamento inicial preciso
        fundoB.transform.position = new Vector3(fundoA.transform.position.x + larguraCenario - 0.02f, fundoA.transform.position.y, fundoA.transform.position.z);
    }

    void Update()
    {
        // Pega a velocidade lá do seu GameManager
        float deslocamento = GameManager.velocidade * Time.deltaTime;
        
        fundoA.transform.position += Vector3.left * deslocamento;
        fundoB.transform.position += Vector3.left * deslocamento;

        // Verificação de saída com margem de segurança (larguraCenario - 1)
        // Isso evita que o fundo demore a pular pra frente
        if (fundoA.transform.position.x <= -larguraCenario)
        {
            TrocarEReposicionar(fundoA, fundoB);
        }

        if (fundoB.transform.position.x <= -larguraCenario)
        {
            TrocarEReposicionar(fundoB, fundoA);
        }

        cronometro += Time.deltaTime;
        if (cronometro >= tempoPorFase)
        {
            indiceAtual = (indiceAtual + 1) % listaDeFotos.Length;
            cronometro = 0f;
        }
    }

    void TrocarEReposicionar(SpriteRenderer saindo, SpriteRenderer parado)
    {
        saindo.sprite = listaDeFotos[indiceAtual];
        AjustarTamanho(saindo);
        
        // Atualiza a largura caso o sprite tenha mudado
        larguraCenario = saindo.bounds.size.x;

        // O SEGREDO: Em vez de calcular baseado no zero, calculamos baseado na posição do que está parado.
        // Assim, mesmo que o jogo esteja a 1000km/h, os dois fundos ficam colados.
        float novoX = parado.transform.position.x + larguraCenario - 0.02f; 
        saindo.transform.position = new Vector3(novoX, parado.transform.position.y, parado.transform.position.z);
    }

    void AjustarTamanho(SpriteRenderer sr)
    {
        if (sr.sprite == null) return;

        float alturaCamera = Camera.main.orthographicSize * 2.0f;
        float larguraCamera = alturaCamera * Screen.width / Screen.height;

        float sW = sr.sprite.bounds.size.x;
        float sH = sr.sprite.bounds.size.y;

        if (sW > 0 && sH > 0)
        {
            sr.transform.localScale = new Vector3(larguraCamera / sW, alturaCamera / sH, 1);
        }
    }
}