using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipLogin : MonoBehaviour
{

    Firebase.Auth.FirebaseAuth auth;


    // Start is called before the first frame update
    void Start()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    public void Skip()
    {
        if(auth.CurrentUser!=null)
            SceneManager.LoadScene ("teachar");
        else
            SceneManager.LoadScene("login");
    }
}
