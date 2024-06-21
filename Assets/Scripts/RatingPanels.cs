using UnityEngine;
using UnityEngine.UI;
public class RatingPanels : MonoBehaviour
{
    [SerializeField] private Image circleFill_Points;
    [SerializeField] private Image circleFill_Time;

    [SerializeField] private GameObject cross_Points;
    [SerializeField] private GameObject checkmark_Points;

    [SerializeField] private GameObject cross_Time;
    [SerializeField] private GameObject checkmark_Time;

    [SerializeField] private GameObject firstStar;
    [SerializeField] private GameObject secondStar;
    [SerializeField] private GameObject thirdStar;

    [SerializeField] private Color red;
    [SerializeField] private Color green;
    [SerializeField] private Color yellow;
    [SerializeField] private Color darkGrey;

    public static int LevelRating = 1;
    private void Start()
    {
        StarsLevel.isExitEnter.AddListener(LevelTaskUpdater);
    }
    public void LevelTaskUpdater()
    {
        if (MainMenuController.SelectedDifficulty.pointCount == MazeGenerator.LevelScore)
        {
            circleFill_Points.color = green;
            checkmark_Points.SetActive(true);
            cross_Points.SetActive(false);

            LevelRating++;
        }
        else
        {
            circleFill_Points.color = red;
            checkmark_Points.SetActive(false);
            cross_Points.SetActive(true);
        }
        if (!StarsLevel.isTimeExceeded)
        {
            circleFill_Time.color = green;
            checkmark_Time.SetActive(true);
            cross_Time.SetActive(false);

            LevelRating++;
        }
        else
        {
            circleFill_Time.color = red;
            checkmark_Time.SetActive(false);
            cross_Time.SetActive(true);
        }

        if (LevelRating >= 1) firstStar.GetComponent<Image>().color = yellow;
        else firstStar.GetComponent<Image>().color = darkGrey;
        if (LevelRating >= 2) secondStar.GetComponent<Image>().color = yellow;
        else secondStar.GetComponent<Image>().color = darkGrey;
        if (LevelRating == 3) thirdStar.GetComponent<Image>().color = yellow;
        else thirdStar.GetComponent<Image>().color = darkGrey;
    }
}
