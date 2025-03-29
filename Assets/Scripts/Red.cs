
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class Red : MonoBehaviour
{

    private TextField tfNombreUsuario;
    private TextField tfContraseña;

    private Label textError;
    public struct DatosUsuario
    {
        public string nombreUsuario;
        public string contraseña;

    }

    void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        tfNombreUsuario = root.Q<TextField>("Usuario");
        
        tfContraseña = root.Q<TextField>("Contrasena");

        textError = root.Q<Label>("Error");

        Button botonEnviar = root.Q<Button>("Enviar");

        botonEnviar.clicked += EnviarDatosJSON;
    }

    private void EnviarDatosJSON()
    {
        StartCoroutine(SubirDatosJSON());
    }

    private IEnumerator SubirDatosJSON()
    {
        DatosUsuario datos;
        datos.nombreUsuario = tfNombreUsuario.value;
        datos.contraseña = tfContraseña.value;

        string datosJSON = JsonUtility.ToJson(datos);
        print(datosJSON);

        UnityWebRequest request = UnityWebRequest.Post("http://10.48.87.168:3000", datosJSON, "application/json");
        yield return request.SendWebRequest();

        //DESPUES DE QUE SE DESCARGA EL ARCHIVO Y PASA UN TIEMPO
        if (request.result == UnityWebRequest.Result.Success)
        {
            print("Correcto");
            SceneManager.LoadScene("Menu");
        }
        else
        {
            textError.text = "Error de conexión: "+request.responseCode;
            print("Error" + request.responseCode);
        }
    }

}
