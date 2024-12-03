using JetBrains.Annotations;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public GameObject cam_obj;
    public float rot_speed;
    Transform cam_obj_t;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam_obj_t = cam_obj.transform;
    }

    // Update is called once per frame
    void Update()
    {
        cam_obj_t.transform.eulerAngles += cam_obj_t.up * Time.deltaTime * rot_speed;
    }
}
