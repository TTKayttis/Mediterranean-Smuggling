using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Events;
public class GameEvents
{
   public class DefaultEvent
    {
       public readonly string message;

        public bool triggered = false;

        public DefaultEvent(string s) { message = s; }
    }

    public class DeadCountEvent : DefaultEvent
    {
        public readonly int limit;

        public DeadCountEvent(string s, int l) : base(s)
        {
            limit = l;
        }
    }
     
    public interface IOnTrigger
    {
        public void Trigger();

    }

    public class GameOverEnvet : DeadCountEvent, IOnTrigger
    {
        public GameOverEnvet(string s, int limit) : base(s, limit)
        {
        }

        public void Trigger()
        {
            GameManager.Instance.GameOver();
        }
    }
    public class SmuggledCountEvent : DefaultEvent, IOnTrigger
    {
        public readonly int limit;

        public UnityEvent OnTrigger = new();
        public SmuggledCountEvent(string s, int l) : base(s)
        {
            limit = l;
        }

        public void Trigger()
        {
            OnTrigger.Invoke();
        }
    }


    public List<DeadCountEvent> deadCountEvents = new List<DeadCountEvent>();
    public List<SmuggledCountEvent> smuggleEvents = new List<SmuggledCountEvent>();
    public void InitEvents()
    {
        deadCountEvents.Add(new DeadCountEvent("Europol has stater to investigate your smuggling ring.", 100));
        deadCountEvents.Add(new DeadCountEvent("Europol has nearly finished their investigation.", 200));
        deadCountEvents.Add(new GameOverEnvet("Europol has arrested you.", 300));

        var civilWar = new SmuggledCountEvent("Civil war has begun, people will now fill ships automatically at turn end.", 50);
        civilWar.OnTrigger.AddListener(() => GameManager.Instance.CivilWar());
        smuggleEvents.Add(civilWar);

    }






}
