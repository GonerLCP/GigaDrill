using UnityEngine;

public class RockScript : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && GameManager.Instance.ActivePlayer.dead ==false)
        {
            GameManager.Instance.ActivePlayer.Explosion();
            Destroy(this.gameObject);
        }
        if (GameManager.Instance.ActivePlayer.exploding == true)
        {
            Destroy(this.gameObject);
        }
    }
}
