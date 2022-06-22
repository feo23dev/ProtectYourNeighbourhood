using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public bool canFire;
    public float fireCooldown;
    float fireRate;
    public float fireRange;
    public Camera cam;
    public AudioSource fireSound;
    public AudioSource magazineSound;
    public ParticleSystem fireEffect;
    public ParticleSystem bulletTrail;
    public ParticleSystem bloodEffect;
    
    Animator myAnim;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0) && canFire && Time.time > fireRate)
        {
            Fire();
            fireRate = Time.time + fireCooldown;

        }

        if(Input.GetKey(KeyCode.R))
        {
            /*if(!magazineSound.isPlaying)
            {
                
                magazineSound.Play();

            } */
            myAnim.Play("ak47magazin");
            
           
        }

    }

    void PlayMagazineSound()
    {
        magazineSound.Play();
    }
    











    void Fire()
    {
        fireSound.Play();
        fireEffect.Play();
        myAnim.Play("ak47-fire");

        RaycastHit hit;

        if(Physics.Raycast(cam.transform.position,cam.transform.forward,out hit,fireRange))
        {
            if(hit.transform.gameObject.CompareTag("Dusman"))
            {
                Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else if(hit.transform.gameObject.CompareTag("Moveable"))
            {
                Rigidbody rb = hit.transform.gameObject.GetComponent<Rigidbody>();
                rb.AddForce(-hit.normal , ForceMode.Impulse);


            }
            
            else
            {
                Instantiate(bulletTrail, hit.point, Quaternion.LookRotation(hit.normal));
            }
           
            
        }

       
        

    }












}
