using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateTable : MonoBehaviour
{
    public int countPlates = 0;

    public void sumarPlato()
    {
        countPlates += 1;
    }

    public void restarPlato()
    {
        countPlates -= 1;
    }
}
