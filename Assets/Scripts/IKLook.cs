using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class IKLook : MonoBehaviour
{
    Animator anim;
    Camera mainCam;
    WeaponController weaponController;
    float wight = 0.4f;
    void Start()
    {
        anim = GetComponent<Animator>();
        mainCam = Camera.main;
        weaponController = GetComponent<WeaponController>();
    }

    private void OnAnimatorIK(int layerIndex)
    {

          anim.SetLookAtWeight(wight, .5f, 1.2f, .5f, .5f);
        //anim.SetLookAtWeight(1.0f, 0.5f, 1.0f, 0.7f, 0.5f);
        Ray lookAtRay = new Ray(transform.position, mainCam.transform.forward);
            anim.SetLookAtPosition(lookAtRay.GetPoint(25));
        

    }

    public void art()
    {
        wight = Mathf.Lerp(wight, 0.6f, Time.fixedDeltaTime);
    }

    public void azalt()
    {
        wight = Mathf.Lerp(wight, 0, Time.fixedDeltaTime);
    }


}
