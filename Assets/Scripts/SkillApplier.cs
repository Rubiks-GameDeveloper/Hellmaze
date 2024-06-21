using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillApplier : MonoBehaviour
{
    [SerializeField] private GameObject skillButtonUse;

    private void Start()
    {
        if (SkillsActivator.activeAbility != null)
        {
            for (int d = 0; d < skillButtonUse.transform.childCount; d++)
            {
                skillButtonUse.transform.GetChild(d).gameObject.SetActive(false);
                if (skillButtonUse.transform.GetChild(d).name == SkillsActivator.activeAbility.skill_Icon.name)
                {
                    skillButtonUse.transform.GetChild(d).gameObject.SetActive(true);

                    AbilityUse(SkillsActivator.activeAbility);
                }
            }
        }
        else
        {
            for (int o = 0; o < skillButtonUse.transform.childCount; o++)
            {
                skillButtonUse.transform.GetChild(o).gameObject.SetActive(false);
            }
        }

        if (SkillsActivator.activeBoost != null)
        {
            
        }
    }

    private void AbilityUse(BoostDescripter descripter)
    {
        if (descripter.boostName == "Mark Spawner")
        {
            MarkSpawnerAbility(descripter);
        }
        else if (descripter.boostName == "Wall Breaker")
        {
            WallBreakerAbility(descripter);
        }
    }
    private void BoostUse(BoostDescripter descripter)
    {

    }
    private void SpeedBoost(BoostDescripter descripter)
    {
        FirstPersonController.walkSpeed += FirstPersonController.walkSpeed / 100 * 15;
    }
    private void MarkSpawnerAbility(BoostDescripter descripter)
    {

    }
    private void WallBreakerAbility(BoostDescripter descripter)
    {

    }
}
