using UnityEngine;
using System.Collections;
using UnityEngine.Events;

    public enum Type {Door,PickUp,Drawer,Light,Electronic};
    public enum State{Open,Close,Pick,Drop};

public class UI_Object : MonoBehaviour
{
    // Contient l'ensemble des parametres publics et prives de la classe
    #region Parametres

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
    public State initialState;
    public string objectName = null;

    #endregion

    // Contient l'ensemble des fonctions MonoBehavior (Awake)
    #region MonoBehavior

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
                    myState = initialState;
                    break;
                }

            case Type.Drawer:
                {
                    drawerScript = GetComponentInParent<OpeningClosingDrawer>();
                    myState = initialState;
                    break;
                }

            case Type.Electronic:
                {
                    electronicScript = GetComponent<ElectronicDevice>();
                    myState = initialState;
                    break;
                }

            case Type.Light:
                {
                    electronicScript = GetComponent<ElectronicDevice>();
                    myState = initialState;
                    break;
                }

            case Type.PickUp:
                {
                    characterTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
                    rb = GetComponent<Rigidbody>();
                    myState = initialState;
                    break;
                }
        }
    }

    #endregion

    // Contient l'ensemble des fonctions qui permettent la mise à jour du UI
    #region MAJ_UI

    public void AddUI() // Permet d'ajouter le texte dans la liste des choix 
    {
        if (myEvent == null)
        {
            DecideText();
            myEvent = uiChoice.AddChoice(displayText);

            if (myEvent != null)
                myEvent.AddListener(OnClick);
        }
    } 

    public void RemoveUI() // Permet d'ajouter le texte de la liste des choix 
    {
        if (myEvent != null)
        {
            uiChoice.RemoveChoice(myEvent);
            myEvent.RemoveListener(OnClick);
            myEvent = null;
        }
    }

    private void DecideText()// Permet de déterminer le texte à afficher en fonction de l'état et du nom de l'objet 
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

    public void ChangeState() // Permet de déterminer le texte à afficher en fonction de l'état et du nom de l'objet 
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
    
    public void Action() // Permet de déterminer l'action à effectuer en fonction du type de l'objet et de son état 
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
                   drawerScript.PlayAnim(this.gameObject.name);
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
//					Debug.Log ("Take");
                        this.transform.parent = null;
                        rb.useGravity = true;
                        rb.isKinematic = false;
                    }

                    else if (myState == State.Drop)
                    {
//					Debug.Log ("Release");
                        this.transform.parent = characterTransform;
                        rb.useGravity = false;
                        rb.isKinematic = true;
                    }

                    return;
                }
        }
    }

	public void PutItemAt(float x, float y, float z) {
//		Debug.Log (this.transform.position.x + "," + this.transform.position.y + "," + this.transform.position.z);
//		this.transform.position = new Vector3 (0, 0, 0);
		this.transform.position = new Vector3 (x, y, z);
//		Debug.Log (newPosition.x + "," + newPosition.y + "," + newPosition.z);
//		transform.Translate (newPosition.x, newPosition.y, newPosition.z);
//		Debug.Log (this.transform.position.x + "," + this.transform.position.y + "," + this.transform.position.z);
	}

    private void OnClick() // Permet de déterminer les actions à effectuer lorsque l'utilisateur appuie sur le choix 
    {
        Action();
        ChangeState();
        DecideText();  
        uiChoice.UpdateChoice(myEvent, displayText);  
    }

    #endregion
}
