using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    private int delivered;
    private int tips;
    private int failed;

    // Start is called before the first frame update
    void Start()
    {
        setToZero();
    }

    public void setToZero()
    {
        delivered = 0;
        tips = 0;
        failed = 0;
    }

    public void increaseDelivered()
    {
        delivered += 1;
    }

    public void increaseFailed()
    {
        failed += 1;
    }

    public void increaseTips(int tip)
    {
        tips += tip;
    }

    public int getDelivered()
    {
        return delivered;
    }

    public int getTips()
    {
        return tips;
    }
    
    public int getFailed()
    {
        return failed;
    }
}
