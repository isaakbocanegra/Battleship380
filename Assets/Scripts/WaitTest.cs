using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WaitTest : MonoBehaviour
{
    public GameObject panel;
    public Image cheems;
    public TMP_Text p1;
    public TMP_Text p2;
    public TMP_Text p3;



    //public WaitTest waiting;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        bool start = false;
        // this test prints "for starting"
        // waits 10 seconds
        // then prints "we ended"

     //   if (start == false)
          //  Destroy(panel);


        //----------------------------------------
        RectTransform panlpos = panel.GetComponent<RectTransform>();

        print("your panel position is " + panlpos.position);
        panlpos.position = new Vector3((float)1000.4, 436, 0);
        print("For starting");
        yield return StartCoroutine(Waitforseconds(5.0f));
        yield return StartCoroutine(makecheemsvisorinvis(false));// make it visable
        yield return StartCoroutine(makecheemsvisorinvis(true));

        ////////below makes the texts visable 
        yield return StartCoroutine(maketextmeshprovisorinvis(false, p1));
        yield return StartCoroutine(maketextmeshprovisorinvis(false, p2));
        yield return StartCoroutine(maketextmeshprovisorinvis(false, p3));
        ////////////above makes them visable
        ///
        yield return StartCoroutine(maketextmeshprovisorinvis(true, p1));
        yield return StartCoroutine(maketextmeshprovisorinvis(true, p2));
        yield return StartCoroutine(maketextmeshprovisorinvis(true, p3));

        yield return StartCoroutine(makepanelvis());
        Destroy(panel);
        print("We ended");

    }

    private IEnumerator maketextmeshprovisorinvis(bool invis, TMP_Text colvalue)
    {
        // TextMeshPro colvalue = p.GetComponent<TextMeshPro>();
        print("we are changeing values for textmesh " + colvalue.color.a);
        if (invis)
        {

            while (colvalue.color.a > 0)
            {
                print("we are changeing values for textmesh " + colvalue.color.a);

                //wait
                colvalue.color = new Color(colvalue.color.r, colvalue.color.g, colvalue.color.b, (float)(colvalue.color.a - .01));
                yield return StartCoroutine(Waitforseconds(.01f));
            }

        }
        else
        {
            while (colvalue.color.a < .9)
            {
                print("we are changeing values for textmesh " + colvalue.color.a);

                //wait
                colvalue.color = new Color(colvalue.color.r, colvalue.color.g, colvalue.color.b, (float)(colvalue.color.a + .01));
                yield return StartCoroutine(Waitforseconds(.01f));
            }
        }

    }

    public void skip()
    {
        Destroy(panel);
    }

    private IEnumerator makecheemsvisorinvis(bool invis)
    {

        if (invis)
        {

            while (cheems.color.a > 0)
            {
                print("we are changeing values " + cheems.color.a);

                //wait
                cheems.color = new Color(cheems.color.r, cheems.color.g, cheems.color.b, (float)(cheems.color.a - .01));
                yield return StartCoroutine(Waitforseconds(.01f));
            }

        }
        else
        {
            while (cheems.color.a < .9)
            {
                print("we are changeing values " + cheems.color.a);

                //wait
                cheems.color = new Color(cheems.color.r, cheems.color.g, cheems.color.b, (float)(cheems.color.a + .01));
                yield return StartCoroutine(Waitforseconds(.01f));
            }
        }

    }

    private IEnumerator makepanelvis()
    {
        print("visable function was called");

        Image imvalue = panel.GetComponent<Image>();
        while (imvalue.color.a > 0.1)
        {
            print("we are changeing values " + imvalue.color.a);

            //wait
            imvalue.color = new Color(0, 0, 0, (float)(imvalue.color.a - .001));
            yield return StartCoroutine(Waitforseconds(.01f));
        }
    }

    IEnumerator Waitforseconds(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }


}
