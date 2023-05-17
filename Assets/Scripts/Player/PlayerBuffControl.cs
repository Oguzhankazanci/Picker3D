using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffControl : MonoBehaviour
{
    [SerializeField] GameObject Buff;

    public void SetBuff(bool Control)
    {
        Buff.SetActive(Control);
    }

}
