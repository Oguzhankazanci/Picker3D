using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectableArea : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI areaCount;
    [SerializeField] int neededCount;

    PlayerBehaviour player_Behaviour;
    InGameUI game_UI;

    List<CollectableObj> collectableObj;

    Animator areaAnim;
    BoxCollider boxcollider;
    Coroutine openRoutine;
    Coroutine failRoutine;
    int objCount;

    void Start()
    {
        GetComponents();
        SetCounter();
    }
    void GetComponents()
    {
        game_UI = FindObjectOfType<InGameUI>();
        collectableObj = new List<CollectableObj>();
        areaAnim = GetComponent<Animator>();
        boxcollider = GetComponent<BoxCollider>();
    }
    void SetCounter()
    {
        areaCount.text = objCount + "/" + neededCount;
    }
    public void AddBall(CollectableObj obj)
    {
        objCount++;
        collectableObj.Add(obj);
        CheckArea();
        SetCounter();
    }
    void CheckArea()
    {
        if (objCount >= neededCount)
        {
            StopCoroutine(failRoutine);
            IEnumeratorOpenStarter(CompleteStageDelay());
        }
        else
        {
            IEnumeratorFailStarter(FailStageDelay());
        }
    }
    public void SetPlayer(PlayerBehaviour pl)
    {
        player_Behaviour = pl;
        CheckArea();

    }
    public void CompleteEvent()
    {
        StartCoroutine(WaitPlayerMovement());
    }
    IEnumerator WaitPlayerMovement()
    {
        yield return new WaitForSeconds(0.5f);
        player_Behaviour.CompleteStage();
    }
    IEnumerator CompleteStageDelay()
    {
        yield return new WaitForSeconds(1f);
        foreach (CollectableObj item in collectableObj)
        {
            item.ObjAddEffect();
        }
        yield return new WaitForSeconds(1f);
        game_UI.StageComplete();
        areaAnim.SetTrigger("Open");
        boxcollider.enabled = false;
    }
    IEnumerator FailStageDelay()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(game_UI.LevelFail());

    }
    void IEnumeratorOpenStarter(IEnumerator currentRoutine)
    {
        if (openRoutine == null)
        {
            openRoutine = StartCoroutine(currentRoutine);
        }

    }
    void IEnumeratorFailStarter(IEnumerator currentRoutine)
    {

        if (failRoutine == null)
        {
            failRoutine = StartCoroutine(currentRoutine);
        }
        else
        {
            StopCoroutine(failRoutine);
            failRoutine = StartCoroutine(currentRoutine);
        }


    }
    public void SetNeededCount(int count)
    {
        neededCount = count;
        SetCounter();
    }
}
