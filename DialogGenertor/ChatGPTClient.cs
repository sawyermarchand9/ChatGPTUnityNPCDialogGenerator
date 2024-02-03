using UnityEngine;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;

public class ChatGPTClient
{
    private const string apiKey = "YOUR API KEY GOES HERE";
    private const string apiUrl = "https://api.openai.com/v1/completions";
    public string jsonResponse {get; set;}

    public ChatGPTClient(){
    }

    public async Task ExecuteTextQuery(string message)
    {
        await QueryTextGPT(message);
    }

    /** 
    This method contains the networking code to post a question to the Chat GPT Text api 
    Developer can change model to get a different response, however GPT_TURBO_INSTRUCT seems to work better for this extensions purpose
    **/
    private async Task QueryTextGPT(string message)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
    
            var requestData = new ChatGptRequest
            {
                model = CGPTConstants.GPT_TURBO_INSTRUCT,
                prompt = message,
                max_tokens = 100
            };
    
            try
            {
                var jsonRequest = JsonUtility.ToJson(requestData);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                var status = response.StatusCode;

                if (response.IsSuccessStatusCode)
                {
                    jsonResponse = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Debug.LogError($"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Exception: {ex.Message}");
            }
        }
    }
}

/** 
    below are some small models for putting together text request 
**/ 
[System.Serializable]
public class ChatGptRequest
{
    public string prompt;
    public string model;
    public int max_tokens;
}

[System.Serializable]
public class Message
{
    public string role;
    public string content;
}
