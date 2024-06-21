using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SkillsActivator : MonoBehaviour
{
    public BoostDescripter[] all_Skills;

    public static BoostDescripter activeBoost;
    public static BoostDescripter activeAbility;

    private BoostDescripter selectedSkill;

    [SerializeField] private TextMeshProUGUI cost_info;
    [SerializeField] private TextMeshProUGUI skillName_info;
    [SerializeField] private TextMeshProUGUI skillDescription_info;
    [SerializeField] private Button unlock_Button;
    [SerializeField] private Button select_Button;
    [SerializeField] private Button deselect_Button;
    [SerializeField] private GameObject activeBoost_gameObject;
    [SerializeField] private GameObject activeAbility_gameObject;

    [SerializeField] private GameObject notEnoughScoresTip;

    private Animator animTip;

    private void Start()
    {
        if (PlayerPrefs.HasKey("activeAbility"))
        {
            activeAbility_gameObject.transform.GetChild(PlayerPrefs.GetInt("activeAbility")).gameObject.SetActive(true);

            for (int p = 0; p < all_Skills.Length; p++)
            {
                if (all_Skills[p].skill_Icon.name == activeAbility_gameObject.transform.GetChild(PlayerPrefs.GetInt("activeAbility")).name)
                {
                    activeAbility = all_Skills[p];
                }
            }
        }
        if (PlayerPrefs.HasKey("activeBoost"))
        {
            activeBoost_gameObject.transform.GetChild(PlayerPrefs.GetInt("activeBoost")).gameObject.SetActive(true);

            for (int p = 0; p < all_Skills.Length; p++)
            {
                if (all_Skills[p].skill_Icon.name == activeBoost_gameObject.transform.GetChild(PlayerPrefs.GetInt("activeBoost")).name)
                {
                    activeBoost = all_Skills[p];
                }
            }
        }

        unlock_Button.interactable = false;
        select_Button.interactable = false;

        animTip = notEnoughScoresTip.GetComponent<Animator>();
    }
    public void SkillInformation(GameObject skill)
    {
        for (int c = all_Skills.Length - 1; c >= 0; c--)
        {
            if (all_Skills[c].name == skill.name)
            {
                skillName_info.text = all_Skills[c].boostName;
                skillDescription_info.text = all_Skills[c].description;
                cost_info.text = all_Skills[c].cost.ToString();

                selectedSkill = all_Skills[c];

                if (!all_Skills[c].isUnlock) 
                { 
                    unlock_Button.interactable = true;
                    select_Button.interactable = false;
                    deselect_Button.interactable = false;
                }
                else if (!all_Skills[c].isActive && all_Skills[c].isUnlock)
                {
                    unlock_Button.interactable = false;
                    select_Button.interactable = true;
                    deselect_Button.interactable = false;
                }
                else if (all_Skills[c].isActive && all_Skills[c].isUnlock)
                {
                    unlock_Button.interactable = false;
                    select_Button.interactable = false;
                    deselect_Button.interactable = true;
                }
            } 
        }
    }
    public void SkillUnlock()
    {
        if (MainMenuController.Scores >= selectedSkill.cost)
        {
            MainMenuController.Scores -= selectedSkill.cost;
            selectedSkill.isUnlock = true;

            unlock_Button.interactable = false;
            select_Button.interactable = true;
        }
        else
        {
            animTip.SetTrigger("tipActive");
        }
    }
    public void SkillActivate()
    {
        if (selectedSkill.isAbility)
        {
            selectedSkill.isActive = true;

            activeAbility = selectedSkill;

            select_Button.interactable = false;
            deselect_Button.interactable = true;

            for (int i = activeAbility_gameObject.transform.childCount - 1; i >= 0; i--)
            {
                if (activeAbility_gameObject.transform.GetChild(i).name == activeAbility.skill_Icon.name)
                {
                    activeAbility_gameObject.transform.GetChild(i).gameObject.SetActive(true);

                    PlayerPrefs.SetInt("activeAbility", i);

                    if (i + 1 < activeAbility_gameObject.transform.childCount)
                    {
                        activeAbility_gameObject.transform.GetChild(i + 1).gameObject.SetActive(false);
                    }
                    else
                    {
                        activeAbility_gameObject.transform.GetChild(i - 1).gameObject.SetActive(false);
                    }
                }
            }     
        }
        else if (!selectedSkill.isAbility)
        {
            selectedSkill.isActive = true;

            activeBoost = selectedSkill;

            select_Button.interactable = false;

            for (int d = activeBoost_gameObject.transform.childCount - 1; d >= 0; d--)
            {
                if (activeBoost_gameObject.transform.GetChild(d).name == activeBoost.skill_Icon.name)
                {
                    activeBoost_gameObject.transform.GetChild(d).gameObject.SetActive(true);

                    PlayerPrefs.SetInt("activeBoost", d);
                    /*
                    if (i + 1 < activeBoost_gameObject.transform.childCount)
                    {
                        activeBoost_gameObject.transform.GetChild(i + 1).gameObject.SetActive(false);
                    }
                    else
                    {
                        activeBoost_gameObject.transform.GetChild(i - 1).gameObject.SetActive(false);
                    }*/
                }
            }
        }
    }
    public void SkillDeActivete()
    {
        if (selectedSkill.isAbility)
        {
            activeAbility.isActive = false;
            activeAbility = null;

            deselect_Button.interactable = false;
            select_Button.interactable = true;

            for (int u = 0; u < activeAbility_gameObject.transform.childCount; u++)
            {
                activeAbility_gameObject.transform.GetChild(u).gameObject.SetActive(false);
            }
        }
        else if (!selectedSkill.isAbility)
        {
            activeBoost.isActive = false;
            activeBoost = null;

            deselect_Button.interactable = false;
            select_Button.interactable = true;

            for (int u = 0; u < activeBoost_gameObject.transform.childCount; u++)
            {
                activeBoost_gameObject.transform.GetChild(u).gameObject.SetActive(false);
            }
        }
    }
}
