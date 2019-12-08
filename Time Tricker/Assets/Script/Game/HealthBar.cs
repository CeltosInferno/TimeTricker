using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    //the bar which contain the sprite bar
    private Transform bar;
    //a coefficient mostly used to swap the bar when the enntity is swapped
    //in order to keep the same direction
    //public float sizeMultiplier = 1f;

    void Start()
    {
        //get the bar
        bar = transform.Find("Bar");
        //Debug.Log(bar);
    }

    /**
     * Set the new size of the bar
     * <param name="sizeNormalized">New size beetween 0 and 1</param>
     * <returns>Void</returns>
     **/
    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }
}
