using UnityEngine;
using System.Collections;
using UnityEngine.Events;

    public enum Type {Door,PickUp,Drawer,Light,Electronic};
    public enum State{Open,Close,Pick,Drop};

public class UI_Object : MonoBehaviour
{
    public UI_Choice uiChoice = null;
    private Transform characterTransform = null;

    private UnityEvent myEvent = null;
    private Rigidbody rb = null;

    private OpeningClosingDoor doorScript = null;
    private OpeningClosingDrawer drawerScript = null;
    private ElectronicDevice electronicScript = null;

    private string displayText = null;
    private State myState;
  
    public Type myType;
    public string objectName = null;
    
    void Awake()
    {
        UI_Choice uiChoice = GameObject.FindWithTag("UI_Manager").GetComponent<UI_Choice>();

        if (objectName == null)
            objectName = this.name;

        switch(myType)
        {
            case Type.Door:
                {
                    doorScript = GetComponent<OpeningClosingDoor>();
                    myState = State.Close;
                    break;
                }

            case Type.Drawer:
                {
                    drawerScript = GetComponent<OpeningClosingDrawer>();
                    myState = State.Close;
                    break;
                }

            case Type.Electronic:
                {
                    electronicScript = GetComponent<ElectronicDevice>();
                    myState = State.Close;
                    break;
                }

            case Type.Light:
                {
                    electronicScript = GetComponent<ElectronicDevice>();
                    myState = State.Close;
                    break;
                }

            case Type.PickUp:
                {
                    characterTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
                    rb = GetComponent<Rigidbody>();
                    myState = State.Drop;
                    break;
                }
        }
    }

    public void AddUI()
    {
        if (myEvent == null)
        {
            DecideText();
            myEvent = uiChoice.AddChoice(displayText);
            myEvent.AddListener(OnClick);
        }
    }

    public void RemoveUI()
    {
        if (myEvent != null)
        {
            uiChoice.RemoveChoice(myEvent);
            myEvent.RemoveListener(OnClick);
            myEvent = null;
        }
    }

    private void DecideText()
    {
        switch (myState)
        {
            case State.Open:
                {
                    displayText = "Close " + objectName;
                    break;
                }

            case State.Close:
                {
                    displayText = "Open " + objectName;
                    break;
                }

            case State.Pick:
                {
                    displayText = "Drop " + objectName;
                    break;
                }

            case State.Drop:
                {
                    displayText = "Pick " + objectName;
                    break;
                }
        }
    }

    private void ChangeState()
    {
        switch (myState)
        {
            case State.Open:
                {
                    Debug.Log("Open vers Close");
                    myState = State.Close;
                    return;
                }

            case State.Close:
                {
                    Debug.Log("Close vers Open");
                    myState = State.Open;
                    return;
                }

            case State.Pick:
                {
                    Debug.Log("Pick vers Drop");
                    myState = State.Drop;
                    return;

                }

            case State.Drop:
                {
                    Debug.Log("Drop vers Pick");
                    myState = State.Pick;
                    return;
                }
        }



        if (myState == State.Open)
        {
            Debug.Log("Open vers Close");
            myState = State.Close;
            DecideText();
            uiChoice.UpdateChoice(myEvent, displayText);
            Action();
            return;
        }

        if (myState == State.Close)
        {
            Debug.Log("Close vers Open");
            myState = State.Open;
            DecideText();
            uiChoice.UpdateChoice(myEvent, displayText);
            Action();
            return;
        }


    }
    
    private void Action()
    {
        switch (myType)
        {
            case Type.Door:
                {
                    doorScript.SelectAndPlayAnimation(myState);
                    return;
                }

            case Type.Drawer:
                {
                    drawerScript.PlayAnim(this.name);
                    return;
                }

            case Type.Electronic:
                {
                    electronicScript.ActOn();
                    return;
                }

            case Type.Light:
                {
                    electronicScript.ActOn();

                    if (myState == State.Open)
                        this.transform.GetChild(0).GetComponent<Light>().enabled = false;

                    else if (myState == State.Close)
                        this.transform.GetChild(0).GetComponent<Light>().enabled = true;

                    return;
                }

            case Type.PickUp:
                {
                    if (myState == State.Pick)
                    {
                        this.transform.parent = null;
                        rb.useGravity = true;
                        rb.isKinematic = false;
                        rb = null;
                    }

                    else if (myState == State.Drop)
                    {
                        this.transform.parent = characterTransform;
                        rb.useGravity = false;
                        rb.isKinematic = true;
                    }

                    return;
                }
        }
    }	

    private void OnClick()
    {
        Action();
        ChangeState();
        DecideText();  
        uiChoice.UpdateChoice(myEvent, displayText);  
    }
}
