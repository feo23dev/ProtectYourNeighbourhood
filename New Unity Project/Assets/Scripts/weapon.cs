using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weapon : MonoBehaviour
{
    [Header("SETTINGS")]
    public bool canFire;
    public float fireCooldown;
    float fireRate;
    public float fireRange;




    [Header("SOUNDS")]
    public AudioSource fireSound;
    public AudioSource magazineSound;
    public AudioSource noAmmoSound;

    [Header("EFFECTS")]
    public ParticleSystem fireEffect;
    public ParticleSystem bulletTrail;
    public ParticleSystem bloodEffect;

    [Header("OTHERS")]
    Animator myAnim;
    public Camera cam;

    [Header("WEAPON SETTINGS")]
    public int totalAmmo;
    public int magazineCapacity;
    public int remainingAmmo;
    public Text totalAmmo_Text;
    public Text remainingAmmo_Text;
    int howManyBullets;









    // Start is called before the first frame update
    void Start()
    {
        remainingAmmo = magazineCapacity;
        myAnim = GetComponent<Animator>();
        UpdateBulletUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (canFire && Time.time > fireRate && remainingAmmo != 0)
            {
                Fire();
                fireRate = Time.time + fireCooldown;
                remainingAmmo--;
                remainingAmmo_Text.text = remainingAmmo.ToString();
            }
            if (remainingAmmo == 0)
            {
                noAmmoSound.Play();
            }

        }


        if (Input.GetKey(KeyCode.R) )
        {
            if (remainingAmmo < magazineCapacity && totalAmmo !=0)
            {
                if(remainingAmmo!=0)
                {
                    ReloadBulletsMath("NotEmpty");
                    
                }
                else
                {
                    ReloadBulletsMath("Empty");
                }
            }
        }

    }

    void Fire()
    
    {
        fireSound.Play();
        fireEffect.Play();
        myAnim.Play("ak47-fire");

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, fireRange))
        {
            if (hit.transform.gameObject.CompareTag("Dusman"))
            {
                Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else if (hit.transform.gameObject.CompareTag("Moveable"))
            {
                Rigidbody rb = hit.transform.gameObject.GetComponent<Rigidbody>();
                rb.AddForce(-hit.normal, ForceMode.Impulse);
            }

            else
            {
                Instantiate(bulletTrail, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }

    }
    void PlayMagazineSound()
    {
        magazineSound.Play();
    }

    public void UpdateBulletUI()
    {
        remainingAmmo_Text.text = remainingAmmo.ToString();
        totalAmmo_Text.text = totalAmmo.ToString();
    }
    public void ReloadBulletsMath(string type)
    {
        switch(type)
        {
            case "NotEmpty":
                if(magazineCapacity >= totalAmmo)
                {
                    int total =remainingAmmo + totalAmmo;
                    if(total > magazineCapacity)
                    {
                        remainingAmmo = magazineCapacity;
                        totalAmmo =total - magazineCapacity;
                    }
                    else
                    {
                        remainingAmmo += totalAmmo;
                        totalAmmo=0;
                    }
                    
                    
                }
                else
                {
                    howManyBullets = magazineCapacity - remainingAmmo;
                    totalAmmo -= howManyBullets;
                    remainingAmmo = magazineCapacity;
                    myAnim.Play("ak47magazin");
                    

                }
                UpdateBulletUI();
                
                

            break;


            case "Empty":
                if(magazineCapacity >= totalAmmo)
                {
                    remainingAmmo=totalAmmo;
                    totalAmmo=0;
                    
                }
                else
                {
                    totalAmmo -= magazineCapacity;
                    remainingAmmo = magazineCapacity;
                    

                }
                UpdateBulletUI();
                
                

            break;
        }

    }












}
