using UnityEngine;
using UnityEngine.UI;
public class RatingPanelAnimationTrigger : MonoBehaviour
{
    public void OnRatingPanelAnimationOver()
    {
        GetComponent<Image>().color += new Color(0, 0, 0, 1);
        transform.Find("Panel").gameObject.SetActive(true);
    }
}
