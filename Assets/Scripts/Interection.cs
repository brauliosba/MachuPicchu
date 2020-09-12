using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Interaction {

    //time in second
    public float time;

    //title of the question / text of the question
    public string title;

    //correct answer
    public bool correct;

    public int type;
}
