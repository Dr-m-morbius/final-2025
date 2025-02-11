using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform theCamera;
    private ammo _ammo;
     private AudioSource source;
     public AudioClip shoot;
     
public Transform firepoint;
    // Start is called before the first frame update
    void Start()
    {
        _ammo = GetComponent<ammo>();
         source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && _ammo.GetAmmoAmount() > 0)
{
    //find crosshair
    RaycastHit hit;
    if(Physics.Raycast(theCamera.position, theCamera.forward, out hit, 50f))
    {
        if(Vector3.Distance(theCamera.position,hit.point) > 2f)
        {
            firepoint.LookAt(hit.point);
        }
    }
    else
    {
        firepoint.LookAt(theCamera.position + (theCamera.forward * 30f));
    }

    //bullet born
    Instantiate(bullet, firepoint.position, firepoint.rotation);
    source.PlayOneShot(shoot);
    //kill ammo
    _ammo.RemoveAmmo();
}
    }
}
