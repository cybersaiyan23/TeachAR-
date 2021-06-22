using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS : MonoBehaviour
{	public bool key= true;
public GameObject[] uielements;
    public void TakeAShot()
	{uielements= GameObject.FindGameObjectsWithTag("UIelements");
		StartCoroutine ("CaptureIt");
	}

	IEnumerator CaptureIt()
	{ToggleUI();
		yield return new WaitForSeconds(1);
		string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
		string fileName = "Screenshot" + timeStamp + ".png";
		string pathToSave = fileName;
		ScreenCapture.CaptureScreenshot(pathToSave);
		yield return new WaitForEndOfFrame();
		Debug.Log("SS taken");

		yield return new WaitForSeconds(1);
	ToggleUI();
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
}
