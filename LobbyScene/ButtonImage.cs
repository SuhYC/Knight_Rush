using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImage : MonoBehaviour
{
    public void SetImageDark(){
        Image img = GetComponent<Image>();

        img.color = new Color(200f/255f,200f/255f,200f/255f);

        return;
    }

    public void SetImageBright(){
        Image img = GetComponent<Image>();

        img.color = new Color(1f,1f,1f);

        return;
    }
}
