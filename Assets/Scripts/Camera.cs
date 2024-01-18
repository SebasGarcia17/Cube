using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform transforCamera;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = transforCamera.rotation;

        Quaternion newRotacion = Quaternion.Euler(transforCamera.eulerAngles.x, transforCamera.eulerAngles.y, 0f);

        transform.rotation = newRotacion;
    }
}
