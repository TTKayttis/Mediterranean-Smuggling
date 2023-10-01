using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Vehicle : MonoBehaviour
{
    //how many people fit
    public int maxRoom;

    public float price;

    public bool Locked = false;

    [SerializeField] SeatHandler seatHandler;

    public UnityEvent OnLock = new();

    public UnityEvent OnUnlock = new();

    //modifier for how easily breaks on overload
    [Range(1,9)]
    public int stability;

    public List<Customer> customers = new List<Customer>();

    /// <summary>
    /// returns true if weather broke vehicle
    /// </summary>
    /// <param name="weather"></param>
    /// <returns></returns>
    public bool TestStability(float weather)
    {
        
        float overload = (float)customers.Count / maxRoom;

        if(overload <= 1)
        {
            overload = 0;
        }
        else
        {
            overload -= 1;
        }
       
        //stab 1 breaks at 5 weather without overload
        return weather >= (4.5f + stability * 0.5f)  - overload * 5f;
    }

    public void ShipVehicle()
    {
        VehicleManager.Instance.RunVehicle(this);
    }
    public void Clear()
    {
        customers.Clear();
        seatHandler.Clear();

    }
    public void AddCustomer(Customer customer)
    {

        MoneyManager.Instance.money += customer.money;
        customers.Add(customer);
        seatHandler.AddCustomer();
    }
    public void Lock()
    {
        Locked = true;
        OnLock.Invoke();

    }

    public void UnLock()
    {
        Locked = false;
        OnUnlock.Invoke();


    }
}
