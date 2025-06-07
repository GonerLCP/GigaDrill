using NUnit.Framework;
using System.Globalization;
using UnityEngine;
using UnityEngine.Splines;
using System.Collections;
using System.Collections.Generic;

public class RythmManager : MonoBehaviour
{
    public static RythmManager Instance { get; private set; }
    public bool RythmWindow;
    public string[] ListOfButtons = {"Y", "B", "A", "X"};
    public List<Sprite> CircleMatchinButtons = new List<Sprite>();
    public string buttonToPress;
    int increment;
    SpriteRenderer sprite;

    private GameManager _gm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null) { Instance = this; } else { Destroy(this); }
        _gm = GameManager.Instance;
        sprite = GetComponent<SpriteRenderer>();
        RythmWindow = false;
        increment = 0;
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

    public void ChangeButton()
    {
        _gm.ActivePlayer.PlayerAnimator.SetInteger("0Y1B2A3X", increment);
        buttonToPress = ListOfButtons[increment];
        sprite.sprite = CircleMatchinButtons[increment];
        increment++;
        increment = increment > 3 ? 0 : increment;
        print(buttonToPress);
    }
}
