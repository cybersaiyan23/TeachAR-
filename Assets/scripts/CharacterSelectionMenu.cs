using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CharacterSelectionMenu : MonoBehaviour
{
    
    public GameObject[] modelObjects;
    public string[] textboxes;
    public int selectedmodel = 0;
    public Text textfield;

    public string gameScene = "model Selection Scene";

    public int[] i;

    private string selectedmodelDataName = "Selectedmodel";
    private bool key = true;
    void Start()
    {

        HideAllCharacters();

        selectedmodel = PlayerPrefs.GetInt(selectedmodelDataName, 0);

        modelObjects[selectedmodel].SetActive(true);
        textfield.text =textboxes[selectedmodel];

    }



    public void HideAllCharacters()
    {
        foreach (GameObject g in modelObjects)
        {
            g.SetActive(false);
        }
    }


     public void Togglemodels()
    {   
        
        {if(key==true)
           {
            modelObjects[selectedmodel].SetActive(false);
            key =false;
           }
        else
        {
         modelObjects[selectedmodel].SetActive(true);
            key =true;   
        }
        
        }
    }


    public void NextCharacter()
    {
        modelObjects[selectedmodel].SetActive(false);
        selectedmodel++;
        
        if (selectedmodel >= modelObjects.Length)
        {
            selectedmodel = 0;
        }
        modelObjects[selectedmodel].SetActive(true);
        textfield.text =textboxes[selectedmodel];
    }

    public void PreviousCharacter()
    {
        modelObjects[selectedmodel].SetActive(false);
        selectedmodel--;
        if (selectedmodel < 0)
        {
            selectedmodel = modelObjects.Length-1;
        }
        modelObjects[selectedmodel].SetActive(true);
        textfield.text =textboxes[selectedmodel];
    }


    public void zoomIn()
    {
        if (i[selectedmodel]<=10)
    {
       modelObjects[selectedmodel].transform.localScale += new Vector3(1,1,1);
       i[selectedmodel]++;
    }
    }

    public void zoomOut()
    {if (i[selectedmodel]>0)
    {
       modelObjects[selectedmodel].transform.localScale -= new Vector3(1,1,1);
       i[selectedmodel]--;
    }
    }
  
}
