using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OvenRegionSpawn : MonoBehaviour
{
    public List<Sprite> AvailablePizza = new List<Sprite>();
    private SpriteRenderer PizzaRender;
    private SpriteRenderer AshRender;
    public PizzaState CurrentState = PizzaState.NotStarted;


    public GameObject GameManager;
    private GameManager _GameManger
    {
        get
        {
            return GameManager.GetComponentInChildren<GameManager>();
        }
    }


    // Age
    public int Age = 0;

    // TotalCookTime
    public int CookingLength = 0;

    // TotalPerfectTime
    public int PerfectLength = 0;
    
    // TotalLifeSpan
    public int LifeSpanLength = 0;

    public int TotalGameTimeInSeconds = 60;

    private static readonly System.Random Prng = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        var Sprites = GetComponentsInChildren<SpriteRenderer>();
        PizzaRender = Sprites.Where(x=>x.name == "OvenPizza").FirstOrDefault();
        AshRender = Sprites.Where(x => x.name == "Ash").FirstOrDefault();
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentState == PizzaState.NotStarted)
        {
            SpawnFreshPizza();
            Invoke("HarvestPizza", 6);
        }

        
    }

    void CleanOven()
    {
        PizzaRender.sprite = null;
        AshRender.enabled = false;
        CurrentState = PizzaState.NotStarted;
        Age = 0;
    }

    void SpawnFreshPizza()
    {
        CleanOven();

        // Select a random sprite
        PizzaRender.sprite = AvailablePizza.GetRandom();

        CookingLength = Prng.Next(3,TotalGameTimeInSeconds/6);
        PerfectLength = CookingLength+Prng.Next(5, TotalGameTimeInSeconds/12);
        LifeSpanLength = PerfectLength + 5;
        CurrentState = PizzaState.Cooking;
        
        InvokeRepeating("CookPizza", 1, 1);
    }

    void HarvestPizza()
    {
        if (CurrentState != PizzaState.NotStarted)
        {
            switch (CurrentState)
            {
                case PizzaState.Cooking:
                    _GameManger.IncrementScore(1.50);
                    break;
                case PizzaState.Perfect:
                    _GameManger.IncrementScore(5);
                    break;
                case PizzaState.Burnt:
                    _GameManger.IncrementScore(-.5);
                    break;
            }
        }
    }

    void CookPizza()
    {
        if (CurrentState != PizzaState.NotStarted)
        {
            if (Age <= CookingLength)
            {
                CurrentState = PizzaState.Cooking;
            }
            else if (Age > CookingLength && Age < PerfectLength)
            {
                CurrentState = PizzaState.Perfect;
            }
            else if (Age > PerfectLength && Age < LifeSpanLength)
            {
                CurrentState = PizzaState.Burnt;
            }
            else if (Age > LifeSpanLength)
            {
                // Destroy Object
                // Destroy Effect and Remove Object from Scene
                CancelInvoke("CookPizza");
                CleanOven();
                return;
            }
            

            switch (CurrentState)
            {
                case PizzaState.Cooking:
                    {
                        // Particle Effects
                        break;
                    }
                case PizzaState.Perfect:
                    {
                        // Perfect Effects
                        break;
                    }
                case PizzaState.Burnt:
                    {
                        // Apply Burnt Effect
                        AshRender.enabled = true;
                        break;
                    }
            }
            Age += 1;
        }
    }
}
