using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Player ActivePlayer;
    public GameObject QTE;
    bool Activated;

    void Start()
    {
        if (Instance == null) {  Instance = this; }else { Destroy(this); }
        Activated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ActivePlayer.drilling == true && Activated == false)
        {
            print("Oui");
            Activated = true;
            QTE.SetActive(true);
        }
        else if(ActivePlayer.drilling == false && Activated == true)
        {
            Activated = false;
            QTE.SetActive(false);
        }
    }
}
