using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordScript : MonoBehaviour
{
    bool canDealDamege;
    List<GameObject> hasDealtDamage;

    [SerializeField] float weaponLenght;
    [SerializeField] float weapoDamage;

    private void Start()
    {
        canDealDamege = false;
        hasDealtDamage = new List<GameObject>();
    }
    private void Update()
    {
        if (canDealDamege)
        {
            RaycastHit hit;

            int layermask = 1 << 9;

            if (Physics.Raycast(transform.position,-transform.up,out hit , weaponLenght,layermask))
            {
                if (!hasDealtDamage.Contains(hit.transform.gameObject)&&hit.transform.TryGetComponent(out AttackState attackState))
                {
                    if (hit.transform.CompareTag("enemy"))
                    {
                        attackState.TakeDamage(weapoDamage);
                        attackState.HitVFX(hit.point);
                        
                        hasDealtDamage.Add(hit.transform.gameObject);
                    }
                  
                    print("damage");
                }
            }
        }
    }

    public void StardDealDamage()
    {
        canDealDamege = true;
        hasDealtDamage.Clear();
    }
    public void EnddDealDamage()
    {
        canDealDamege = false;
      
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position,transform.position-transform.up*weaponLenght);
    }
}
