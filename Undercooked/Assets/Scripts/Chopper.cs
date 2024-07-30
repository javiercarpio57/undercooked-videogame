using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chopper : MonoBehaviour
{
    [HideInInspector]
    public bool isChopping = false;

    public Knife chopperKnife;

    private Food currentFood;
    private Holder chopperHolder;

    private Timing timing;

    public MusicElements musicElements;

    // Start is called before the first frame update
    void Start()
    {
        chopperHolder = GetComponent<Holder>();
        timing = GetComponentInChildren<Timing>();
        timing.desactivateCanvas();
    }

    public bool startChopping()
    {
        musicElements.chop.Play();
        chopperKnife.gameObject.SetActive(false);
        if (chopperHolder.hasMovable())
        {
            MovableObject movable = chopperHolder.GetMovableObjet();
            currentFood = movable.GetComponent<Food>();

            if(currentFood != null && currentFood.getStatus() == FoodStatus.RAW)
            {
                isChopping = true;
            }
        }

        return isChopping;
    }

    public void stopChopping()
    {
        musicElements.chop.Stop();
        chopperKnife.gameObject.SetActive(true);
        isChopping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isChopping)
        {
            
            currentFood.processFood(Time.deltaTime);
            timing.modifyTime(Time.deltaTime);

            if(currentFood.getStatus() != FoodStatus.RAW)
            {
                stopChopping();
                
            }
        }
    }
}
