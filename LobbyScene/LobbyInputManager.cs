using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyInputManager : MonoBehaviour
{
    public GameObject SettingPanel;
    public GameObject PlayerSkinSettingPanel;
    public GameObject EnemySkinSettingPanel;
    public GameObject UpgradePanel;

    public GameObject GameStartPanel;

    public GameObject MoneyImg;
    public GameObject SettingButton;
    public GameObject UpgradeButton;
    public GameObject StartButton;
    public GameObject MainButton;
    public GameObject MoneyText;

    public GameObject SkinSettingButton;

    public GameObject ExitPanel;

    void Update()
    {
        checkInput();
    }

    private void checkInput(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            SkinSettingButton.GetComponent<SlideUpButtonSet>().SubButtonDown();

            if(SettingPanel.activeSelf){
                SettingPanel.SetActive(false);
                return;
            }

            if(GameStartPanel.activeSelf){
                GameStartPanel.SetActive(false);
                MoneyImg.GetComponent<ButtonImage>().SetImageBright();
                SettingButton.GetComponent<ButtonImage>().SetImageBright();
                UpgradeButton.GetComponent<ButtonImage>().SetImageBright();
                StartButton.GetComponent<ButtonImage>().SetImageBright();
                MainButton.GetComponent<ButtonImage>().SetImageBright();
                MoneyText.GetComponent<MoneyUI>().SetImageBright();

                return;
            }

            if(PlayerSkinSettingPanel.activeSelf){
                PlayerSkinSettingPanel.SetActive(false);
                return;
            }

            if(EnemySkinSettingPanel.activeSelf){
                EnemySkinSettingPanel.SetActive(false);
                return;
            }

            if(UpgradePanel.activeSelf){
                UpgradePanel.SetActive(false);
                return;
            }

            if(ExitPanel.activeSelf){
                ExitPanel.SetActive(false);
                return;
            }

            ExitPanel.SetActive(true);
            return;
        }
    }
}
