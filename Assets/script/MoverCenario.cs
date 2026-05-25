using UnityEngine;

public class MoverCenario : MonoBehaviour
{
    void Update()
    {
        float vel = (GameManager.velocidade > 0) ? GameManager.velocidade : 5f;
        transform.Translate(Vector2.left * vel * Time.deltaTime);
    }
}