using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    private UIDocument menu;
    private Button botonA;
    private Button botonB;

    void OnEnable(){
        menu = GetComponent<UIDocument>();
        var root = menu.rootVisualElement;
        botonA = root.Q<Button>("BotonA");
        botonB = root.Q<Button>("BotonB");

        //Callbacks
        botonA.RegisterCallback<ClickEvent, String>(IniciarJuego, "SampleScene");
        botonB.RegisterCallback<ClickEvent, String>(IniciarJuego, "EscenaMapa");
    }

    private void IniciarJuego(ClickEvent evt, String escena)
    {
        SceneManager.LoadScene(escena);
    }


}
