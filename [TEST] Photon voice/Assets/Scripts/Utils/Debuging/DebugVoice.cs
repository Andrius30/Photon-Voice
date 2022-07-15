using Photon.Voice.PUN;
using Photon.Voice.Unity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugVoice : MonoBehaviour
{
    [SerializeField] List<Toggle> toggles;

    [SerializeField] PhotonVoiceView photonVoiceView;
    [SerializeField] Recorder recorder;

    #region Toggles
    void OnCharacterSpawn(PlayerController player)
    {
        photonVoiceView = player.GetComponent<PhotonVoiceView>();
        recorder = photonVoiceView.RecorderInUse;
        foreach (var toggle in toggles)
        {
            InitToggles(toggle);
            AddToggleListeners(toggle);
        }
    }
    bool LoadSettings(Toggle toggle)
    {
        bool isOn = default;
        if (PlayerPrefs.HasKey(toggle.name))
        {
            isOn = bool.Parse(PlayerPrefs.GetString(toggle.name, toggle.isOn.ToString()));
            Debug.Log($"Initializing toggles {toggle.name} isOn {isOn}");
        }
        return isOn;
    }
    void InitToggles(Toggle toggle)
    {
        switch (toggle.name)
        {
            case "Transmit":
                toggle.isOn = LoadSettings(toggle);
                UpdateToggles(toggle);
                break;
            case "Echo":
                toggle.isOn = LoadSettings(toggle);
                UpdateToggles(toggle);
                break;
        }
    }
    void AddToggleListeners(Toggle toggle)
    {
        switch (toggle.name)
        {
            case "Transmit":
                toggle.onValueChanged.AddListener((isOn) =>
                {
                    UpdateToggles(toggle);
                });
                break;
            case "Echo":
                toggle.onValueChanged.AddListener((isOn) =>
                {
                    UpdateToggles(toggle);
                });
                break;
        }

    }
    void UpdateToggles(Toggle toggle)
    {
        switch (toggle.name)
        {
            case "Transmit":
                recorder.TransmitEnabled = toggle.isOn;
                Debug.Log($"Transmit {recorder.TransmitEnabled}");
                PlayerPrefs.SetString(toggle.name, toggle.isOn.ToString());
                break;
            case "Echo":
                recorder.DebugEchoMode = toggle.isOn;
                Debug.Log($"Echo {recorder.DebugEchoMode}");
                PlayerPrefs.SetString(toggle.name, toggle.isOn.ToString());
                break;
        }
    } 
    #endregion


    void OnEnable() => PlayerController.onPlayerSpawned += OnCharacterSpawn;
    void OnDisable() => PlayerController.onPlayerSpawned -= OnCharacterSpawn;
}
