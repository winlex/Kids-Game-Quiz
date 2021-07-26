using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public enum difficulty { easy, medium, hard}

public class Generate : MonoBehaviour {
    public UnityEvent Finish;

    public difficulty Difficulty = difficulty.easy;
    public int Easy = 3;
    public int Medium = 6;
    public int Hard = 9;
    public CardBundleData BundleSprites;
    public GameObject Cell;
    public Text Task;

    private Grid grid;
    private System.Random rand = new System.Random();
    private List<CardData> currentSpriteOnDisplay = new List<CardData>();
    private List<CardData> LastAnswer = new List<CardData>();
    private bool EndGame = false;

    void Start()
    {
        CreateLevel();
        Task.DOFade(1, 2);
    }

    public void CreateLevel()
    {
        switch (Difficulty)
        {
            case difficulty.easy:
                {
                    grid = new Grid(Easy);
                }
                break;
            case difficulty.medium:
                {
                    grid = new Grid(Medium);
                }
                break;
            case difficulty.hard:
                {
                    grid = new Grid(Hard);
                }
                break;
        }

        for (int i = 0; i < grid.Coordinates.Count; i++)
        {
            CardData currentLetter = new CardData();
            do
            {
                currentLetter = BundleSprites.CardData[rand.Next(0, BundleSprites.CardData.Length)];
            } while (currentSpriteOnDisplay.IndexOf(currentLetter) > -1);
            currentSpriteOnDisplay.Add(currentLetter);
            
            GameObject element = Cell.GetComponent<Transform>().GetChild(1).gameObject;
            SpriteRenderer currentSprite = element.GetComponent<SpriteRenderer>();
            currentSprite.sprite = currentLetter.Sprite;
            GameObject letter = Instantiate(
                Cell,
                grid.Coordinates[i],
                Quaternion.identity,
                this.GetComponent<Transform>()
            ) as GameObject;
            letter.name = currentLetter.Identifier;
            if (currentLetter.Identifier == "7" || currentLetter.Identifier == "8")
                letter.GetComponent<Transform>().GetChild(1).Rotate(new Vector3(0, 0, -90));
            else
                letter.GetComponent<Transform>().GetChild(1).Rotate(new Vector3(0, 0, 0));
        }

        CardData temp = new CardData();
        do
        {
            temp = currentSpriteOnDisplay[rand.Next(0, currentSpriteOnDisplay.Count)];
        } while (LastAnswer.IndexOf(temp) > -1);
        LastAnswer.Add(temp);
        Task.text = "Find " + temp.Identifier;
    }

    public void NextLevel()
    {
        Transform thisTransform = this.GetComponent<Transform>();
        int childC = thisTransform.childCount;

        switch (Difficulty)
        {
            case difficulty.easy:
                for (int i = 0; i < childC; i++)
                    Destroy(thisTransform.GetChild(i).gameObject);
                currentSpriteOnDisplay.Clear();

                Difficulty = difficulty.medium;
                CreateLevel();
                break;
            case difficulty.medium:
                for (int i = 0; i < childC; i++)
                    Destroy(thisTransform.GetChild(i).gameObject);
                currentSpriteOnDisplay.Clear();

                Difficulty = difficulty.hard;
                CreateLevel();
                break;
            case difficulty.hard:
                Finish?.Invoke();
                EndGame = true;
                break;
        }
    }

    public bool CheckAnswer(string nameSelectedSprite)
    {
        if (!EndGame && LastAnswer[LastAnswer.Count - 1].Identifier == nameSelectedSprite) return true;
        else return false;
    }

    public void DisableCells()
    {
        Transform transform = this.GetComponent<Transform>();
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
            Destroy(transform.GetChild(i).GetComponent<BoxCollider2D>());
    }
}
