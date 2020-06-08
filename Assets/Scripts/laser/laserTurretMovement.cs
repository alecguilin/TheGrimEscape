using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserTurretMovement : MonoBehaviour
{

    public float speed = 5f;

    public enum faceDirection
    {
        forward,
        left,
        backward,
        right
    }

    public Transform face;
    faceDirection direction;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (face.transform.rotation.y > 315f || face.transform.rotation.y <= 45f)
        {
            direction = faceDirection.forward;
        }
        else if (face.transform.rotation.y > 45f || face.transform.rotation.y <= 135f)
        {
            direction = faceDirection.right;
        }
        else if (face.transform.rotation.y > 135f || face.transform.rotation.y <= 225f)
        {
            direction = faceDirection.backward;
        }
        else
        {
            direction = faceDirection.left;
        }

        StartCoroutine(waiter(direction));

    }


    IEnumerator waiter(faceDirection d)
    {
        if (d == faceDirection.forward || d == faceDirection.backward)
        {
            face.transform.Translate(speed * Time.deltaTime, 0, 0);
            yield return new WaitForSeconds(1.0f);
            face.transform.Translate(-(speed * Time.deltaTime), 0, 0);
            yield return new WaitForSeconds(1.0f);
        }
        else
        {
            face.transform.Translate(0, speed * Time.deltaTime, 0);
            yield return new WaitForSeconds(1.0f);
            face.transform.Translate(0, -(speed * Time.deltaTime), 0);
            yield return new WaitForSeconds(1.0f);
        }
    }
}

