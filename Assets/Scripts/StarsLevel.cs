using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;

public class StarsLevel : MonoBehaviour
{
    public static bool isTimeExceeded = false;

    public static UnityEvent isExitEnter = new UnityEvent();

    [SerializeField] private TextMeshProUGUI text_Timer;
    [SerializeField] private GameObject ratingPanel;
    private void Start()
    {
        ratingPanel.SetActive(false);

        isTimeExceeded = false;
        text_Timer.gameObject.SetActive(false);
        isExitEnter.AddListener(RatingPanelOpen);
    }
    public void OnTimerAnimationOver()
    {
        text_Timer.text = "";
        text_Timer.gameObject.SetActive(true);
        if (MainMenuController.SelectedDifficulty.time <= 0) return;
        StartCoroutine(Timer(MainMenuController.SelectedDifficulty.time));
    }
    public void RatingPanelOpen()
    {
        ratingPanel.SetActive(true);
        ratingPanel.GetComponent<Animator>().SetTrigger("ratingOpen");
        StopAllCoroutines();
    }
    public void MazeExit()
    {
        MainMenuController.Scores += RatingPanels.LevelRating;

        PlayerPrefs.SetInt("Scores", MainMenuController.Scores);

        SceneConnector.SceneTransition("MainMenu", LoadSceneMode.Single);
    }

    private IEnumerator Timer(int time)
    {
        for (int t = 0; t <= time; t++)
        {
            var i = time - t;
            text_Timer.text = i.ToString() + "s";
            yield return new WaitForSeconds(1);
        }

        isTimeExceeded = true;
    }
}
