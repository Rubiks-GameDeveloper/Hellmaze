using UnityEngine;
using UnityEngine.UI;
public class AnimationQuit : MonoBehaviour
{
    [SerializeField] private Button unlockButton;

    public void OnAnimationTipStart()
    {
        unlockButton.interactable = false;
    }
    public void OnAnimationTipOver()
    {
        unlockButton.interactable = true;
    }
}
