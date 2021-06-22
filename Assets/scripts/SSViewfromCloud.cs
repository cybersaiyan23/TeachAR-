using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Firebase.Storage;

public class SSViewfromCloud : MonoBehaviour
{
    [SerializeField]
    GameObject Panel;
    int i, N;

    FirebaseStorage storage;
    StorageReference storageReference;

    Firebase.Auth.FirebaseAuth auth;

    // Start is called before the first frame update
    void Start()
    {
        storage = FirebaseStorage.DefaultInstance;
        storageReference = storage.GetReferenceFromUrl("gs://teachar-f92d7.appspot.com");

        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        
        StartCoroutine("DownloadDummy");
    }
    private IEnumerator DownloadDummy()
    {
        StorageReference dummyRef = storageReference.Child($"screenshots/{auth.CurrentUser.UserId}/0.png");

        var getmetaTask = dummyRef.GetMetadataAsync().ContinueWith( task => 
        {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                StorageMetadata meta = task.Result;
                int.TryParse(meta.GetCustomMetadata("Count"), out N);
                i = N;
            }
        });

        yield return new WaitUntil( () => getmetaTask.IsCompleted );
        Debug.Log("count: "+N);

        if (N > 0)
            StartCoroutine("DownloadandShow");
    }

    private IEnumerator DownloadandShow()
    {
        StorageReference imageRef = storageReference.Child($"screenshots/{auth.CurrentUser.UserId}/{i}.png");
        
        var downloadTask = imageRef.GetBytesAsync(long.MaxValue);
        yield return new WaitUntil( () => downloadTask.IsCompleted );

        Texture2D texture = new Texture2D (2, 2, TextureFormat.RGB24, false);
        texture.LoadImage(downloadTask.Result);

        Sprite sp = Sprite.Create(texture, new Rect (0, 0, texture.width, texture.height), new Vector2 (0.5f, 0.5f));
        Panel.GetComponent<Image> ().sprite = sp;
    }

    public void NextPicture()
    {
        if(N>0)
        {
            i += 1;
            if ( i> N )
                i = 1;
            StartCoroutine("DownloadandShow");
        }
    }
    public void PreviousPicture()
    {
        if(N>0)
        {
            i -= 1;
            if ( i < 1 )
                i = N;
            StartCoroutine("DownloadandShow");
        }
    }
}
