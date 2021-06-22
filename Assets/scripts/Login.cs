using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
	Firebase.Auth.FirebaseAuth auth;
    public InputField email;
    public InputField password;
    public GameObject resultText;
    public GameObject signInFail;
    


    // Start is called before the first frame update
    void Start()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }
    // Update is called once per frame
    void Update()
    {
        if(auth.CurrentUser!=null)
                resultText.SetActive(true);
    }

    public void SignUp()
    {
        auth.CreateUserWithEmailAndPasswordAsync(email.text.ToString(), password.text.ToString()).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.Log(string.Format("CreateUserWithEmailAndPasswordAsync was canceled."));
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log(string.Format("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception));
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.Log(string.Format("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId));
        });
    }

    public void SignIn()
    {
        auth.SignInWithEmailAndPasswordAsync(email.text.ToString(), password.text.ToString()).ContinueWith(task =>
        	{
	            if (task.IsCanceled)
	            {
                    signInFail.SetActive(true);
	                Debug.Log(string.Format("SignInWithEmailAndPasswordAsync was canceled."));
	                return;
	            }
	            if (task.IsFaulted)
	            {
                    signInFail.SetActive(true);
	                Debug.Log(string.Format("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception));
	                return;
	            }
	            Firebase.Auth.FirebaseUser newUser = task.Result;
	            Debug.Log(string.Format("User signed in successfully: {0} ({1})",
	                newUser.DisplayName, newUser.UserId));
        	}
        );

        if(auth.CurrentUser != null)
		{
			SceneManager.LoadScene ("teachar");
		}
    }

    public void SignOut()
    {
    	auth.SignOut();
    	Debug.Log("Signout Success");
    }
}
