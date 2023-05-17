using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterController : MonoBehaviour
{
    [SerializeField] Transform spawnPosition;

    Animator anim;
    Coroutine flyRoutine;
    bool startFly;

    private void Start()
    {
        GetComponent();
    }
    void GetComponent()
    {
        anim = GetComponent<Animator>();
    }
    public void StartDrop()
    {
        if (!startFly)
        {
            startFly = true;
            anim.SetTrigger("Fly");
            flyRoutine = StartCoroutine(DropRoutine());
        }

    }
    IEnumerator DropRoutine()
    {
        while (startFly)
        {
            yield return new WaitForSeconds(0.1f);
            DropObject();
        }
      
    }
    void DropObject()
    {
        Debug.Log("test");
        ObjectPooler.Instance.SpawnObject(PoolType.Sphere, spawnPosition, spawnPosition.transform.rotation);
    }
    public void EndMovementTrigger()
    {
        startFly = false;
        StopCoroutine(flyRoutine);
    }
}
