using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Firebase.Storage;

public class SS_Share : MonoBehaviour
{	public GameObject[] uielements;
  	public bool key= true;
	int count=0;

	FirebaseStorage storage;
    StorageReference storageReference;
    StorageReference dummyRef;

	Firebase.Auth.FirebaseAuth auth;

    void Start()
    {
		uielements= GameObject.FindGameObjectsWithTag("UIelements");
		storage = FirebaseStorage.DefaultInstance;
        storageReference = storage.GetReferenceFromUrl("gs://teachar-f92d7.appspot.com");

        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
		StartCoroutine("DownloadDummy");
        
    }
	private IEnumerator DownloadDummy()
    {
    	dummyRef = storageReference.Child($"screenshots/{auth.CurrentUser.UserId}/0.png");

        var getmetaTask = dummyRef.GetMetadataAsync().ContinueWith( task => 
        {
        	if (!task.IsFaulted && !task.IsCanceled)
        	{
		        StorageMetadata meta = task.Result;
		        int.TryParse(meta.GetCustomMetadata("Count"), out count);
    		}
        });

        yield return new WaitUntil( () => getmetaTask.IsCompleted );
        Debug.Log("count: "+count);
    }

    public void TakeAShot()
	{	
		StartCoroutine ("TakeScreenshotAndShare");
	}

	private IEnumerator TakeScreenshotAndShare()
	{
		ToggleUI();
		yield return new WaitForSeconds(1);
		yield return new WaitForEndOfFrame();

		Texture2D ss = new Texture2D( Screen.width, Screen.height, TextureFormat.RGB24, false );
		ss.ReadPixels( new Rect( 0, 0, Screen.width, Screen.height ), 0, 0 );
		ss.Apply();

		count += 1;

		StorageReference uploadRef = storageReference.Child($"screenshots/{auth.CurrentUser.UserId}/{count}.png");

		var bytes = ss.EncodeToPNG();
        var uploadTask = uploadRef.PutBytesAsync(bytes);
        yield return new WaitUntil( () => uploadTask.IsCompleted );

        if (uploadTask.Exception != null)
        {
            Debug.Log($"Failed to upload: {uploadTask.Exception}");
            yield break;
        }

        var getUrlTask = uploadRef.GetDownloadUrlAsync();
        yield return new WaitUntil( () => getUrlTask.IsCompleted );

        if(getUrlTask.Exception != null)
        {
            Debug.Log($"Faied to get url: {uploadTask.Exception}");
        }

        Debug.Log($"Download from: {getUrlTask.Result}");

		var metadataChange = new MetadataChange()
        {
            CustomMetadata = new Dictionary<string, string>()
            {
                {"Count", count.ToString() }
            }
        };

        Texture2D dummy = new Texture2D(256,256);
        dummy = Texture2D.blackTexture;
        var bytes_ = dummy.EncodeToPNG();
        StorageReference uploadRef_ = storageReference.Child($"screenshots/{auth.CurrentUser.UserId}/0.png");

        var changemetaTask = uploadRef_.PutBytesAsync(bytes_, metadataChange);
        yield return new WaitUntil( () => uploadTask.IsCompleted );

        if (uploadTask.Exception != null)
        {
            Debug.Log($"Failed to upload: {uploadTask.Exception}");
            yield break;
        }

        var getUrlTask_ = uploadRef_.GetDownloadUrlAsync();
        yield return new WaitUntil( () => getUrlTask_.IsCompleted );

        if(getUrlTask.Exception != null)
        {
            Debug.Log($"Faied to get url: {uploadTask.Exception}");
        }

        Debug.Log($"Download from: {getUrlTask.Result}");

		string filePath = Path.Combine( Application.temporaryCachePath, "shared img.png" );
		File.WriteAllBytes( filePath, ss.EncodeToPNG() );

		// To avoid memory leaks
		Destroy( ss );

		new NativeShare().AddFile( filePath )
			.SetSubject( "Model" ).SetText( "Hey check out my model!" ).SetUrl( "https://github.com/jithindk/TeachAR" )
			.SetCallback( ( result, shareTarget ) => Debug.Log( "Share result: " + result + ", selected app: " + shareTarget ) )
			.Share();

	yield return new WaitForSeconds(1);
	ToggleUI();
		// Share on WhatsApp only, if installed (Android only)
		//if( NativeShare.TargetExists( "com.whatsapp" ) )
		//	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
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


