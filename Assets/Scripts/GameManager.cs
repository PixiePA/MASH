using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum GameState { Start, Playing, GameOver };
    static public GameState state;
    static List<Listener> GameListeners = new List<Listener>();
    static public int score;
    static public int highScore;
    static public float gameTime = 0;
    static public float bestTime;
    static public int passengers;
    [SerializeField] GameObject map;
    static MapGeneratorScript mapGen;
    // Start is called before the first frame update
    void Start()
    {
        mapGen = map.GetComponent<MapGeneratorScript>();
        state = GameState.Start; //Set Game State

        if (PlayerPrefs.HasKey("highScore"))
        {
            highScore = PlayerPrefs.GetInt("highScore");
        }
        else
            highScore = 0;//Getting stores high score


        if (PlayerPrefs.HasKey("bestTime"))
        {
            bestTime = PlayerPrefs.GetFloat("bestTime");
        }
        else
            bestTime = Mathf.Infinity;

        mapGen.GenerateMap();

        //StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.Start && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }

        if ((state == GameState.GameOver || state == GameState.Playing) && Input.GetKeyDown(KeyCode.R))
        {
            EndGame();
            Reset();
        }

        if (state == GameState.Playing)
        {
            gameTime += Time.deltaTime;
        }


    }

    static public void StartGame()
    {
        gameTime = 0;
        score = 0;

        foreach (Listener listener in GameListeners)
        {
            listener.GameStarted();
        }

        state = GameState.Playing;



    }

    static public void EndGame()
    {
        if (score > highScore)
        {
            PlayerPrefs.SetInt("highScore", score);
            PlayerPrefs.SetFloat("bestTime", gameTime);
            highScore = score;
            bestTime = gameTime;

        }

        else if (score == highScore && gameTime < bestTime)
        {
            PlayerPrefs.SetFloat("bestTime", gameTime);
            bestTime = gameTime;
        }

        foreach (Listener listener in GameListeners)
        {
            listener.GameEnded();
        }

        state = GameState.GameOver;
    }

    static public void Reset()
    {
        foreach (Listener listener in GameListeners)
        {
            listener.GameReset();
        }

        state = GameState.Start;
    }

    static public void AddListener(Listener listener)
    {
        GameListeners.Add(listener);
    }

    static public void MapReset()
    {
        mapGen.SpawnNewTrees();
    }

    static public void UpdatePassengers(int amount)

    {
        passengers = amount;
    }
}
