using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player_transform;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(GameMaster.gm_script.GetXRot(), 0, 0);
        transform.position = new Vector3(player_transform.position.x, player_transform.position.y + 40, player_transform.position.z + -10);
    }
}
