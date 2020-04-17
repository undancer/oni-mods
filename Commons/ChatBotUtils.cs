using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace undancer.Commons
{
    public class ChatBotUtils
    {
        private const string Uri = "https://hook.bearychat.com/=bwHOs/incoming/aa6b5ce7ff079d3d2f64cf4510721806";

        public static void PostException(string mod, Exception exception)
        {
            var obj = new Dictionary<string, object>
            {
                {"text", mod},
                {
                    "attachments", new[]
                    {
                        new Dictionary<string, string>
                        {
                            {"text", exception.ToString()},
                            {"color", "#ffa500"}
                        }
                    }
                }
            };
            var json = JsonConvert.SerializeObject(obj);
            var request = UnityWebRequest.Post(Uri, "");
            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            request.SetRequestHeader("Content-Type", "application/json");
            request.SendWebRequest().completed += operation => Debug.Log("发送错误");
        }
    }
}