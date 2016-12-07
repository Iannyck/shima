using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class EventList
{
    public UnityEvent unEvent;
    public string text;
    private bool isEmpty;

    public EventList(UnityEvent a)
    {
        unEvent = a;
		text = "Aucun Choix";
        isEmpty = true;
    }

    public bool IsEmpty()
    {
        return isEmpty;
    }

    public void Initialize(string texte)
    {
        text = texte;
        isEmpty = false;
    }

    public void Reset()
    {
        text = "Aucun Choix";
        isEmpty = true;
    }
}

public class UI_Choice : MonoBehaviour
{
    public int hauteurChoix = 30;

    private UnityEvent OnClicked1 = new UnityEvent();
    private UnityEvent OnClicked2 = new UnityEvent();
    private UnityEvent OnClicked3 = new UnityEvent();

    private List<EventList> listeEvent = new List<EventList>();

    void Start()
    {
        EventList event1 = new EventList(OnClicked1);
        EventList event2 = new EventList(OnClicked2);
        EventList event3 = new EventList(OnClicked3);

        listeEvent.Add(event1);
        listeEvent.Add(event2);
        listeEvent.Add(event3);
    }

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            if (!listeEvent[0].IsEmpty())
            {
                listeEvent[0].unEvent.Invoke();
            }
        }

        if (Input.GetKeyDown("2"))
        {
            if (!listeEvent[1].IsEmpty())
            {
                listeEvent[1].unEvent.Invoke();
            }
        }

        if (Input.GetKeyDown("3"))
        {
            if (!listeEvent[2].IsEmpty())
            {
                listeEvent[2].unEvent.Invoke();
            }
        }

    }

    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width - 200, 0, 200, 120), "Choice Menu");

        if (GUI.Button(new Rect(Screen.width - 200, hauteurChoix*1, 200, 20), listeEvent[0].text))
        {
            listeEvent[0].unEvent.Invoke();
        }

        if (GUI.Button(new Rect(Screen.width - 200, hauteurChoix * 2, 200, 20), listeEvent[1].text))
        {
            listeEvent[1].unEvent.Invoke();
        }

        if (GUI.Button(new Rect(Screen.width - 200, hauteurChoix * 3, 200, 20), listeEvent[2].text))
        {
            listeEvent[2].unEvent.Invoke();
        }
    }

    public UnityEvent AddChoice(string texte)
    {
        for (int i = 0; i < listeEvent.Count; i++)
        {
            if (listeEvent[i].IsEmpty())
            {
                listeEvent[i].Initialize((i+1)+": " +texte);
                return listeEvent[i].unEvent;
            }
        }

//        Debug.Log("ERREUR : Liste de choix complete");
        return null;
    }

    public void RemoveChoice(UnityEvent a)
    {
        for (int i=0; i<listeEvent.Count; i++)
        {
            if (listeEvent[i].unEvent == a)
            {
                listeEvent[i].Reset();
                return;
            }
        }

//        Debug.Log("ERREUR : Evenement introuvable pour Remove");
    }

    public void UpdateChoice(UnityEvent a,string b)
    {
        for (int i = 0; i < listeEvent.Count; i++)
        {
            if (listeEvent[i].unEvent == a)
            {
                listeEvent[i].text = (i+1)+": " + b;
                return;
            }
        }

        Debug.Log("ERREUR : Evenement introuvable pour Update");
    }
}
