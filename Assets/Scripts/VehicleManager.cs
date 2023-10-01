
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleManager : MonoSingleton<VehicleManager>
{

    public Customer defaultCustomer;
    private GameManager gameManager;

    public int MaxVehicles;

    public List<Vehicle> activeVehicles;

    public bool HasRoom { get
        {
            return activeVehicles.Count < MaxVehicles;
        } }
    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    public bool AddVehicle(GameObject newObject)
    {
        if (activeVehicles.Count >= MaxVehicles)
        {
            return false;
        }
        GameObject o = Instantiate(newObject, transform);
        activeVehicles.Add(o.GetComponent<Vehicle>());



        return true;



    }
    public void HandleLeftOver(List<Customer> people)
    {
       if(activeVehicles.Count == 0)
        {
            return;
        } 


        for (int i = 0; i < people.Count; i++)
        {
            bool found = false;
            foreach (var v in activeVehicles)
            {
                if (v.customers.Count <= v.maxRoom)
                {
                    v.AddCustomer(people[i]);
                    found = true;
                    continue;
                }
            }
            if (found) continue;

            foreach (var v in activeVehicles)
            {
                if (v.customers.Count <= v.maxRoom * 2)
                {
                    v.AddCustomer(people[i]);
                    continue;
                }
            }
            if (found) continue;

            activeVehicles[Random.Range(0, activeVehicles.Count)].AddCustomer(people[i]);
        }

    }
    public void RunVehicles(float weather)
    {

        List<Vehicle> vehiclsWithPeople = new List<Vehicle>();
        foreach (var v in activeVehicles)
        {
            if (v.customers.Count > 0)
            {
                vehiclsWithPeople.Add(v);
            }
           
        }

        List<Vehicle> broken = new List<Vehicle>();


        for (int i = 0; i < vehiclsWithPeople.Count; i++)
        {
            var vehicle = vehiclsWithPeople[i];
                if (vehicle.Locked)
                {
                    continue;
                }
            if (vehicle.TestStability(weather))
            {
                broken.Add(vehicle);
            }
            else
            {
                gameManager.delivered += vehicle.customers.Count;
                vehicle.Clear();

            }
        }
        int customersOnBroken = 0;
      
        foreach (var item in broken)
        {

            customersOnBroken += item.customers.Count;
            activeVehicles.Remove(item);
            gameManager.HandleBrokenVehicles(item);
            Destroy(item.gameObject);
        }

    }

    public void RunVehicle(Vehicle vehicle)
    {
        float weather = GameManager.Instance.weather;
        if (vehicle.TestStability(weather))
        {
            gameManager.HandleBrokenVehicles(vehicle);
            activeVehicles.Remove(vehicle);
            Destroy(vehicle.gameObject);
        }
        else
        {
            gameManager.delivered += vehicle.customers.Count;
            vehicle.Clear();
            vehicle.Lock();
        }
    }
}
