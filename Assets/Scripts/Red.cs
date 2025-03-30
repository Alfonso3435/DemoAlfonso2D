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
    private string usuarioActual;

    [Serializable]
    public struct DatosUsuario
    {
        public string nombreUsuario;
        public string contraseña;
    }

    [Serializable]
    public struct RespuestaServidor
    {
        public bool success;
        public string mensaje;
        public string horaInicio;
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
        DatosUsuario datos = new DatosUsuario
        {
            nombreUsuario = tfNombreUsuario.value,
            contraseña = tfContraseña.value
        };

        string datosJSON = JsonUtility.ToJson(datos);
        UnityWebRequest request = new UnityWebRequest("http://192.168.0.104:3000/unity/recibeJSON", "POST");
        
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(datosJSON);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            RespuestaServidor respuesta = JsonUtility.FromJson<RespuestaServidor>(request.downloadHandler.text);

            if (respuesta.success)
            {
                usuarioActual = datos.nombreUsuario;
                SceneManager.LoadScene("EscenaMenu");
            }
            else
            {
                textError.text = respuesta.mensaje;
            }
        }
        else
        {
            textError.text = "Error de conexión: " + request.responseCode;
        }
    }

    private IEnumerator FinalizarSesion()
{
    DatosUsuario datos = new DatosUsuario
    {
        nombreUsuario = usuarioActual
    };

    string datosJSON = JsonUtility.ToJson(datos);
    UnityWebRequest request = new UnityWebRequest("http://192.168.0.104:3000/unity/finalizarSesion", "POST");

    byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(datosJSON);
    request.uploadHandler = new UploadHandlerRaw(bodyRaw);
    request.downloadHandler = new DownloadHandlerBuffer();
    request.SetRequestHeader("Content-Type", "application/json");

    yield return request.SendWebRequest();

    if (request.result == UnityWebRequest.Result.Success)
    {
        Debug.Log("Sesión finalizada correctamente");
    }
    else
    {
        Debug.LogError("Error al finalizar sesión: " + request.responseCode);
    }
}

void OnApplicationQuit()
{
    StartCoroutine(FinalizarSesion());
}

}
 
