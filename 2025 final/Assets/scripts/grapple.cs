using System.Collections;
using System.Collections.Generic;
using UnityEditor.Sprites;
using UnityEngine;

public class grapple : MonoBehaviour
{
    private playermove pm;
    public Transform cam;
    public Transform player;
    public Transform grappletip;
    public LayerMask WhatisGrappleable;
    public LineRenderer lr;
    private SpringJoint joint;

    public float maxdistance;
    public float delay;
    private Vector3 grapplepoint;
    private bool grappling;

    public float cooldown;
    private float timer;
    public KeyCode grapplekey = KeyCode.Mouse1;
    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<playermove>();

    }

    // Update is called once per frame
    void Update()
    {
      
        if (Input.GetKeyDown(grapplekey))
        {
            Startgrapple();
        } 
        if (Input.GetKeyUp(grapplekey))
        {
            stopgrapple();
        } 
    }
    private void LateUpdate()
    {
       Drawrope();
    }
    private void Startgrapple()
    {

        grappling = true;

        RaycastHit hit;
        if(Physics.Raycast(cam.position, cam.forward, out hit, maxdistance, WhatisGrappleable))
        {
            grapplepoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplepoint;

            float distancefrompoint = Vector3.Distance(player.position, grapplepoint);

            joint.maxDistance = distancefrompoint * 0.8f;
            joint.minDistance = distancefrompoint * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;
          lr.positionCount = 2;
          //grappleposition = grappletip.position;
            
        }
     
    }
void Drawrope()
{
 if(!joint) return;

 lr.SetPosition(0, grappletip.position);
 lr.SetPosition (1, grapplepoint);
}
    

   
    private void stopgrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }
}
