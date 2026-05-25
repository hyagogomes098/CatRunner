using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicaMenu : MonoBehaviour
{
    public GameObject painelConfig;
    public AudioSource musicaMenu; 
    public Slider sliderVolume;

    void Start()
    {
        if (painelConfig != null) painelConfig.SetActive(false);

        // Tenta pegar o volume salvo. Se não existir, começa em 0.8 (80%)
        float volumeSalvo = PlayerPrefs.GetFloat("VolumeGeral", 0.8f);
        
        if (sliderVolume != null)
        {
            sliderVolume.value = volumeSalvo * 100f;
        }

        if (musicaMenu != null)
        {
            musicaMenu.volume = volumeSalvo;
        }
    }

    public void IniciarJogo() 
    { 
        SceneManager.LoadScene(1); 
    }

    public void SairDoJogo() 
    { 
        Debug.Log("O botão Exit funcionou! O jogo fecharia agora no celular.");
        Application.Quit(); 
    }

    public void AbrirConfig() { painelConfig.SetActive(true); }
    public void FecharConfig() { painelConfig.SetActive(false); }

    public void AjustarVolume()
    {
        if (sliderVolume != null)
        {
            float volumeFinal = sliderVolume.value / 100f;
            
            // Ajusta a música do menu agora
            if (musicaMenu != null) musicaMenu.volume = volumeFinal;

            // SALVA na memória do jogo para as outras cenas usarem
            PlayerPrefs.SetFloat("VolumeGeral", volumeFinal);
            PlayerPrefs.Save();
        }
    }
}