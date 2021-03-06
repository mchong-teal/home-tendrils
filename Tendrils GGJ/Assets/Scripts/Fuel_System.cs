﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Fuel_System : MonoBehaviour {

    // public
    public float delay;
    public float maxDelay;
    public float fuel;
    public float maxFuel;
    public float jetForce;
    public Slider fuelslider;
    public bool JetOn;

    // private
    float fuelTick;
    float refillSpeed;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start() {

        if (!rb) {
            rb = GetComponent<Rigidbody2D>();
        }

        if (maxDelay <= 0) {
            maxDelay = 2.0f;
            Debug.LogWarning("Max Delay not set properly in " + name + ". Defaulting to: " + maxDelay);
        }

        if (maxFuel <=0 || maxFuel>= 500) {
            maxFuel = 100.0f;
            Debug.LogWarning("Max Fuel not set properly in " + name + ". Defaulting to: " + maxFuel);
        }

        if (jetForce <= 0 || jetForce >= 15) {
            jetForce = 10.0f;
            Debug.LogWarning("Jet Force not set properly in " + name + ". Defaulting to: " + jetForce);
        }

        // init
        
        JetOn = false;
        fuelTick = maxFuel / 100.0f;
        refillSpeed = maxFuel / 100.0f;
        delay = maxDelay;
        fuel = maxFuel;
        fuelslider.maxValue = maxFuel;
        fuelslider.value = fuel;
    }

    // Update is called once per frame
    void Update() {

        fuelslider.value = fuel;
        if (fuel > maxFuel) { fuel = maxFuel; }
        else if (fuel < 0.0f) { fuel = 0.0f; }
    }

    public void UseJetForce() {
        delay = maxDelay;
        fuel -= fuelTick;
    }

    public void UseJetForce(bool jf) {
        delay = maxDelay;
        fuel -= fuel;
    }

    public void IdleJetForce() {
        if (JetOn == false) { //recharge for fuel
            delay -= Time.deltaTime;
            if (delay <= 0.0f) { fuel += fuelTick * refillSpeed; }
        }
    }
    public void upgradeMaxFuel()
    {
        maxFuel = maxFuel + 10.0f;
    }
    public void upgradeMaxDelay()
    {
        maxDelay = maxDelay * 0.90f;
    }
    public void upgradeJetForce()
    {
        jetForce = jetForce + 2.0f;
    }
}
