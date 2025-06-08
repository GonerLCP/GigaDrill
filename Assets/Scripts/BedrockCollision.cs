using UnityEngine;

public class BedrockCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.Instance.ActivePlayer.Explosion();
        }
    }
}
