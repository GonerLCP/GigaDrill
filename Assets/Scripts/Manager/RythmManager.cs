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

    public List<Sprite> CircleMatchinButtons = new List<Sprite>();
    public GameObject CenterButton;
    public List<Sprite> CenterMatchinButtons = new List<Sprite>();

    public int increment;
    SpriteRenderer sprite;

    private GameManager _gm;

    public AudioSource audioSource;

    public AudioClip drill;
    public AudioClip gleam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null) { Instance = this; }
        _gm = GameManager.Instance;
        sprite = GetComponent<SpriteRenderer>();
        RythmWindow = false;
        increment = 0;
        RythmCompleted = false;
        //this.transform.parent.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void SetRythmWindow() //Indique quand on est dans la fenetre d'opportunit�. C'es tappell� depuis l'animator
    {
        RythmWindow = true; //Attention, peut cr�er des probl�mes si on exit l'anim pas au bon moment � pr�ter attention
        audioSource.PlayOneShot(gleam, 0.1f);
        //RythmWindow = RythmWindow ? false : true;
    }

    public void ChangeButton()//Change le bouton � appuyer ainsi que les assets qui vont avec, donc le centre et le cercle du QTE
    {
        increment++;
        increment = increment > 3 ? 0 : increment;
        _gm.ActivePlayer.PlayerAnimator.SetInteger("0Y1B2A3X", increment);
        sprite.sprite = CircleMatchinButtons[increment];
        CenterButton.GetComponent<SpriteRenderer>().sprite = CenterMatchinButtons[increment];
    }

    public void RythmEnd()//Appel� � la fin de l'animator
    {
        RythmWindow = false;

        if (RythmCompleted == true)//Si on � fait une touche, que c'�tait la bonne et que c'�tait dans les temps alors
        {
            ChangeButton();
            RythmCompleted = false;
            return;
        }
        else //sinon boum boum
        {
            _gm.ActivePlayer.drilling=false;
            _gm.ActivePlayer.Explosion();
        }
    }

    public void AddImpulsion()
    {
        _gm.ActivePlayer.Impulsion = _gm.ActivePlayer.ImpulsionSetter;
        audioSource.PlayOneShot(drill, 0.1f);
    }
}
