using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    MovementHandler player_Movement;
    PlayerTrigger player_Trigger;
    PlayerStorage player_Storage;
    PlayerBuffControl player_BuffControl;


    PlayerBehaviour currentBehaviour;
    InGameUI gameUI;
    void Start()
    {
        GetComponents();
    }

    void GetComponents()
    {
        gameUI = FindObjectOfType<InGameUI>();
        currentBehaviour = GetComponent<PlayerBehaviour>();
        player_Movement = GetComponent<MovementHandler>();
        player_Trigger = GetComponentInChildren<PlayerTrigger>();
        player_Storage = GetComponentInChildren<PlayerStorage>();
        player_BuffControl = GetComponentInChildren<PlayerBuffControl>();   
    }

    public void StageTrigger(GameObject area)
    {
        player_Movement.speed = 0;
        player_Storage.StageForce();
        BuffTrigger(false);
        area.GetComponent<CollectableArea>().SetPlayer(currentBehaviour);
    }
    public void BuffTrigger(bool control)
    {
        player_BuffControl.SetBuff(control);
    }
    public void CompleteStage()
    {
        player_Movement.speed = 4;
    }
    public void LevelEnd()
    {
        player_Movement.speed = 0;
        StartCoroutine(gameUI.LevelComplete());
    }
    public void LevelStart()
    {
        player_Movement.speed = 4;
    }
}
