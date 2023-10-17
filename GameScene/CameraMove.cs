using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private static GameObject Cam;

    void Start()
    {
        if(Cam == null)
        {
            Cam = gameObject;
        }
        else if (Cam != gameObject)
        {
            Destroy(gameObject);
        }
    }

    public static void CamMove(Vector2 vec)
    {
        Cam.transform.position = new Vector3(0f, vec.y + 3.5f, -10f);
        return;
    }

    public static GameObject GetCam(){
        return Cam;
    }
}
