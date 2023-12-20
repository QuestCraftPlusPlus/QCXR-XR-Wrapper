using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
public class LoginText : MonoBehaviour
{
    string code = "";

    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = JNIStorage.apiClass.GetStatic<string>("msaMessage");
        if(GetComponent<TextMeshProUGUI>().text != "")
        {
	    string message = JNIStorage.apiClass.GetStatic<string>("msaMessage");

            Match match = Regex.Match(message, "enter the code (\\w+)", RegexOptions.IgnoreCase);

            if (match.Success)
            {
                code = match.Groups[1].Value;
	 	        GetComponent<TextMeshProUGUI>().text  = GetComponent<TextMeshProUGUI>().text + "Complete to continue. To use on device authentication, please press 'Sign In' again.";
            }
            else 
	        {
		        code = "";
		        GetComponent<TextMeshProUGUI>().text  = GetComponent<TextMeshProUGUI>().text + "Complete to continue.";
            }
        }
    }

    public string getMsaCode(){
    	return code;
    }
}
