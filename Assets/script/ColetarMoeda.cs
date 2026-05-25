using UnityEngine;

public class ColetarMoeda : MonoBehaviour
{
    public AudioClip somMoeda; 
    private AudioSource meuAudio;

    void Start() {
        // Pega o rádio que tá no próprio Player
        meuAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D outro)
    {
        // Se o Player bater em algo com a Tag "Moeda"
        if (outro.CompareTag("Moeda"))
        {
            if (somMoeda != null && meuAudio != null)
            {
                meuAudio.PlayOneShot(somMoeda);
            }

            // --- ESSA É A LINHA QUE EU ACRESCENTEI ---
            MeowRun.quantidadePawcoins++; 
            // ----------------------------------------

            Debug.Log("Dinheiro na conta! Coletei uma!");
            Destroy(outro.gameObject); // Destrói a moeda (o outro)
        }
    }
}