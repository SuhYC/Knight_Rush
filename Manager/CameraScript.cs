using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    void Update(){
        GetComponent<AudioSource>().volume = SceneController.MainManager.BGMVolume;
    }
}
