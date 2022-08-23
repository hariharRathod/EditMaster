using System;
using System.Collections.Generic;
using GestureRecognizer;
using UnityEngine;

public class MyRecogniserWraper : Recognizer
{
    
    private GesturePattern myGesturePattern;

    private List<GesturePattern> originalPatternsData;

    private void OnEnable()
    {
        GameEvents.MyDrawableAreaIsOn += GetGesturePattern;
    }

    private void OnDisable()
    {
        GameEvents.MyDrawableAreaIsOn -= GetGesturePattern;
    }

    
    private void Start()
    {
        originalPatternsData = new List<GesturePattern>(patterns);
       
    }

    public void SetGesturePattern(GesturePattern pattern)
    {
        patterns.Clear();
        patterns.Add(myGesturePattern);
        print("patterns count in mygesture: " + patterns.Count);
        
    }

    public void ResetGesturePatterns()
    {
        patterns.Clear();
        patterns = new List<GesturePattern>(originalPatternsData);
    }
    
    private void GetGesturePattern(Transform arg1, GesturePattern gesturePattern)
    {
        myGesturePattern = gesturePattern;
        SetGesturePattern(myGesturePattern);
    }
}
