using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camrea : MonoBehaviour
{

    public float sensX;
    public float sensY;
    public Transform orentation;
    float Xrotation;
    float Yrotation;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        Yrotation += mouseX;
        Xrotation -=mouseY;
        Xrotation = Mathf.Clamp(Xrotation, -90f, 90f);

        //rotate
        transform.rotation = Quaternion.Euler(Xrotation, Yrotation,0);
        orentation.rotation = Quaternion.Euler(0, Yrotation,0);
    }
}
