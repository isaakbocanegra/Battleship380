using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitTest : MonoBehaviour
{
    //public WaitTest waiting;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        // this test prints "for starting"
        // waits 10 seconds
        // then prints "we ended"
        //----------------------------------------
        print("For starting");
        yield return StartCoroutine(DisableScript(10.0f));

        print("We ended");
        
    }

    IEnumerator DisableScript(float waitTime){
        yield return new WaitForSeconds(5f);
    }

    
}
