using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;


namespace Patrick
{


    public class HuggingFaceManager : MonoBehaviour
    {
        private NPCController npc;
        private string url = "https://api-inference.huggingface.co/models/sentence-transformers/all-MiniLM-L6-v2";
        private string key = "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";

        private TMP_InputField inputField;
        private string prompt;
        private string role = "你是一位教導簡易英文的老師，並且用淺顯易懂的方法回答問題。";
        private string inputText;
        private string[] npcSentences;

        [SerializeField, Header("NPC 物件")]
        private NPCController NPC;

        public global::System.String Url { get => Url1; set => Url1 = value; }
        public global::System.String Url1 { get => url; set => url = value; }
        public global::System.String Url2 { get => url; set => url = value; }

        private void waiting ()
        {
            inputField = GameObject.Find("input words").GetComponent<TMP_InputField>();

            inputField.onEndEdit.AddListener(PlayerInput);

            npcSentences = npc.data.sentences;

        }

        private void PlayerInput(string input)
        {
            print($"<color=#3f3>{input}</color>");
            prompt = input;
            StartCoroutine(GetResult());
        }

        private IEnumerator GetResult()
        {
            var inputs = new

            {
                source_sentence = inputText,
                sentences = npcSentences
            };

            string json = JsonConvert.SerializeObject(inputs);
            byte[] postData = Encoding.UTF8.GetBytes(json);
            UnityWebRequest request = new UnityWebRequest(Url1, "POST");
            request.uploadHandler = new UploadHandlerRaw(postData);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + key);

            yield return request.SendWebRequest();

            print(request.result);

            if (request.result != UnityWebRequest.Result.Success)
            {
                print($"<color=#3f3>要求失敗：{request.error}</color>");
            }
            else
            {
                string responeText = request.downloadHandler.text;
                var respone = JsonConvert.DeserializeObject<List<string>>(responeText);
                print($"<color=#3f3>分數：{responeText}</color>");

            if (respone != null && respone.Count > 0)
                {
                    int best = respone.Select((value, index) => new
                    {
                        Value = value,
                        Index = index
                    }).OrderByDescending(x => x.Value).First().Index;
                    print($"<color=#3f3>最佳答案：{npcSentences[best]}</color>");

                    npc.PlayAnimation(best);
                }
            }
        }
    }
}
