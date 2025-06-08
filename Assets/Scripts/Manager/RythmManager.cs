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
    public bool RythmCompleted;

    public string[] ListOfButtons = {"Y", "B", "A", "X"};

    public List<Sprite> CircleMatchinButtons = new List<Sprite>();
    public GameObject CenterButton;
    public List<Sprite> CenterMatchinButtons = new List<Sprite>();

    public int increment;
    SpriteRenderer sprite;

    private GameManager _gm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null) { Instance = this; }
        _gm = GameManager.Instance;
        sprite = GetComponent<SpriteRenderer>();
        RythmWindow = false;
        increment = 0;
        RythmCompleted = false;
        this.transform.parent.gameObject.SetActive(false);
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
        increment++;
        increment = increment > 3 ? 0 : increment;
        _gm.ActivePlayer.PlayerAnimator.SetInteger("0Y1B2A3X", increment);
        sprite.sprite = CircleMatchinButtons[increment];
        CenterButton.GetComponent<SpriteRenderer>().sprite = CenterMatchinButtons[increment];
    }

    public void RythmEnd()
    {
        if (RythmCompleted == true)
        {
            ChangeButton();
            RythmCompleted = false;
            return;
        }
        else
        {
            _gm.ActivePlayer.drilling=false;
            _gm.ActivePlayer.Explosion();
        }
    }
}
