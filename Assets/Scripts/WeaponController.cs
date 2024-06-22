using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public bool isStrafe = false;
    Animator anim;
    public GameObject handweapon;
    public GameObject shoulderweapon;
    public GameObject currentWeapon;

    public GameObject trails;
    bool canAttack = true;

    IKLook ik;

    float comboIndex = 1;
    

    void Start()
    {
        trailClose();
        anim = GetComponent<Animator>();
        ik = GetComponent<IKLook>();
        PlayerPrefs.SetInt("combo1",0);
        PlayerPrefs.SetInt("combo1",1);
        PlayerPrefs.SetInt("combo2",0);
        PlayerPrefs.SetInt("combo3",0);
      
    }

   
    void Update()
    {
        anim.SetBool("iS", isStrafe);
        if (Input.GetKeyDown(KeyCode.F)&&!anim.GetBool("isAttack"))
        {
            isStrafe = !isStrafe;
        }

        if (isStrafe)
        {
            GetComponent<MovementRigidbody>().movementType = MovementRigidbody.MovementType.Strafe;
            ik.azalt();
        }
        if (!isStrafe)
        {
            GetComponent<MovementRigidbody>().movementType = MovementRigidbody.MovementType.Directional;
            ik.art();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) &&isStrafe&&canAttack)
        {
            
           
            anim.SetTrigger("saldir");
        }
        if (Input.GetKeyDown(KeyCode.Z) && !anim.GetBool("isAttack")&&isStrafe)
        {
            if (PlayerPrefs.GetInt("combo"+comboIndex)==1)
            {
                anim.SetTrigger("combo" + comboIndex);
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && isStrafe)
        {
            anim.SetBool("block",true);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1) && isStrafe)
        {
            anim.SetBool("block", false);
        }

    }

    void equip()
    {
        shoulderweapon.SetActive(false);
        handweapon.SetActive(true);
    }

    void unequip()
    {
        shoulderweapon.SetActive(true);
        handweapon.SetActive(false);
    }

    public void trailOpen()
    {
        for (int i = 0; i < trails.transform.childCount; i++)
        {
            trails.transform.GetChild(i).gameObject.GetComponent<TrailRenderer>().emitting =true;
        }
    }
    public void trailClose()
    {
        for (int i = 0; i < trails.transform.childCount; i++)
        {
            trails.transform.GetChild(i).gameObject.GetComponent<TrailRenderer>().emitting = false;
        }
    }

    public void StartDealDamage()
    {
        currentWeapon.GetComponentInChildren<swordScript>().StardDealDamage();
    }
    public void EndDealDamage()
    {
        currentWeapon.GetComponentInChildren<swordScript>().EnddDealDamage();
    }

}
