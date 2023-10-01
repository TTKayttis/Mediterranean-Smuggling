using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static GameEvents;

public class GameManager : MonoSingleton<GameManager>
{
    public GameObject gameOverCanvas;

    public float weather = 0;

    public int delivered = 0;

    public int lost = 0;

    public int peopleNotServed = 0;

    public bool fillLeftOverBoats = false;


    public UnityEvent OnDayChange = new();

    [SerializeField] WeatherRoller weatherRoller;

    public List<BorderPatrolState> borderPatrolStates = new List<BorderPatrolState>();

    public List<BorderPatrolState> borderPatrolStatesLimited = new List<BorderPatrolState>();

    public List<BorderPatrolState> borderPatrolStatesStart = new List<BorderPatrolState>();


    [SerializeField] GameEvents gameEvents = new GameEvents();
    
    public float TurnLength = 60f;

    public float TurnTimeLeft { get; private set; }

    public static int round = 1;
    private void Start()
    {
        round =1;
        GuideText.Instance.AddGuideText("Buy ships, drag and drop people on ships.");
        GuideText.Instance.AddGuideText("Ships with people will be shipped at end of turn");
        TurnTimeLeft = TurnLength;
        gameEvents.InitEvents();
    }
    private void FixedUpdate()
    {
        TurnTimeLeft -= Time.fixedDeltaTime;

        if(TurnTimeLeft <= 0)
        {
            TickDay();
           
        }
    }


    public void TickDay()
    {
        VehicleManager.Instance.RunVehicles(weather);
        round++;
        TurnLength += 10f / round;
        TurnTimeLeft = TurnLength;

        weather = weatherRoller.RollWeather();
      
        OnDayChange.Invoke();
        if (fillLeftOverBoats)
        {
            VehicleManager.Instance.HandleLeftOver(CustomerSpawner.Instance.SpawnCustomer());
        }
        else
        {
            CustomerSpawner.Instance.Clear();
            CustomerSpawner.Instance.SpawnCustomer();
        }
       


        foreach (var v in VehicleManager.Instance.activeVehicles)
        {
            v.UnLock();
        }
        CheckEvents();
    }

    public void CivilWar()
    {
        fillLeftOverBoats = true;
        GuideText.Instance.AddGuideText("Civil war: people will now fill ships automatically at turn end");

    }

    private void CheckEvents()
    {
      foreach(var e in gameEvents.deadCountEvents)
        {
            if (!e.triggered)
            {
                if(e.limit < lost)
                {
                    EventUI.Instance.SetText(e.message);
                    if (e is GameEvents.IOnTrigger)
                    {
                        ((IOnTrigger)e).Trigger();
                    }
                    e.triggered = true;
                    continue;
                    
                }
            }

        }
        foreach (var e in gameEvents.smuggleEvents)
        {
            if (!e.triggered)
            {
                if (e.limit < delivered)
                {
                    EventUI.Instance.SetText(e.message);
                    if (e is GameEvents.IOnTrigger)
                    {
                        ((IOnTrigger)e).Trigger();
                    }
                    e.triggered = true;
                    continue;

                }
            }

        }
    }
    public void GameOver()
    {
       gameOverCanvas.SetActive(true);


    }

    public void Reload()
    {
        SceneManager.LoadScene(0);


    }

    public void BuyVehicle(GameObject o, Vehicle vehicle)
    {
        var vehicleManager = VehicleManager.Instance;
        if (vehicleManager.HasRoom)
        {
            if (MoneyManager.Instance.TryUseMoney(vehicle.price))
            {
                vehicleManager.AddVehicle(o);

            }
        }
      

    }


    internal void HandleBrokenVehicles(Vehicle vehicle)
    {
       List<BorderPatrolState> possibleEvents = new List<BorderPatrolState>();
        int peopleOnVehicle = vehicle.customers.Count;
        foreach (var state in borderPatrolStates)
        {
            if(vehicle.maxRoom <= state.minShipSizeToHappen)
            {
                possibleEvents.Add(state);
              
            }

        }
        if(possibleEvents.Count == 0)
        {
            
            lost += peopleOnVehicle;
            EventUI.Instance.SetText($"{peopleOnVehicle} people drowned after ship sunk");
        }
        else
        {
            var state = possibleEvents[Random.Range(0, possibleEvents.Count)];
            int saved = state.SaveCustomers(peopleOnVehicle);
            int dead = peopleOnVehicle - saved;

            lost += dead;
            delivered += saved;
            EventUI.Instance.SetText(state.GetEventString(dead, saved));
        }


    }

    internal void HandleLeftBehind(int leftBehind)
    {
        if (fillLeftOverBoats)
        {
            VehicleManager.Instance.RunVehicles(weather);
            return;
        }
        peopleNotServed += leftBehind;
        
    }

    
}
