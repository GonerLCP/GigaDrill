using UnityEngine;

public class DestroyTile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("je collide");
        if (collision.GetComponent<Player>().drilling == true)
        {
            Destroy(this);
        }
    }
}
