﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControladorDeJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public bool joystickComPosicaoFixa = true;
    public int distanciaDeAfastamentoDoJoystick = 4;

    private Image bgImage; 
    private Image joystickKnobImage; 
    private Vector3 inputVector; 
    private Vector3 unNormalizedInput; 
    private Vector3[] fourCornersArray = new Vector3[4]; 
    private Vector2 bgImageStartPosition;

#pragma warning disable 0649
    [SerializeField] private MyButtonEvents[] buttons;
#pragma warning restore 0649
    //[SerializeField] private MyButtonEvents button3;

    public static ControladorDeJoystick cj;

    public MyButtonEvents GetButton(int numButton)
    {
        if (buttons.Length > numButton)
            return buttons[numButton];
        else return null;
    }

    private void Start()
    {
        if (cj == null)
            cj = this;
        else
            Destroy(gameObject);

        if (GetComponent<Image>() == null)
        {
            Debug.LogError("There is no joystick image attached to this script.");
        }

        if (transform.GetChild(0).GetComponent<Image>() == null)
        {
            Debug.LogError("There is no joystick handle image attached to this script.");
        }

        if (GetComponent<Image>() != null && transform.GetChild(0).GetComponent<Image>() != null)
        {
            bgImage = GetComponent<Image>(); 
            joystickKnobImage = transform.GetChild(0).GetComponent<Image>(); 
            
            bgImage.rectTransform.GetWorldCorners(fourCornersArray); 

            bgImageStartPosition = fourCornersArray[3]; 
            bgImage.rectTransform.pivot = new Vector2(1, 0); 

            bgImage.rectTransform.anchorMin = new Vector2(0, 0); 
            bgImage.rectTransform.anchorMax = new Vector2(0, 0); 
            bgImage.rectTransform.position = bgImageStartPosition; 
        }

        EventAgregator.AddListener(EventKey.inicializaDisparaTexto, OnStartTalk);
        EventAgregator.AddListener(EventKey.finalizaDisparaTexto, OnFinishTalk);
        EventAgregator.AddListener(EventKey.requestHideControllers, OnStartTalk);
        EventAgregator.AddListener(EventKey.requestShowControllers, OnFinishTalk);
        EventAgregator.AddListener(EventKey.startCheckPoint, OnStartCheckPoint);
        EventAgregator.AddListener(EventKey.requestToFillDates, OnRequestFillDates);
        EventAgregator.AddListener(EventKey.starterHudForTest, VerifiqueBtnDash);
        EventAgregator.AddListener(EventKey.allAbilityOn, VerifiqueBtnDash);
        EventAgregator.AddListener(EventKey.changeHardwareController, OnChangeHardwareController);

        OnChangeHardwareController(new StandardSendGameEvent(EventKey.changeHardwareController, GlobalController.g.Control));
    }

    private void OnDestroy()
    {
        EventAgregator.RemoveListener(EventKey.inicializaDisparaTexto, OnStartTalk);
        EventAgregator.RemoveListener(EventKey.finalizaDisparaTexto, OnFinishTalk);
        EventAgregator.RemoveListener(EventKey.requestHideControllers, OnStartTalk);
        EventAgregator.RemoveListener(EventKey.requestShowControllers, OnFinishTalk);
        EventAgregator.RemoveListener(EventKey.startCheckPoint, OnStartCheckPoint);
        EventAgregator.RemoveListener(EventKey.requestToFillDates, OnRequestFillDates);
        EventAgregator.RemoveListener(EventKey.starterHudForTest, VerifiqueBtnDash);
        EventAgregator.RemoveListener(EventKey.allAbilityOn, VerifiqueBtnDash);
        EventAgregator.RemoveListener(EventKey.changeHardwareController, OnChangeHardwareController);
    }

    private void OnChangeHardwareController(IGameEvent e)
    {
        StandardSendGameEvent ssge = (StandardSendGameEvent)e;

        transform.parent.gameObject.SetActive((Controlador)ssge.MyObject[0] == Controlador.Android);        

    }

    private void VerifiqueBtnDash(IGameEvent e)
    {
        new MyInvokeMethod().InvokeAoFimDoQuadro(() =>
        {
            MostrarBotaoDash(GameController.g.Manager.Dados.TemDash);
        });
    }

    private void OnRequestFillDates(IGameEvent e)
    {
        StandardSendGameEvent ssge = (StandardSendGameEvent)e;
        SaveDates S = (SaveDates)ssge.MyObject[0];

        if (S == null)
        {
            MostrarBotaoDash(false);
        }
        else
        {
            MostrarBotaoDash(S.Dados.TemDash);
        }

    }

    void MostrarBotaoDash(bool mostrar)
    {
        if(buttons!=null)
            if(buttons[3]!=null)
                buttons[3].gameObject.SetActive(mostrar);
    }

    void OnStartCheckPoint(IGameEvent e)
    {
        OnPointerUp(null);
    }

    void OnFinishTalk(IGameEvent e)
    {
        if (GlobalController.g.Control == Controlador.Android)
            transform.parent.gameObject.SetActive(true);
    }

    void OnStartTalk(IGameEvent e)
    {
        OnPointerUp(null);

        if(GlobalController.g.Control==Controlador.Android)
            transform.parent.gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData ped)
    {
        Vector2 localPoint = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImage.rectTransform, ped.position, ped.pressEventCamera, out localPoint))
        {
            localPoint.x = (localPoint.x / bgImage.rectTransform.sizeDelta.x);
            localPoint.y = (localPoint.y / bgImage.rectTransform.sizeDelta.y);

            inputVector = new Vector3(localPoint.x * 2 + 1, localPoint.y * 2 - 1, 0);

            unNormalizedInput = inputVector;

            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            joystickKnobImage.rectTransform.anchoredPosition =
             new Vector3(inputVector.x * (bgImage.rectTransform.sizeDelta.x / distanciaDeAfastamentoDoJoystick),
                         inputVector.y * (bgImage.rectTransform.sizeDelta.y / distanciaDeAfastamentoDoJoystick));

            if (!joystickComPosicaoFixa)
            {
                // if dragging outside the circle of the background image
                if (unNormalizedInput.magnitude > inputVector.magnitude)
                {
                    var currentPosition = bgImage.rectTransform.position;
                    currentPosition.x += ped.delta.x;
                    currentPosition.y += ped.delta.y;

                    // keeps the joystick on the left-hand half of the screen
                    currentPosition.x = Mathf.Clamp(currentPosition.x, 0 + bgImage.rectTransform.sizeDelta.x, Screen.width / 2);
                    currentPosition.y = Mathf.Clamp(currentPosition.y, 0, Screen.height - bgImage.rectTransform.sizeDelta.y);

                    // moves the entire joystick along with the drag  
                    bgImage.rectTransform.position = currentPosition;
                }
            }
        }
    }

    public void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector3.zero;
        joystickKnobImage.rectTransform.anchoredPosition = Vector3.zero;
    }

    public Vector3 GetInputDirection()
    {
        return new Vector3(inputVector.x, inputVector.y, 0);
    }

    public float GetInputVal(string qualVal)
    {
        float val = 0;
        switch (qualVal)
        {
            case "horizontal":
                val = inputVector.x!=0?Mathf.Sign(inputVector.x)*Mathf.Min(1.5f*GetInputDirection().magnitude,1):0;
            break;
            case "vertical":
                val = inputVector.y;
            break;
        }
        return val;
    }
}
