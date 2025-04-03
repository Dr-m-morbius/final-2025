using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
     public  float moveSpeed = 30f;
         public float lifeTime = 5f;
public ParticleSystem ps;
private AudioSource source;
public AudioClip land;
public AudioClip explo;
      private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
      
    }

    void Awake()
    {
          ps = GetComponent<ParticleSystem>();
         ps.Play();
         source = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
          lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
              _rigidbody.velocity = transform.forward * moveSpeed;
  
    }
    void OnCollisionEnter(Collision other)
    {
       source.PlayOneShot(land);
        Destroy(this.gameObject);
        
    }
     void OnTriggerEnter(Collider other)
     {
        if (other.gameObject.CompareTag("door"))
        {
            source.PlayOneShot(explo);
            Destroy(other.gameObject);
        }
        
     }

}
