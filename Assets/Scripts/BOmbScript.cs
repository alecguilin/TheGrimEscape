using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOmbScript : MonoBehaviour
{

    public Renderer mat;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BOmbprint()
    {
        mat.material.color = Random.ColorHSV(0f, 1f, 0f,1f, 0f, 1f);
        transform.Translate(Vector3.forward * 25* Time.deltaTime);
        Debug.Log("EVENT IS TRIGGERED");
    }
}
