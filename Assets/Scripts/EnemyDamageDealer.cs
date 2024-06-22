using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageDealer : MonoBehaviour
{
    bool canDealDamage;
    bool hasDealtDamage;

    [SerializeField] float weaponLenght;
    [SerializeField] float weaponDamage;
    void Start()
    {
        canDealDamage = false;
        hasDealtDamage = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canDealDamage && !hasDealtDamage)
        {
            RaycastHit hit;
            int layermask = 1 << 11;
            if (Physics.Raycast(transform.position,-transform.up,out hit,weaponLenght,layermask))
            {
              
                if (hit.transform.TryGetComponent(out HealthSystem healths))
                {
                    healths.TakeDamage(weaponDamage);
                    healths.HitVFX(hit.point);
                    hasDealtDamage = true;
                } 
            }
        }
    }

    public void StartDealDamage()
    {
        canDealDamage = true;
        hasDealtDamage = false;
    }
    public void EndDealDamage()
    {
        canDealDamage = false;
      
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position,transform.position-transform.up *weaponLenght);
    }
}
