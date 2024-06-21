using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "BoostDescripter", menuName = "Personal asset/Skill")]
public class BoostDescripter : ScriptableObject
{
    public GameObject skill_Icon;

    public string boostName;
    public string description;

    public int cost;
    public bool isAbility = false;
    public bool isUnlock = false;
    public bool isActive = false;
}
