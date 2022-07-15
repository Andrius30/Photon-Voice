using Photon.Pun;
using Photon.Voice.PUN;
using Photon.Voice.Unity;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviourPun
{
    public static Action<PlayerController> onPlayerSpawned;

    [SerializeField] float speed;
    [SerializeField] CharacterController characterController;
    [SerializeField] PhotonVoiceView voiceView;
    [SerializeField] Image recordingSprite;
    [SerializeField] Image speakingSprite;

    float horizontal;
    float vertical;

    void Awake()
    {
        recordingSprite.enabled = false;
        speakingSprite.enabled = false;
        if (photonView.IsMine)
        {
            CameraController.onPlayerSpawn?.Invoke(this);
            onPlayerSpawned?.Invoke(this);
            AssignRecorderOnSpawn();
        }
    }
    void Update()
    {
        if (photonView.IsMine)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            Vector3 move = new Vector3(horizontal, 0, vertical);
            characterController.Move(move * speed * Time.deltaTime);
            recordingSprite.enabled = voiceView.IsRecording;
            speakingSprite.enabled = voiceView.IsSpeaking;
            //#if !UNITY_EDITOR
            DebugRecording(voiceView.IsRecording);
            //#endif
        }
    }

    #region DEBUGING
    void DebugRecording(bool recording)
    {
        Color color = recording ? Color.green : Color.red;
        Log.log($"Recording", $"{recording}", color, false);
    }
    #endregion

    void AssignRecorderOnSpawn()
    {
        voiceView.RecorderInUse = FindObjectOfType<Recorder>();
    }

}
