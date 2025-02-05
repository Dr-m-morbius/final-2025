using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
     public  float moveSpeed = 30f;
         public float lifeTime = 5f;

      private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
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
        Destroy(this.gameObject);
    }

}
