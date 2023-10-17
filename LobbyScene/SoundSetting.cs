using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundSetting : MonoBehaviour
{
    void OnEnable(){
        float f;
        switch(transform.parent.GetSiblingIndex()){
            case 0: // BGM
                f = SceneController.MainManager.BGMVolume;
                GetComponent<Slider>().value = f;
                if(SceneController.MainManager.language == 1){ // Eng
                    transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>().text = "BackGround Music : " + ((int)(f * 100f)).ToString();
                }
                else if(SceneController.MainManager.language == 2){ // Kor
                    transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>().text = "배경음 크기 : " + ((int)(f * 100f)).ToString();
                }
                
                return;
            case 1: // SoundEffect
                f = SceneController.MainManager.soundEffectVolume;
                GetComponent<Slider>().value = f;
                if(SceneController.MainManager.language == 1){ // Eng
                    transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Sound Effect : " + ((int)(f * 100f)).ToString();
                }
                else if(SceneController.MainManager.language == 2){ // Kor
                    transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>().text = "효과음 크기 : " + ((int)(f * 100f)).ToString();
                }
                return;
        }
    }

    public void Setting(){
        float f = GetComponent<Slider>().value;
        switch(transform.parent.GetSiblingIndex()){
            case 0: // BGM
                SceneController.MainManager.SetBGMVolume(f);
                if(SceneController.MainManager.language == 1){ // Eng
                    transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>().text = "BackGround Music : " + ((int)(f * 100f)).ToString();
                }
                else if(SceneController.MainManager.language == 2){ // Kor
                    transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>().text = "배경음 크기 : " + ((int)(f * 100f)).ToString();
                }
                
                DataController.DC.GetData();
                DataController.DC.SaveData();

                return;
            case 1: // SoundEffect
                SceneController.MainManager.SetSoundEffectVolume(f);
                if(SceneController.MainManager.language == 1){ // Eng
                    transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Sound Effect : " + ((int)(f * 100f)).ToString();
                }
                else if(SceneController.MainManager.language == 2){ // Kor
                    transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>().text = "효과음 크기 : " + ((int)(f * 100f)).ToString();
                }
                
                DataController.DC.GetData();
                DataController.DC.SaveData();

                return;
        }
    }
}
