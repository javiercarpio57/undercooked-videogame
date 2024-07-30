using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    
    private Holder stoveHolder;

    public MusicElements musicElements;

    void Start()
    {
        stoveHolder = GetComponent<Holder>();
    }

    // Update is called once per frame
    void Update()
    {
        

        Container container = stoveHolder.GetComponentInChildren<Container>();
        if(container != null)
        {
            if (!musicElements.beep.isPlaying)
            {
                container.smoke.Stop();
            }

            Cooking cooking = container.GetComponent<Cooking>();
            if (container.getListCount() > 0) {

                if(container.getCurrentTime() <= container.getTotalTime())
                {
                    musicElements.beep.Stop();
                    //container.smoke.Stop();
                    cooking.modifyTime(Time.deltaTime);
                    cooking.imageStatus.sprite = cooking.statusCanvas.none;
                    container.isReady = false;
                }
                else if(container.getCurrentTime() > container.getTotalTime() && container.currentBurningTime <= container.totalBurningTime)
                {
                    cooking.imageStatus.sprite = cooking.statusCanvas.ready;
                    foreach (Food f in container.getCurrentFood())
                    {
                        if (f.getStatus() == FoodStatus.INSPOT)
                        {
                            f.changeState(f.currentState.nextState);
                            container.addCookedFood(f);
                        }
                    }
                    cooking.increasingBurningTime(Time.deltaTime);

                    if(container.currentBurningTime/container.totalBurningTime >= 0.5)
                    {
                        cooking.imageStatus.sprite = cooking.statusCanvas.burning;

                        if (!musicElements.beep.isPlaying)
                        {
                            musicElements.beep.Play();
                            container.smoke.Play();
                        }
                    }
                    else
                    {
                        musicElements.beep.Stop();
                        //container.smoke.Stop();
                    }

                    if(container.getListCount() == 3)
                    {
                        container.isReady = true;
                    }
                }
                else
                {
                    //container.smoke.Stop();
                    musicElements.beep.Stop();
                    cooking.imageStatus.sprite = cooking.statusCanvas.burned;
                    container.potState = potState.BURNED;
                    container.changePotState();
                    container.isReady = false;
                }
            }
            else
            {
                //container.smoke.Stop();
                //musicElements.beep.Stop();
            }
        }
        else
        {
            musicElements.beep.Stop();
        }
    }


}
