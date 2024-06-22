using UnityEngine;
using TMPro;



public class Rifle : MonoBehaviour
{
    [Header("Fire")]
    public bool readyFire;
    private float insidefiringfrequency;
    public float firingfrequencyy;
    public float range;
    public float power = 35f;

    [Header("Effects")]
    public ParticleSystem FireEffect;
    public ParticleSystem BulletHoleEffect;
    public ParticleSystem Blood;

    [Header("Sounds")]
    public AudioSource Sarjorsound;
    public AudioSource FireSound;
    public AudioSource BulletEnd;
    public AudioSource TakeAmmo;
    public AudioSource TakeHealth;

    [Header("Others")]
    public Camera cam;
    public Animator anim;
    public float camfieldpov;
    public float approachpov = 40;
    public bool openscoop;
    [Header("Gun Settings")]
    public int TotalBullets;
    public int MagazineCapacity;
    public int RemainingBullets;
    public string gun_name;
    public TMP_Text TotalBulletsText;
    public TMP_Text RemainingBulletsText;

    int spendbullet;
   
    [Header("Gun Change Settings")]
    public GameObject pistol;
    public Animator animpistol;


  
    int countbulletholes;
    public Transform spherePoint;

    [Header("ObjectPool")]
    public int bulletIndex;
    public ParticleSystem[] bulletjolse;

