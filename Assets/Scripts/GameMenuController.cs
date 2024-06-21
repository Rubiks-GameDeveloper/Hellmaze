using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameMenuController : MonoBehaviour
{
    private bool isGameMenuOpen = false;

    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject openButton;
    [SerializeField] private GameObject SettingsPanel;

    [SerializeField] private TextMeshProUGUI valueOutput_MouseSensivity;
    [SerializeField] private TextMeshProUGUI valueOutput_CameraFOV;
    [SerializeField] private Slider slider_MouseSensivity;
    [SerializeField] private Slider slider_CameraFOV;

    private void Start()
    {
        Application.targetFrameRate = 120;
        menu.SetActive(false);
        openButton.SetActive(true);
        SettingsPanel.SetActive(false);
        
        MazeGenerator.MazeSpawning(MainMenuController.SelectedDifficulty);
        
        SettingsLoad();
    }
    public void GameMenuOpen()
    {
        if (isGameMenuOpen)
        {
            SettingsPanel.SetActive(false);
            isGameMenuOpen = false;
        }
        else if (!isGameMenuOpen)
        {
            isGameMenuOpen = true;
        }
    }
    public void OnSliderSensivityValueChanged()
    {
        FirstPersonController.mouseSensitivity = (float)Math.Round(slider_MouseSensivity.value, 2);
        valueOutput_MouseSensivity.text = FirstPersonController.mouseSensitivity.ToString();
    }
    public void OnSliderFOVValueChanged()
    {
        FirstPersonController.fov = (float)Math.Round(slider_CameraFOV.value, 1);
        Camera.main.fieldOfView = FirstPersonController.fov;
        valueOutput_CameraFOV.text = FirstPersonController.fov.ToString();
    }
    public void SettingsApplied()
    {
        PlayerPrefs.SetFloat("Sensivity", FirstPersonController.mouseSensitivity);
        PlayerPrefs.SetFloat("FOV", FirstPersonController.fov);
    }
    public void SettingsLoad()
    {
        FirstPersonController.mouseSensitivity = PlayerPrefs.GetFloat("Sensivity", 0.1f);
        FirstPersonController.fov = PlayerPrefs.GetFloat("FOV", 60f);
        Application.targetFrameRate = 90;

        Camera.main.fieldOfView = FirstPersonController.fov;

        slider_MouseSensivity.value = FirstPersonController.mouseSensitivity;
        slider_CameraFOV.value = FirstPersonController.fov;

        valueOutput_MouseSensivity.text = FirstPersonController.mouseSensitivity.ToString();
        valueOutput_CameraFOV.text = FirstPersonController.fov.ToString();
    }
}
