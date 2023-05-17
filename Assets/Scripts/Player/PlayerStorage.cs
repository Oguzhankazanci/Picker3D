using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStorage : MonoBehaviour
{
    List<Rigidbody> balls;

    private void Start()
    {
        balls = new List<Rigidbody>();
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag=="Collectable")
        {
            AddList(coll.GetComponent<Rigidbody>());
        }
    }
    private void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Collectable")
        {
            RemoveList(coll.GetComponent<Rigidbody>());
        }
    }
    void AddList(Rigidbody Obj)
    {
        balls.Add(Obj);
    }

    void RemoveList(Rigidbody Obj)
    {
        if (balls.Contains(Obj))
        {
            balls.Remove(Obj);
        }

    }
    public void StageForce()
    {
        foreach (Rigidbody item in balls)
        {
            item.AddForce(Vector3.forward * 10f,ForceMode.VelocityChange);
        }
        balls.Clear();
    }

}
