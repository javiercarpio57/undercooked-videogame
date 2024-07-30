﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timing : MonoBehaviour
{
    private Holder holder;

    public Canvas canvas;
    public Image foregroundImage;
    
    private float time = 2f;
    private float currentTime;

    private void Start()
    {
        holder = GetComponent<Holder>();
        //canvas.enabled = false;
    }

    public void activateCanvas()
    {
        setInitialTime();

        canvas.enabled = true;
    }

    public void desactivateCanvas()
    {
        canvas.enabled = false;
    }

    private void setInitialTime()
    {
        time = getFoodTime();
        Food food = holder.movableAnchor.GetComponentInChildren<Food>();

        currentTime = food.currentState.currentTime;
        foregroundImage.fillAmount = currentTime / time;
    }

    private float getFoodTime()
    {
        Food food = holder.movableAnchor.GetComponentInChildren<Food>();
        if(food != null)
        {
            return food.currentState.processingTime;
        }
        return 0;
    }

    private void OnEnable()
    {
        currentTime = 0;
    }

    public void modifyTime(float changeTime)
    {
        currentTime += changeTime;

        float currentTimePct = (float)currentTime / (float)time;
        foregroundImage.fillAmount = currentTimePct;
    }

}
