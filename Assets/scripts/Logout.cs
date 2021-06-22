using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logout : MonoBehaviour
{
    Firebase.Auth.FirebaseAuth auth;

    // Start is called before the first frame update
    void Start()
    {
         auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    public void SignOut()
    {
    	auth.SignOut();
    	Debug.Log("Signout Success");
			SceneManager.LoadScene ("Login");
    }
}
