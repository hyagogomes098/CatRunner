using UnityEngine;

public class DestruirForaDaTela : MonoBehaviour
{
    void Update()
    {
        if (transform.position.x < -30f)
        {
            Destroy(gameObject);
        }
    }
}