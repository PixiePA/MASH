using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Listener
{
    // Start is called before the first frame update
    [SerializeField] Text GameStartOverlay;
    [SerializeField] Canvas GameInstructions;

    [SerializeField] Text Score;
    [SerializeField] Text Time;

    [SerializeField] Text HighScore;
    [SerializeField] Text BestTime;

    [SerializeField] Text Passengers;


    void Start()
    {
        GameManager.AddListener(this);
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = "Score: " + GameManager.score;
        Time.text = "Time: " + GameManager.gameTime;

        HighScore.text = "High Score: " + GameManager.highScore;
        BestTime.text = "Best Time: " + GameManager.bestTime;
        Passengers.text = GameManager.passengers + "/3";
    }

    public override void GameStarted()
    {
        GameInstructions.enabled = false;
    }

    public override void GameReset()
    {
        GameStartOverlay.text = "Press Space" + "\n" + "To Start";
        GameInstructions.enabled = true;
    }

    public override void GameEnded()
    {
        GameStartOverlay.text = "Game Over" + "\n" + "Press R";
        GameInstructions.enabled = true;
    }
}
