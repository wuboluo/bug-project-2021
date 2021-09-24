using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atk_Fire : MonoBehaviour
{
    private Animator Atk_Fire_Switch;

    // Start is called before the first frame update
    void Start()
    {
        Atk_Fire_Switch = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetSwitch()
    {
        Atk_Fire_Switch.SetBool("shoot", false);
    }
}