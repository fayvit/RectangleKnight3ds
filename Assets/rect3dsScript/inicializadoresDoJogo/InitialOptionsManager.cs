﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InitialOptionsManager : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private MenuBasico menu;
    [SerializeField] private MenuDeOpcoes menuO;
    [SerializeField] private MenuDoNovoJogoCarregarDeletar menuDoNovo;
    [SerializeField] private LanguageSwitcher languageS;
    [SerializeField] private InputTextDoCriandoNovoJogo input;
    [SerializeField] private CreditosDoJogo creditos;
#pragma warning restore 0649
    private EstadoDoMenu estado = EstadoDoMenu.preLoad;

    private InputTextDoCriandoNovoJogo Input { get { return input; } set { input = value; } }

    private enum EstadoDoMenu
    {
        preLoad,
        faseInicial,
        menuNovoJogoCarregarDeletar,
        externalUpdate,
        menuDeLinguagensAberto
    }

    IEnumerator PedirSomDoMenu()
    {
        yield return new WaitForEndOfFrame();

        EventAgregator.Publish(new StandardSendGameEvent(EventKey.startMusic, NameMusic.IntroTheme));

    }

    // Start is called before the first frame update
    void Start()
    {
        //Firebase.Analytics.FirebaseAnalytics.SetCurrentScreen("menuInicial", "gameMenu");

        StartCoroutine(PedirSomDoMenu());

        //SaveDatesManager.Load();

        Debug.Log(SaveDatesManager.s.SavedGames.Count + " : " + SaveDatesManager.s.SaveProps.Count + " : " + SaveDatesManager.s.ChosenLanguage);

        /*
#if UNITY_ANDROID
        GooglePlayTrophyDictionary.Start();
#else */
        RequestStandardMenu(null);
//#endif

        EventAgregator.AddListener(EventKey.googlePlayTrySingInFinish, RequestStandardMenu);
        EventAgregator.AddListener(EventKey.returnOfdeleteFile, ReturnOfDelete);
        EventAgregator.AddListener(EventKey.returnOfInputNameOfGame, ReturnOfDelete);
        EventAgregator.AddListener(EventKey.startLoadDeleteButtonPress, OnStartLoadDeleteButtonPress);
        EventAgregator.AddListener(EventKey.returnToMainMenu, OnReturnToMainMenu);
    }

    private void OnDestroy()
    {
        EventAgregator.RemoveListener(EventKey.googlePlayTrySingInFinish, RequestStandardMenu);
        EventAgregator.RemoveListener(EventKey.returnOfdeleteFile, ReturnOfDelete);
        EventAgregator.RemoveListener(EventKey.returnOfInputNameOfGame, ReturnOfDelete);
        EventAgregator.RemoveListener(EventKey.startLoadDeleteButtonPress, OnStartLoadDeleteButtonPress);
        EventAgregator.RemoveListener(EventKey.returnToMainMenu, OnReturnToMainMenu);

    }

    void RequestStandardMenu(IGameEvent e)
    {
        estado = EstadoDoMenu.faseInicial;
        menu.IniciarHud(OnInitialMenuOptionSelect, BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.opcoesDeMenu).ToArray());
    }



    void OnReturnToMainMenu(IGameEvent e)
    {
        BotaoVoltarAoMenuPrincipal();
    }

    void OnStartLoadDeleteButtonPress(IGameEvent e)
    {
        StandardSendGameEvent ssge = (StandardSendGameEvent)e;
        int qual = (int)ssge.MyObject[0];
        if ((bool)ssge.MyObject[1])
        {
            menuDoNovo.IniciarJogo(qual);
        }
        else
            menuDoNovo.DeletarJogo(qual);

        estado = EstadoDoMenu.externalUpdate;

    }

    void ReturnOfDelete(IGameEvent e)
    {
        estado = EstadoDoMenu.menuNovoJogoCarregarDeletar;
    }

    // Update is called once per frame
    void Update()
    {
        switch (estado)
        {
            case EstadoDoMenu.faseInicial:
                menu.MudarOpcao();

                if (ActionManager.ButtonUp(0, GlobalController.g.Control))
                {
                    OnInitialMenuOptionSelect(menu.OpcaoEscolhida);
                }
                break;
            case EstadoDoMenu.menuNovoJogoCarregarDeletar:
                menuDoNovo.MudarOpcao();

                if (ActionManager.ButtonUp(0, GlobalController.g.Control))
                {
                    OnMenuOfNewOptionSelect(menuDoNovo.OpcaoEscolhida);
                }
                else
                if (ActionManager.ButtonUp(2, GlobalController.g.Control))
                {
                    BotaoVoltarAoMenuPrincipal();
                }
                break;
            case EstadoDoMenu.menuDeLinguagensAberto:
                languageS.Update();
                break;
        }
    }

    public void OnMenuOfNewOptionSelect(int qual)
    {
        if (qual == 0)
        {
            Input.Iniciar();
        }
        else
        {
            menuDoNovo.IsDeleteOrLoad();
        }

        estado = EstadoDoMenu.externalUpdate;
    }

    void OnInitialMenuOptionSelect(int option)
    {
        menu.FinalizarHud();

        switch (option)
        {
            case 0:
                menuDoNovo.IniciarHud();
                estado = EstadoDoMenu.menuNovoJogoCarregarDeletar;
                break;
            case 1:
                menuO.gameObject.SetActive(true);
                estado = EstadoDoMenu.externalUpdate;
                break;
            case 2:
                languageS.Start();
                estado = EstadoDoMenu.menuDeLinguagensAberto;
                break;
            case 3:
                creditos.Start();
                estado = EstadoDoMenu.externalUpdate;
                break;
        }
    }

    public void BotaoVoltarAoMenuPrincipal()
    {
        estado = EstadoDoMenu.faseInicial;
        menuDoNovo.FinalizarHud(2);
        menu.IniciarHud(OnInitialMenuOptionSelect, BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.opcoesDeMenu).ToArray());
    }
}
