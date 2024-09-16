using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stat : MonoBehaviour
{
    //this component needs to work with uiImages health bar and mana bar
    private Image component;

    [SerializeField]
    private Text statValue;

    [SerializeField]
    private float lerpSpeed;
    
    //current fill shows the current fill of bars
    private float currentFill;

    //MyMaxValue needs to control the player hp and mana  value
    public float MyMaxValue { get; set; }

    // this is the value at the moment
    private float currentValue;

    //Same but now its public so we gonna use it in player
    public float myCurrentValue
    {
        get
        {
            //gets MyCurrentValue gets currentValue to control player
            return currentValue;
        }

        set
        {
            if (value > MyMaxValue)
            {
                //this is check to controll that current value never will me more then maxValue
                currentValue = MyMaxValue;
            }
            else if(value < 0)
            {
                //this should be to control -value
                currentValue = 0;
            }
            else
            {
                //if both previous check is okay  then current value gets value
                currentValue = value;
            }
            //we need this to control current fill 
            currentFill = currentValue / MyMaxValue;

            if (statValue != null)
            {
                statValue.text = currentValue + "/" + MyMaxValue;
            }
        }
    }
    void Start()
    {
        component = GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleBar();
    }
    //this gives those stats at game begining
    public void initialize(float currentValue, float maxValue)
    {
        if (component == null)
        {
            component = GetComponent<Image>();
        }
        MyMaxValue = maxValue;
        myCurrentValue = currentValue;
        component.fillAmount = myCurrentValue / MyMaxValue;
    }

    private void HandleBar()
    {
        //if current fill not equal to fill amount in unity then it fills quiqly by using frames and lerp speed
        if (currentFill != component.fillAmount)
        {
            component.fillAmount = Mathf.Lerp(component.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
        }
    }
}
