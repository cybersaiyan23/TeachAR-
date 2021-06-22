using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spider : MonoBehaviour
{   private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
       anim.SetBool("bird",x); 
       anim.SetBool("fly",y); 
    }
    public bool x=false;
    public bool y=false;

    public void die()
    {if(x==false)
        x=true;
    }

    public void idle()
    {if(x==true)
        x=false;
    }

    public void attack()
    {if(y==false)
        y=true;
    }

    public void idle2()
    {if(y==true)
        y=false;
    }
    
}