    private void Start()
    {

        PlayerPrefs.SetInt("Rifle_Bullet", 50);
     
        readyFire = true;
        TotalBullets = PlayerPrefs.GetInt(gun_name + "_Bullet");
        starting_bullet_filling();
        bulletstate("print");
      
    }
    public void fire()
    {
        if (RemainingBullets != 0 && !openscoop)
        {
            RemainingBullets--;
            RemainingBulletsText.text = RemainingBullets.ToString();
           // anim.Play("ateset2");
          //  FireEffect.Play();
          //  FireSound.Play();
        }
        else if (openscoop && RemainingBullets != 0)
        {
            RemainingBullets--;
            RemainingBulletsText.text = RemainingBullets.ToString();
            //anim.SetTrigger("scoopfire");
          //  anim.Play("scopfire");
           // FireEffect.Play();
           // FireSound.Play();
        }
        else
        {
           // BulletEnd.Play();
        }

        RaycastHit hit;
        Debug.Log("a");
        if (readyFire && Time.time > insidefiringfrequency && RemainingBullets != 0)
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
            {

                Debug.Log(hit.transform.name);


                //Mermi izi ve kan efekti için kullanýlabilir
                if (hit.transform.gameObject.CompareTag("default"))
                {
                    Debug.Log(hit.transform.name);
                    /*bulletIndex++;
                    ParticleSystem p =  bulletjolse[bulletIndex];
                    p.transform.gameObject.SetActive(true);
                    p.transform.position = spherePoint.position;
                    p.transform.LookAt(transform.position);*/
                     // hit.transform.gameObject.GetComponent<EnemyScript>().HealthManagement2(power);
                    ParticleSystem p = Instantiate(Blood, spherePoint.position, Quaternion.LookRotation(hit.normal));
                     p.Play();
                   /* ParticleSystem particle = bullethole2();
                    //particle.gameObject.SetActive(true);
                    particle.transform.position = hit.transform.position;
                    particle.transform.rotation = Quaternion.LookRotation(hit.normal);
                    particle.Play();*/



                }
              /*  else if (hit.transform.gameObject.CompareTag("default"))
                {
                   // hit.transform.gameObject.GetComponent<EnemyScript2>().HealthManagement2(power);
                    ParticleSystem p = Instantiate(Blood, spherePoint.position, Quaternion.LookRotation(hit.normal));
                    p.Play();
                }else if (hit.transform.gameObject.CompareTag("default"))
                {
                   // hit.transform.gameObject.GetComponent<enemyscript3>().HealthManagement2(power);
                    ParticleSystem p = Instantiate(Blood, spherePoint.position, Quaternion.LookRotation(hit.normal));
                    p.Play();
                }
                else
                {
                    if (!hit.transform.gameObject.CompareTag("default"))
                    {
                        Instantiate(BulletHoleEffect, spherePoint.position, Quaternion.LookRotation(hit.normal));
                    }

                }*/

            }
            insidefiringfrequency = Time.time + firingfrequencyy;

        }



    }

    private void Update()
    {
        Debug.DrawLine(cam.transform.position, cam.transform.forward * 1000, Color.red);
        if (Input.GetMouseButtonDown(0))
        {
            fire();
        }
    }
    void starting_bullet_filling()
    {
        if (MagazineCapacity >= TotalBullets)
        {
            RemainingBullets = TotalBullets;
            TotalBullets = 0;

            PlayerPrefs.SetInt(gun_name + "_Bullet", TotalBullets);

        }
        else
        {
            TotalBullets -= MagazineCapacity;
            RemainingBullets = MagazineCapacity;
            PlayerPrefs.SetInt(gun_name + "_Bullet", TotalBullets);
        }
    }
    void bulletstate(string state)
    {
        switch (state)
        {
            case "bullet":
                if (TotalBullets <= MagazineCapacity)
                {
                    int pulse = RemainingBullets += TotalBullets;
                    if (pulse > MagazineCapacity)
                    {
                        TotalBullets = pulse - MagazineCapacity;
                        RemainingBullets = MagazineCapacity;
                        PlayerPrefs.SetInt(gun_name + "_Bullet", TotalBullets);
                    }
                    else if (pulse < MagazineCapacity)
                    {
                        RemainingBullets = pulse;
                        TotalBullets = 0;
                        PlayerPrefs.SetInt(gun_name + "_Bullet", TotalBullets);
                    }
                    else
                    {
                        TotalBullets = 0;
                        RemainingBullets += TotalBullets;
                        PlayerPrefs.SetInt(gun_name + "_Bullet", TotalBullets);
                    }

                }
                else
                {
                    spendbullet = MagazineCapacity - RemainingBullets;
                    TotalBullets -= spendbullet;
                    RemainingBullets = MagazineCapacity;
                    PlayerPrefs.SetInt(gun_name + "_Bullet", TotalBullets);
                }
                TotalBulletsText.text = TotalBullets.ToString();
                RemainingBulletsText.text = RemainingBullets.ToString();
                break;
            case "nobullet":
                if (TotalBullets <= MagazineCapacity)
                {
                    RemainingBullets = TotalBullets;
                    TotalBullets = 0;
                    PlayerPrefs.SetInt(gun_name + "_Bullet", TotalBullets);
                }
                else
                {
                    TotalBullets -= MagazineCapacity;
                    RemainingBullets = MagazineCapacity;
                    PlayerPrefs.SetInt(gun_name + "_Bullet", TotalBullets);
                }

                TotalBulletsText.text = TotalBullets.ToString();
                RemainingBulletsText.text = RemainingBullets.ToString();
                break;
            case "print":
                TotalBulletsText.text = TotalBullets.ToString();
                RemainingBulletsText.text = RemainingBullets.ToString();
                break;
        }
    }
    public void reload()
    {
        if (gun_name == "Rifle" && RemainingBullets < MagazineCapacity && TotalBullets != 0)
        {
          //  anim.Play("sarzor");
        }
    }

    public void gunsound()
    {
        //Sarjorsound.Play();
    }

    public void reloadgun()
    {
        if (RemainingBullets < MagazineCapacity && TotalBullets != 0)
        {
            if (RemainingBullets != 0)
            {
                bulletstate("bullet");
                bulletIndex = 0;

            }
            else
            {
                bulletstate("nobullet");
                bulletIndex = 0;
            }

        }

    }

    

    void bulletsave(string gungenre, int bulletcount)
    {

        switch (gungenre)
        {
            case "Rifle":
                TotalBullets += bulletcount;
                PlayerPrefs.SetInt(gungenre + "_Bullet", TotalBullets);
                bulletstate("print");
                break;
            case "Pistol":
                PlayerPrefs.SetInt("Pistol_Bullet", PlayerPrefs.GetInt("Pistol_Bullet") + bulletcount);
                break;
            case "Sniper":
                PlayerPrefs.SetInt("Sniper_Bullet", PlayerPrefs.GetInt("Sniper_Bullet") + bulletcount);
                break;
            default:
                break;
        }
    }

    public void gunchange()
    {
        pistol.SetActive(true);
       // animpistol.Play("gonepistol");
        transform.gameObject.SetActive(false);

    }
    public ParticleSystem bullethole2()
    {
       
            if (countbulletholes <= 24)
            {
                
                countbulletholes++;
                return bulletjolse[countbulletholes];

            }
            else
            {
                
                countbulletholes = 0;
              
             
                return bulletjolse[countbulletholes];
            }
           
        
       
    }


   

}
