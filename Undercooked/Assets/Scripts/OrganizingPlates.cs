using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganizingPlates : MonoBehaviour
{
    public Holder tablePlatesHolder;
    public PlateTable plateTable;
    public float eatingTime = 10f;

    public GameObject dirtyPlate;

    //private float currentTime = 0f;
    private Holder myHolder;

    // Start is called before the first frame update
    void Start()
    {
        myHolder = GetComponent<Holder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myHolder.movableAnchor.transform.childCount > 0)
        {
            foreach (Transform child in myHolder.movableAnchor.transform)
            {
                Plate plate = child.gameObject.GetComponent<Plate>();
                if(plate.currentTime <= eatingTime)
                {
                    plate.modifyTime(Time.deltaTime);
                    plate.cleanCanvas();
                }
                else
                {
                    plate.currentTime = 0f;
                    plate.toDirty();
                    Plate dirt = Instantiate(dirtyPlate, tablePlatesHolder.movableAnchor.transform).GetComponent<Plate>();
                    dirt.toDirty();
                    if (!tablePlatesHolder.hasMovable())
                    {
                        tablePlatesHolder.setMovable(dirt.gameObject.GetComponent<MovableObject>());
                    }
                    else
                    {
                        tablePlatesHolder.queue.Enqueue(dirt.gameObject.GetComponent<MovableObject>());
                    }

                    if (myHolder.hasMovable())
                    {
                        myHolder.removeMovable();
                        Destroy(plate.gameObject);
                    }
                    else
                    {
                        Destroy(plate.gameObject);
                    }
                    
                }
            }
        }
    }
}
