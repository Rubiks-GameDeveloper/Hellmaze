using UnityEngine;
public class LabirintExit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        StarsLevel.isExitEnter.Invoke();
    }
}
