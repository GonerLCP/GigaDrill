using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI recordText;
    
    private int score = 0;
    private int record = 0;

    private float playerHeight;

    private GameManager _gm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gm = GameManager.Instance;

        scoreText.text = "Height: " + score.ToString() + " m";
        recordText.text = "Highest: " + record.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        score = (int)_gm.ActivePlayer.transform.position.y;
        scoreText.text = "Height: " + score.ToString() + " m";
    }
}
