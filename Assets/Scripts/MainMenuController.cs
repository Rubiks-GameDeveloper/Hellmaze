using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class MainMenuController : MonoBehaviour
{
    public static int Scores;
    public static MazeDifficulty SelectedDifficulty { get; set; }

    [SerializeField] private MazeDifficulty startMazeDifficulty;
    [SerializeField] private Button StartButton;
    [SerializeField] private TextMeshProUGUI g_Scores;
    [SerializeField] private GameObject CameraRotationPoint;
    
    private void Start()
    {
        Application.targetFrameRate = 120;
        Scores = PlayerPrefs.GetInt("Scores");
        g_Scores.text = Scores.ToString();
        MazeGenerator.MazeSpawning(startMazeDifficulty);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void SetMazeDifficulty(MazeDifficulty difficulty)
    {
        SelectedDifficulty = difficulty;
        if (!StartButton.interactable) StartButton.interactable = true;
    }
    public void GameSceneLoad()
    {
        SceneConnector.SceneTransition("Game", LoadSceneMode.Single);
    }
    public void MainMenuSceneLoad()
    {
        SceneConnector.SceneTransition("MainMenu", LoadSceneMode.Single);
    }
    public void Cheat()
    {
        Scores++;

        g_Scores.text = Scores.ToString();
    }
    private void FixedUpdate()
    {
        CameraRotationPoint.transform.Rotate(new Vector3(0, 0.1f, 0));
    }
}
