using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class userinterface : MonoBehaviour
{ public GameObject[] uielements;
  public bool key= true;
    void Start()
    {uielements= GameObject.FindGameObjectsWithTag("UIelements");
        
    }
  public void playGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Settings()
    {
        SceneManager.LoadScene("settings");
    }

   

    public GameObject PanelText;
    public bool panel=false;
    
    public void openpanel()
    {
        if(panel==false)
        {PanelText.SetActive(true);
        panel=true;}
        else
        {PanelText.SetActive(false);
        panel=false;}
    
    }

public GameObject camerapanel;
    public bool panels=false;
    
    public void cameraPanel()
    {
        if(panels==false)
        {camerapanel.SetActive(true);
        panels=true;}
        else
        {camerapanel.SetActive(false);
        panels=false;}
    
    }


public GameObject gallerypanel;
    public bool gallerpanels=false;
    
    public void galleryPanel()
    {
        if(gallerpanels==false)
        {gallerypanel.SetActive(true);
        gallerpanels=true;}
        else
        {gallerypanel.SetActive(false);
        gallerpanels=false;}
    
    }

public GameObject Settingspanel;
    public bool sett=false;
    public void openSettings()
    {
        if(sett==false)
        {Settingspanel.SetActive(true);
        sett=true;
        ToggleUI();}
        else
        {Settingspanel.SetActive(false);
        sett=false;
        ToggleUI();}
    
    }





    public void ToggleUI()
    {   
        
        {if(key==true)
           {
            foreach (GameObject g in uielements)
        {
            g.SetActive(false);
        }
            key =false;
           }
        else
        {
         foreach (GameObject g in uielements)
        {
            g.SetActive(true);
        }
            key =true;   
        }
        
        }
    }

    public void Exitbutton(){
       Application.Quit();
       Debug.Log("Game closed");
   }
}