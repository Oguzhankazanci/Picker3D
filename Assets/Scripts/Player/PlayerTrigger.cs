using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{

    PlayerBehaviour player_Behaviour;
    private void Start()
    {
        GetComponents();
    }
    void GetComponents()
    {
        player_Behaviour = GetComponentInParent<PlayerBehaviour>();
    }
    private void OnTriggerEnter(Collider coll)
    {
        switch (coll.tag)
        {
            case "CollectableArea":
                player_Behaviour.StageTrigger(coll.gameObject);
                break;
            case "PowerBuff":
                player_Behaviour.BuffTrigger(true);
                Destroy(coll.gameObject);
                break;
            case "Helicopter":
                coll.GetComponent<HelicopterController>().StartDrop();
                break;
            case "Finish":
                player_Behaviour.LevelEnd();
                break;
            default:
                break;
        }
     
    }
}
