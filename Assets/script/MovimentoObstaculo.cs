using UnityEngine;

public class MoverObstaculo : MonoBehaviour
{
    public float velocidade = 6f;

    void Update()
    {
        transform.Translate(Vector2.left * velocidade * Time.deltaTime);

        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}