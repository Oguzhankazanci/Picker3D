using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObj : MonoBehaviour
{
    [SerializeField] GameObject Pieces;

    CollectableObj currentObj;

    bool added;

    void Start()
    {
        GetComponents();
    }
    void GetComponents()
    {
        currentObj = GetComponent<CollectableObj>();
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "CollectableAreaIn"&& !added)
        {
            added = true;
            coll.transform.parent.parent.GetComponent<CollectableArea>().AddBall(currentObj);
            //ObjAddEffect();
        }
    }
    public void ObjAddEffect()
    {
        Rigidbody NewObjRb;
        NewObjRb = Instantiate(Pieces, transform.position, Pieces.transform.rotation, transform.parent).GetComponent<Rigidbody>();
        NewObjRb.transform.eulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        NewObjRb.AddForce(Vector3.up * 150f);
        Destroy(gameObject);
    }
}
