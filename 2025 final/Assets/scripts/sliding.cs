using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sliding : MonoBehaviour
{
    public Transform orentation;
    public Transform playerobj;
    private Rigidbody rb;
    private playermove playermove;

    public float maxtime;
    public float slideforce;
    private float slidetimer;
    private float slideyscale = 0.5F;
    private float startyscale;
    public KeyCode slidekay = KeyCode.E;
    private float horizontalinput;
    private float verticalinput;
    private bool slide;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playermove = GetComponent<playermove>();
        startyscale = playerobj.localScale.y;
    }
    private void FixedUpdate()
    {
        if(slide)
        {
            slidingmovement();
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontalinput = Input.GetAxisRaw("Horizontal");
        verticalinput = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(slidekay) && (horizontalinput != 0 || verticalinput != 0))
        {
            startslide();
        }
        if(Input.GetKeyUp(slidekay) && slide)
        {
            stopslide();
        }
    }
    private void startslide()
    {
        slide = true;
        playerobj.localScale = new Vector3(playerobj.localScale.x, slideyscale, playerobj.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slidetimer = maxtime;
    }
    private void slidingmovement()
    {
        Vector3 inputDirection = orentation.forward * verticalinput + orentation.right * horizontalinput;
        rb.AddForce(inputDirection.normalized * slideforce, ForceMode.Force);
        slidetimer -= Time.deltaTime;

        if(slidetimer <= 0)
        {
            stopslide();
        }
    }
    private void stopslide()
    {
        slide = false;
                playerobj.localScale = new Vector3(playerobj.localScale.x, startyscale, playerobj.localScale.z);

    }
}
