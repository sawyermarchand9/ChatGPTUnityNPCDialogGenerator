using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Threading.Tasks;
using System.IO;
public class DialogGenerator : EditorWindow
{
    private string npcName;
    private string emotion;
    private string occupation;
    private string message = "";

    [MenuItem("Tools/NPC Dialog Generator")]
    public static void ShowWindow()
    {
        GetWindow(typeof(DialogGenerator));
    }


    private void OnGUI()
    {
        GUILayout.Space(10);
        GUILayout.Label("Generate NPC Dialog", EditorStyles.boldLabel);
        npcName = EditorGUILayout.TextField("NPC Name", npcName);
        GUILayout.Space(10);
        GUILayout.Label("Context Feilds", EditorStyles.boldLabel);
        emotion = EditorGUILayout.TextField("Emotional state", emotion);
        occupation = EditorGUILayout.TextField("Ocupation", occupation);
        GUILayout.Space(10);

        if(GUILayout.Button("Generate NPC Dialog"))
        {
            message = $"I am working on a video game character named {npcName}. "+
            $"They are a {occupation} and they currently feel {emotion}. "+
            "could you generate a greeting that {npcName} would say to the player?";
            Task.Run(async ()=>{
                ChatGPTClient client = new ChatGPTClient();
                await client.ExecuteTextQuery(message);
                CGPTResponseHandler handler = new CGPTResponseHandler(client.jsonResponse);
                handler.handleCharacterDialog(npcName);
                AssetDatabase.Refresh();
            });
        }

        if(GUILayout.Button("Refresh Assets Folder"))
        {
            // optional to use after running generate button
            AssetDatabase.Refresh();
        }

    }
}
