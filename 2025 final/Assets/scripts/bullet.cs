using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class bullet : MonoBehaviour
{
     public  float moveSpeed = 30f;
         public float lifeTime = 5f;
public ParticleSystem ps;
  public int LevelSelect;
private AudioSource source;
public AudioClip land;
public ParticleSystem splodeps;
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
            //source.PlayOneShot(explo);
            //Destroy(other.gameObject);
        }
         if (other.gameObject.CompareTag("endlvl"))
         {
            SceneManager.LoadScene(LevelSelect);
         }
          if (other.gameObject.CompareTag("bullet"))
        {
             splodeps.Play();
             //Destroy(this.gameObject);
        }
        
     }

}
