using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Player ActivePlayer;
    public GameObject QTE;
    public TilemapCollider2D GridCollider;
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
            GridCollider.isTrigger = true; //Permet de passer à travers la collision de la grid
            Activated = true;//l'équivalent d'un doOnce
            QTE.SetActive(true);//Commence le QTE
        }
        else if(ActivePlayer.drilling == false && Activated == true)
        {
            GridCollider.isTrigger = false;//Réactive la collision
            Activated = false;
            QTE.SetActive(false);
        }
    }
}
