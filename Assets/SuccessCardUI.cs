using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessCardUI : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Animator>().SetTrigger("PopUp");
    }
}
