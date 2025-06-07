using UnityEngine;

public class RythmManager : MonoBehaviour
{
    public static RythmManager Instance { get; private set; }
    public bool RythmWindow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RythmWindow = false;
        if (Instance == null) { Instance = this; } else { Destroy(this); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRythmWindow() 
    {
        print(RythmWindow);
        RythmWindow = !RythmWindow; //Attention, peut créer des problèmes si on exit l'anim pas au bon moment à préter attention
        //RythmWindow = RythmWindow ? false : true;
    }
}
