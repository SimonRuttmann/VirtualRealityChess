using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR.Extras;

public class LaserPointerWrap : MonoBehaviour
{
    [SerializeField] Schachbrett schachbrett;
    protected IInputHandler[] inputhandlers; 
    private SteamVR_LaserPointer steamVrLaserPointer;
    [SerializeField] VrSchachMenu vrSchachMenu;
    [SerializeField] SchachManager schachManager;
    protected IInputHandler[] inputHandlers;

    private void Awake()
    {
        Debug.Log("Awake ausgeführt");
       // schachbrett = GetComponent<Schachbrett>();
        //inputhandler = GetComponent<IInputHandler>();
     //   inputhandlers = GetComponents<IInputHandler>();


         
    
   //     inputHandlers = GetComponents<IInputHandler>();
    


    steamVrLaserPointer = gameObject.GetComponent<SteamVR_LaserPointer>();
        steamVrLaserPointer.PointerIn += OnPointerIn;
        steamVrLaserPointer.PointerOut += OnPointerOut;
        steamVrLaserPointer.PointerClick += OnPointerClick;
    }

    private void OnPointerClick(object sender, PointerEventArgs e)
    {
        //IPointerClickHandler clickHandler = e.target.GetComponent<IPointerClickHandler>();

        //if (clickHandler == null)
        //{
        //    return;
        //}

        //Debug.Log(e.target.tag);
        if(e.target.GetComponent<Figur>() != null)
        {
            Debug.Log("Figurauswahl");
            Vector3 v3 = e.target.position;
            Debug.Log("Schachbrett: " + schachbrett);
            Debug.Log("V3 Position: " + v3);

            schachbrett.OnSquareSelected(v3);
        }
        else if (e.target.tag == "VrButton")
        {
            Debug.Log("VrButton");
            string id = e.target.name;
            switch (id)
            {
                case "Startbutton": this.vrSchachMenu.hideUI(); break;
                case "Neustartbutton": this.schachManager.RestartGame(); this.vrSchachMenu.hideUI(); break;
                case "BeendenButton": this.vrSchachMenu.beendeSpiel(); break;  //Beende ist auskommentiert
            }

        }
        else
        {
           
            //Vector2 ped = new PointerEventData(EventSystem.current).position;

            //Vector3 v33 = steamVrLaserPointer.transform.localPosition;
            //Debug.Log("Pointer Local" + v33);

            //v33 = steamVrLaserPointer.transform.position;
            //Debug.Log("Pointer position" + v33);
            //Vector2 pedv = new PointerEventData(EventSystem.current).pointerCurrentRaycast.world;
            
            //Debug.Log("Schachauswahl press Position" + pedv);
            //schachbrett.OnSquareSelected(new Vector3(0, pedv.x, pedv.y));

            //Debug.Log("Distance Point Press transform position" + new PointerEventData(EventSystem.current).pointerPress.transform.position);
            //Debug.Log("Distance Point Press local position" + new PointerEventData(EventSystem.current).pointerPress);
            //Debug.Log("e Distance2" + e.distance);

            Debug.Log("MURKSER: " + e.hit);
            schachbrett.OnSquareSelected(e.hit);
            //Vector3 pedv3 = new PointerEventData(EventSystem.current).pointerCurrentRaycast.wordNormal;
            //Debug.Log("Schachauswahl World Position" + pedv3);
            //schachbrett.OnSquareSelected(pedv3);
        }   

       
        //Vector2 ped = new PointerEventData(EventSystem.current).position;
        //clickHandler.OnPointerClick(new PointerEventData(EventSystem.current));
        //EventSystem.current.
        //Debug.Log("V3 Position: "+v3);
        //Vector3 vector3 = new Vector3(ped.x, ped.y);
      //  foreach (var handler in inputhandlers){
       //     Debug.Log("Sende Input Received");
        //    handler.ProcessInput(v3, null, null);
       // }
        //inputhandler.ProcessInput(v3,null,null);//x und z wird benötigt
    }

    private void OnPointerOut(object sender, PointerEventArgs e)
    {
        IPointerExitHandler pointerExitHandler = e.target.GetComponent<IPointerExitHandler>();
        if (pointerExitHandler == null)
        {
            return;
        }

        pointerExitHandler.OnPointerExit(new PointerEventData(EventSystem.current));
    }

    private void OnPointerIn(object sender, PointerEventArgs e)
    {
        IPointerEnterHandler pointerEnterHandler = e.target.GetComponent<IPointerEnterHandler>();
        if (pointerEnterHandler == null)
        {
            return;
        }

        pointerEnterHandler.OnPointerEnter(new PointerEventData(EventSystem.current));
    }
}
