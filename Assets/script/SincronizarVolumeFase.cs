using UnityEngine;

public class SincronizarVolumeGeral : MonoBehaviour
{
    void Start()
    {
        // Pega TODOS os AudioSources que estiverem nesse objeto
        AudioSource[] todosOsSons = GetComponents<AudioSource>();
        
        // Pega o volume que salvamos lá no Menu
        float volumeSalvo = PlayerPrefs.GetFloat("VolumeGeral", 0.8f);
        
        foreach (AudioSource som in todosOsSons)
        {
            som.volume = volumeSalvo;
        }
        
        Debug.Log(gameObject.name + " teve o volume sincronizado para: " + volumeSalvo);
    }
}