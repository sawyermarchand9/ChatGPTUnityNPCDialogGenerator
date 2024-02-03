using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.ShaderGraph.Serialization;
using System.Runtime.InteropServices;
using Codice.Client.Common.GameUI;
using UnityEditor;
using System;

public class CGPTResponseHandler
{
    private string chatGPTResponse;
    private string text;

    public CGPTResponseHandler(string chatGPTResponse){
        this.chatGPTResponse = chatGPTResponse;
        formatDialog();
    }

    // this method will get the text object off of the choices
    private void formatDialog(){
        JObject jsonObject = JObject.Parse(chatGPTResponse);
        JArray choices = (JArray) jsonObject.GetValue("choices");
        text = (string) choices[0]["text"];
    }

    // handles creation and updating of character dialog json file
    public void handleCharacterDialog(string characterName){
        handleDialogFolder();
        string basePath = "Assets/NPCDialog/";
        string path = basePath+characterName+".json";
        if(File.Exists(path))
        {
            updateCharacterFile(path);
        }else{
            createNewCharacterFile(path);
        }

    }

    private void updateCharacterFile(string path)
    {
        Debug.Log("File exists . . .");
        string characterString = File.ReadAllText(path);
        JObject character = JObject.Parse(characterString);
        JArray dialogs = (JArray) character["Dialogs"];
        dialogs.Add(text);
        Debug.Log(character);
        string jsonString = JsonConvert.SerializeObject(character);
        File.WriteAllText(path, jsonString);
    }

    private void createNewCharacterFile(string path)
    {
        JArray jArray = new JArray{text};
        JObject jObject = new JObject();
        jObject["Dialogs"] = jArray;
        string jsonString = JsonConvert.SerializeObject(jObject);
        File.WriteAllText(path, jsonString);
        Debug.Log(jsonString);
        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
    }

    private void handleDialogFolder()
    {
        string path = "Assets/NPCDialog/";

        if(!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

}
