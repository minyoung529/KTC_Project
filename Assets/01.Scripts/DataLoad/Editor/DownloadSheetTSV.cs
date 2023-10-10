using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Unity.EditorCoroutines.Editor;
using System;

public class DownloadSheetTSV
{
    public void Download(SpreadInformation sheetInfo, Action<string> onSuccess, Action onFail)
    {
        EditorCoroutineUtility.StartCoroutine(DownloadCoroutine(sheetInfo, onSuccess, onFail), this);
    }

    private IEnumerator DownloadCoroutine(SpreadInformation sheetInfo, Action<string> onSuccess, Action onFail)
    {
        string url = sheetInfo.GetAddress();

        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            onSuccess?.Invoke(www.downloadHandler.text);
        }
        else
        {
            onFail?.Invoke();
        }
    }
}
