using B_Extensions;
using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Services
{
    public class FirestoreService : Singleton<FirestoreService>
    {
        private const string ProjectId = "nwi056-cabezazo";
        private const string ApiKey = "AIzaSyCp9clRlOO3GIOHNOBop-lRrFZNyzonhqc";
        private const string CollectionName = "players";

        private static readonly string BaseUrl = $"https://firestore.googleapis.com/v1/projects/{ProjectId}/databases/(default)/documents";

        public event Action<Services.PlayerData> OnDataSent;
        public event Action<string> OnError;

        public void SendPlayerData(Services.PlayerData playerData)
        {
            StartCoroutine(SendPlayerDataCoroutine(playerData));
        }

        private IEnumerator SendPlayerDataCoroutine(Services.PlayerData playerData)
        {
            string url = $"{BaseUrl}/{CollectionName}?key={ApiKey}";
            string json = BuildFirestoreJson(playerData);

            using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
            {
                byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");

                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log($"[Firestore] PlayerData sent successfully: {playerData.nombre}");
                    OnDataSent?.Invoke(playerData);
                }
                else
                {
                    string errorMsg = $"[Firestore] Failed to send data: {request.error}\nResponse: {request.downloadHandler.text}";
                    Debug.LogError(errorMsg);
                    OnError?.Invoke(request.error);
                }
            }
        }

        private string BuildFirestoreJson(Services.PlayerData playerData)
        {
            string fechaIso = playerData.fechaRegistro.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            return $"{{" +
                $"\"fields\":{{" +
                    $"\"uid\":{{\"stringValue\":\"{EscapeJson(playerData.uid)}\"}}," +
                    $"\"nombre\":{{\"stringValue\":\"{EscapeJson(playerData.nombre)}\"}}," +
                    $"\"correo\":{{\"stringValue\":\"{EscapeJson(playerData.correo)}\"}}," +
                    $"\"telefono\":{{\"stringValue\":\"{EscapeJson(playerData.telefono)}\"}}," +
                    $"\"score\":{{\"integerValue\":{playerData.score}}}," +
                    $"\"tiempo\":{{\"doubleValue\":{playerData.tiempo.ToString(System.Globalization.CultureInfo.InvariantCulture)}}}," +
                    $"\"fechaRegistro\":{{\"timestampValue\":\"{fechaIso}\"}}" +
                $"}}" +
            $"}}";
        }

        private string EscapeJson(string value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            return value.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t");
        }
    }
}
