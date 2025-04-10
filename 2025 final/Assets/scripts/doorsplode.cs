using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorsplode : MonoBehaviour
{
    // Start is called before the first frame update
      public ParticleSystem ps;
      public float delay =1f;
      public AudioClip splode;
      private AudioSource source;
      public bool hit = false;
    void Start()
    {
       source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hit)
       {
        delay -= Time.deltaTime;
        if(delay <= 0)
        {
            Destroy(this.gameObject);
        }
       } 
    }
     void  OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            ps.Play();
            source.PlayOneShot(splode);
            hit = true;
            //Destroy(this.gameObject);
        }
}}
