using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Unity.VisualScripting;
using System.Collections.Generic;

public class Url_Request : MonoBehaviour
{

    public string var_result = "";
    private List<string> HeaderList = new List<string>();

    GameObject MacroUrl;
    private void Awake()
    {
        MacroUrl = FindObjectOfType<Url_Request>().gameObject;
    }

    void Start()
    {
        var_result = "";
    }

    public void AddSetHeader(string name, string type)
    {

        HeaderList.Add(name);
        HeaderList.Add(type);

    }

    public void RequestGetMethod(string url)
    {
        var_result = "";
        StartCoroutine(GetRequest(url));
        
        IEnumerator GetRequest(string uri)
        {
            
            
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                int total_ = HeaderList.Count;
                var name_ = "";
                var type_ = "";

                if (total_ > 0)
                {
                    for (int i = 0; i < total_; i++)
                    {
                        //Debug.Log(HeaderList[i]);
                        if ( (i % 2) == 0)
                        {
                            name_ = HeaderList[i];
                        }
                        else
                        {
                            type_ = HeaderList[i];
                            webRequest.SetRequestHeader(""+ name_, ""+ type_);
                        }
                    }
                }
                

                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                string[] pages = uri.Split('/');
                int page = pages.Length - 1;
                
                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                        var_result = "Error";
                        Debug.Log("Error : Connection Error");
                        
                        CustomEvent.Trigger(MacroUrl, "Function_Url_Get_Return");
                        break;
                    case UnityWebRequest.Result.DataProcessingError:
                        var_result = "Error";
                        Debug.Log(pages[page] + ": Error: " + webRequest.error);
                        
                        CustomEvent.Trigger(MacroUrl, "Function_Url_Get_Return");
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        var_result = "Error";
                        Debug.Log(pages[page] + ": HTTP Error: " + webRequest.error);
                        
                        CustomEvent.Trigger(MacroUrl, "Function_Url_Get_Return");
                        break;
                    case UnityWebRequest.Result.Success:
                        var_result = webRequest.downloadHandler.text;
                        if (var_result != "")
                        {
                            CustomEvent.Trigger(MacroUrl, "Function_Url_Get_Return");
                        }
                        break;


                }
            }
        }

        HeaderList.Clear();
    }

    public void RequestDeleteMethod(string url)
    {

        var_result = "";
        StartCoroutine(DeleteRequest(url));

        IEnumerator DeleteRequest(string uri)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Delete(uri))
            {
                int total_ = HeaderList.Count;
                var name_ = "";
                var type_ = "";

                if (total_ > 0)
                {
                    for (int i = 0; i < total_; i++)
                    {
                        //Debug.Log(HeaderList[i]);
                        if ((i % 2) == 0)
                        {
                            name_ = HeaderList[i];
                        }
                        else
                        {
                            type_ = HeaderList[i];
                            webRequest.SetRequestHeader("" + name_, "" + type_);
                        }
                    }
                }


                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                string[] pages = uri.Split('/');
                int page = pages.Length - 1;

                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                        var_result = "Error";
                        Debug.Log("Error : Connection Error");
                        
                        CustomEvent.Trigger(MacroUrl, "Function_Url_Delete_Return");
                        break;
                    case UnityWebRequest.Result.DataProcessingError:
                        var_result = "Error";
                        Debug.Log(pages[page] + ": Error: " + webRequest.error);
                        
                        CustomEvent.Trigger(MacroUrl, "Function_Url_Delete_Return");
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        var_result = "Error";
                        Debug.Log(pages[page] + ": HTTP Error: " + webRequest.error);
                        
                        CustomEvent.Trigger(MacroUrl, "Function_Url_Delete_Return");
                        break;
                    case UnityWebRequest.Result.Success:
                        var_result = "Success";
                        
                        CustomEvent.Trigger(MacroUrl, "Function_Url_Delete_Return");
                        break;


                }
            }
        }

        HeaderList.Clear();
    }


    public void RequestPatchMethod(string url,string data)
    {
        var_result = "";
        StartCoroutine(WebPatchRequest(url, data));

        IEnumerator WebPatchRequest(string uri,string data)
        {
            string url = uri;
            
            byte[] requestBodyData = System.Text.Encoding.UTF8.GetBytes(data);

            using (UnityWebRequest webRequest = UnityWebRequest.Put(url, requestBodyData))
            {
                webRequest.method = "PATCH";

                int total_ = HeaderList.Count;
                var name_ = "";
                var type_ = "";

                if (total_ > 0)
                {
                    for (int i = 0; i < total_; i++)
                    {
                        if ((i % 2) == 0)
                        {
                            name_ = HeaderList[i];
                        }
                        else
                        {
                            type_ = HeaderList[i];
                            webRequest.SetRequestHeader("" + name_, "" + type_);
                        }
                    }
                }


                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                string[] pages = uri.Split('/');
                int page = pages.Length - 1;

                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                        var_result = "Error";
                        Debug.Log("Error : Connection Error");
                        
                        CustomEvent.Trigger(MacroUrl, "Function_Url_Patch_Return");
                        break;
                    case UnityWebRequest.Result.DataProcessingError:
                        var_result = "Error";
                        Debug.Log(pages[page] + ": Error: " + webRequest.error);
                        
                        CustomEvent.Trigger(MacroUrl, "Function_Url_Patch_Return");
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        var_result = "Error";
                        Debug.Log(pages[page] + ": HTTP Error: " + webRequest.error);
                        
                        CustomEvent.Trigger(MacroUrl, "Function_Url_Patch_Return");
                        break;
                    case UnityWebRequest.Result.Success:
                        var_result = "Success";
                        
                        CustomEvent.Trigger(MacroUrl, "Function_Url_Patch_Return");
                        break;


                }

            }
            
        }
        HeaderList.Clear();
    }

    public void RequestPostMethod(string url, string data)
    {
        var_result = "";
        StartCoroutine(CallPostRequest(url, data));

        IEnumerator CallPostRequest(string uri, string data)
        {
            string url = uri;
             
            byte[] requestBodyData = System.Text.Encoding.UTF8.GetBytes(data);

            using (UnityWebRequest webRequest = UnityWebRequest.Put(url, requestBodyData))
            {
                webRequest.method = "POST";

                int total_ = HeaderList.Count;
                var name_ = "";
                var type_ = "";

                if (total_ > 0)
                {
                    for (int i = 0; i < total_; i++)
                    {
                        if ((i % 2) == 0)
                        {
                            name_ = HeaderList[i];
                        }
                        else
                        {
                            type_ = HeaderList[i];
                            webRequest.SetRequestHeader("" + name_, "" + type_);
                        }
                    }
                }


                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                string[] pages = uri.Split('/');
                int page = pages.Length - 1;

                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                        var_result = "Error";
                        Debug.Log("Error : Connection Error");
                        CustomEvent.Trigger(MacroUrl, "Function_Url_Post_Return");
                        break;
                    case UnityWebRequest.Result.DataProcessingError:
                        var_result = "Error";
                        Debug.Log(pages[page] + ": Error: " + webRequest.error);
                        
                        CustomEvent.Trigger(MacroUrl, "Function_Url_Post_Return");
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        var_result = "Error";
                        Debug.Log(pages[page] + ": HTTP Error: " + webRequest.error);
                        
                        CustomEvent.Trigger(MacroUrl, "Function_Url_Post_Return");
                        break;
                    case UnityWebRequest.Result.Success:
                        var_result = "Success";
                        
                        CustomEvent.Trigger(MacroUrl, "Function_Url_Post_Return");
                        break;


                }

            }

        }
        HeaderList.Clear();
    }
}
