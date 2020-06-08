using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBallLaserScript : MonoBehaviour
{
    private LineRenderer lineRenderer; //Line Renderer Component
    public Transform laserHitPoint; //Position of the end of the laser
    public LayerMask Mask;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.useWorldSpace = true; //Line renderer uses worldspace rather than local
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit; //Detects if raycast hit something
        Ray ray = new Ray(transform.position, transform.forward); //Ray to be shot out
        if (Physics.Raycast(ray.origin, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, Mask )) { //Detect what the ray hits
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log(hit.collider.tag);
            laserHitPoint.position = hit.point; //Set endpoint to the positiong that the Raycast hit
            laserHitPoint.position = new Vector3(laserHitPoint.position.x, 1, laserHitPoint.position.z);
            lineRenderer.SetPosition(0, transform.position); //Set initial point of linerenderer

            lineRenderer.SetPosition(1, laserHitPoint.position); //Set final point of lineRenderer
            lineRenderer.enabled = true; //Turn on the linerenderer
        }
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.tag == "Player" && !Player.GetComponent<PlayerController>().getDashing()) {
                //hit.collider.gameObject.SetActive(false);
                Player.GetComponent<PlayerController>().decHealthAndInvokeIFrames();
            }
        }
    }

}
