using UnityEngine;
using UnityEngine.UI;

public class FadeTela : MonoBehaviour
{
    public Image imagemPreta;
    public float velocidadeFade = 2f;

    private float alpha = 0f;
    private bool fadeAtivo = false;

    // 🔥 NOVO (garante que começa invisível)
    void Start()
    {
        Color cor = imagemPreta.color;
        cor.a = 0f;
        imagemPreta.color = cor;
    }

    void Update()
    {
        if (fadeAtivo)
        {
            alpha += Time.unscaledDeltaTime * velocidadeFade;
            alpha = Mathf.Clamp01(alpha);

            Color cor = imagemPreta.color;
            cor.a = alpha;
            imagemPreta.color = cor;

            // 🔥 NOVO (pausa só depois que escurecer tudo)
            if (alpha >= 1f)
            {
                Time.timeScale = 0f;
            }
        }
    }

    public void IniciarFade()
    {
        fadeAtivo = true;
    }
}