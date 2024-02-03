using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CGPTConstants 
{
    public static string CHAT_GPT_TEXT_URL = "https://api.openai.com/v1/completions"; // as of Feb 2024 this api link works 
    public static string CHAT_GPT_TEXT_DAVINCI_CODEX_URL = "https://api.openai.com/v1/engines/davinci-codex/completions"; // might be depricated
    public static string DAVINCI_TEXT_MODEL = "davinci-002";
    public static string BABBAGE_TEXT_MODEL = "babbage-002"; 
    public static string GPT_TURBO_INSTRUCT = "gpt-3.5-turbo-instruct"; // better than the above two in terms of the format of the repsonse we want for this project
}
