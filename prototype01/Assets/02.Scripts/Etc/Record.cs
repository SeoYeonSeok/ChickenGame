using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record : IComparable<Record>
{
    public string name;
    public int score;

    public Record(string _name, int _score)
    {
        name = _name;
        score = _score;
    }

    public string GetName()
    {
        return name;
    }

    public int GetScore()
    {
        return score;
    }

    public void SetName(string _name)
    {
        name = _name;
    }

    public void SetScore(int _score) 
    {
        score = _score;
    }

    public void Print()
    {
        Debug.Log(name + "'s Score : " + score);
    }
    
    public int CompareTo(Record other)
    {
        if (other == null) return 1;

        return other.score.CompareTo(score);
    }    
}
