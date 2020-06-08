using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserScript : MonoBehaviour
{

    public Transform startPoint;
    public Transform endPoint;
    public GameObject RoomMaster;
    public float onTime;
    public float offTime;
    LineRenderer laserLine;

    CapsuleCollider capsule;


    // Start is called before the first frame update
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();

        laserLine.startWidth = 1f;
        laserLine.endWidth = 1f;

        /*
        capsule = gameObject.AddComponent();

        capsule.radius = .25f / 2;
        capsule.center = Vector3.zero;
        capsule.direction = 2;
        fdsfsdfdsf
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (!RoomMaster.GetComponent<RoomMaster>().roomCompleted) {
            laserLine.SetPosition(0, startPoint.position);
            laserLine.SetPosition(1, endPoint.position);

            /*
            capsule.transform.position = startPoint.position + (endPoint.position - startPoint.position) / 2;
            capsule.transform.LookAt(startPoint.position);
            capsule.height = (endPoint.position - startPoint.position).magnitude;
            */

            StartCoroutine(waiter());
        }
        else {
            gameObject.SetActive(false);
            StopCoroutine(waiter());
        }



    }

    IEnumerator waiter()
    {
        while (laserLine.enabled == true)
        {
            yield return new WaitForSeconds(1.0f);
            laserLine.enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            yield return new WaitForSeconds(1.0f);
            laserLine.enabled = true;
            GetComponent<BoxCollider>().enabled = true;
        }
    }
}
