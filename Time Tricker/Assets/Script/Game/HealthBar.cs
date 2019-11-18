using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    //the bar which contain the sprite bar
    private Transform bar;

    void Start()
    {
        //get the bar
        bar = transform.Find("Bar");
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
