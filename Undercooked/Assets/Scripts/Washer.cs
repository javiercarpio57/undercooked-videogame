using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Washer : MonoBehaviour
{

    public MovableAnchor anchorWasher;
    public MovableAnchor anchorPlates;

    Queue<Plate> movablePlates;
    Queue<Plate> movableWasher;

    public bool washing = false;
    public bool allowToWash = false;

    private Plate plate;

    public MusicElements musicElements;
    
    void Start()
    {
        movablePlates = new Queue<Plate>();
        movableWasher = new Queue<Plate>();
        plate = null;
    }

    private void Update()
    {
        if (allowToWash && hasDirtyPlates())
        {
            allowToWash = false;
            plate = movableWasher.Peek();
        }

        if (washing && plate != null)
        {
            if (plate.washingTime < plate.washTime)
            {
                plate.increaseTime(Time.deltaTime);
            }
            else
            {
                getDirtyPlate();
                setCleanPlate(plate);
                plate.washingTime = 0f;
                plate = null;
                
                updateDirtyPosition();
                updateCleanPosition();
                if (hasDirtyPlates())
                {
                    allowToWash = true;
                    washing = true;
                }
                else
                {
                    allowToWash = false;
                    washing = false;
                }
            }
        }
    }

    public bool hasDirtyPlates()
    {
        return movableWasher.Count > 0;
    }

    public bool hasCleanPlates()
    {
        return movablePlates.Count > 0;
    }

    public Plate getDirtyPlate()
    {
        return movableWasher.Dequeue();
    }

    public Plate getCleanPlate()
    {
        return movablePlates.Dequeue();
    }

    public void setDirtyPlate(Plate move)
    {
        move.gameObject.transform.SetParent(anchorWasher.transform);
        move.transform.localPosition = new Vector3(0, (float)(movableWasher.Count * 0.2), 0);
        movableWasher.Enqueue(move);
    }

    private void setCleanPlate(Plate move)
    {
        musicElements.plate.Play();
        move.returnToClean();
        move.gameObject.transform.SetParent(anchorPlates.transform);
        move.transform.localPosition = new Vector3(0, (float)(movablePlates.Count * 0.2), 0);
        movablePlates.Enqueue(move);
    }

    public void updateDirtyPosition()
    {
        float pos = 0;
        foreach(Transform child in anchorWasher.transform)
        {
            child.localPosition = new Vector3(0, (float)(pos * 0.2), 0);
            pos += 1;
        }
    }

    public void updateCleanPosition()
    {
        float pos = 0;
        foreach (Transform child in anchorPlates.transform)
        {
            child.localPosition = new Vector3(0, (float)(pos * 0.2), 0);
            pos += 1;
        }
    }
}
