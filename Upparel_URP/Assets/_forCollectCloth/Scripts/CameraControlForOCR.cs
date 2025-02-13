using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using TMPro;
using System;
using System.Collections.Generic;

public class CameraControllerForOCR : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI ocrResultText;

    [SerializeField]
    private GameObject loadingView; // Reference to the loading view GameObject

    public bool IsSessionComplete { get; private set; } = false; // Flag to indicate completion

    // Method to open camera session
    public void OpenCameraSession()
    {
        IsSessionComplete = false; // Reset the flag

        // Check permission
        NativeCamera.Permission permission = NativeCamera.CheckPermission(true);

        if (permission == NativeCamera.Permission.ShouldAsk)
        {
            permission = NativeCamera.RequestPermission(true);
        }

        if (permission == NativeCamera.Permission.Granted)
        {
            NativeCamera.TakePicture((path) =>
            {
                if (path != null)
                {
                    // Load the image from the specified path
                    Texture2D capturedTexture = NativeCamera.LoadImageAtPath(path, 1024, false);
                    if (capturedTexture == null)
                    {
                        Debug.LogError("Couldn't load texture from " + path);
                        IsSessionComplete = true;
                        return;
                    }

                    // Start the OCR process
                    //스택 여기 봐주시면 됩니다
                    //StartCoroutine(SendImageForOCR(capturedTexture));

                    //스택에게 넘겨주기 전 test용 코드 (OCR 완료되면 여기 지워주시면 됨)
                    StartCoroutine(SendImageForOCR(capturedTexture,(fabrics)=> { 
                        //인수로 들어오는 fabrics활용하기
                        //델리게이트
                    }));
                }
                else
                {
                    // User canceled the camera
                    IsSessionComplete = true;
                }
            }, maxSize: 1024);
        }
        else
        {
            Debug.Log("Camera permission not granted");
            IsSessionComplete = true;
        }
    }



    //스택에게 넘겨주기 전 test용 코드
    private IEnumerator TestAwaitImage()
    {
        // Show the loading view
        loadingView.SetActive(true);

        yield return new WaitForSeconds(3);

        // Simulate an OCR result
        string simulatedOCRResult = "예시 텍스트 확인";

        // Update the UI Text with the simulated result
        if (ocrResultText != null)
        {
            ocrResultText.text = simulatedOCRResult;
        }


        loadingView.SetActive(false);

        // Set the flag to indicate completion
        IsSessionComplete = true;
    }

    // Coroutine to send the image to the backend for OCR (스택 여기에요!!)
    private IEnumerator SendImageForOCR(Texture2D tex,Action<Fabrics> action) 
    {
        byte[] png = tex.EncodeToPNG();
        string content = Convert.ToBase64String(png);

        OCR ocr = new OCR();
        List<Feature> features = new List<Feature>();
        features.Add(new Feature("DOCUMENT_TEXT_DETECTION"));
        ocr.SetAndAdd(new Images(content), features);


        HttpRequest req = new HttpRequestBuilder()
            .ReqHandler((www) => {
                string text = www.downloadHandler.text;
                GoogleVisionResponse response = JsonUtility.FromJson<GoogleVisionResponse>(text);
                string ocrText = response.responses[0].textAnnotations[0].description;
                ocrText = ocrText.Replace("\n", " ");
                ocr.ocrText = ocrText;
                print("Google Vision API\n" + ocrText);
            })
            .FailureHandler(() => {

            })
            .build();

        HttpRequest req2 = new HttpRequestBuilder()
        .ReqHandler((www) => {
            ChatCompletionResponse res = JsonUtility.FromJson<ChatCompletionResponse>(www.downloadHandler.text);
            print(res.choices[0].message.content);
            string json = res.choices[0].message.content;

            print("GPT response\n" + json);
            Fabrics fabrics = JsonUtility.FromJson<Fabrics>(json);
            action?.Invoke(fabrics);

            string gptResultText = FormatFabricsToString(fabrics);

            // Update the result in the TextMeshProUGUI component (ocrResultText) after GPT processing
            //string gptResultText = FormatFabricsToString(fabrics);

            ocrResultText.text = gptResultText;  // Set the final processed text to the UI

            // Force a refresh in case the text isn't updated immediately
            ocrResultText.ForceMeshUpdate();

            //분석된 데이터가 들어옵니당.
            //fabrics 를 이용해 지지고 볶고 맛보고!
        })
        .build();



        //로딩 시작 코드 작성 
        //로딩 시작.....
        loadingView.SetActive(true);

        yield return OCR_REQ.GetOCR(ocr, req);

        List<Message> messages = new List<Message>();
        messages.Add(new Message("user", ocr.ocrText));

        yield return OCRAI.GetGPT(messages, req2);

        //로딩 종료 코드 작성
        //로딩 종료...

        loadingView.SetActive(false);

        // Set the flag to indicate completion
        IsSessionComplete = true;


    }


    //Helper method to format Fabrics data into a readable string
    private string FormatFabricsToString(Fabrics fabrics)
    {
        string result = "";

        foreach (Fabric fabric in fabrics.fabrics)
        {
            result += $"{fabric.position} {fabric.name} {fabric.value}\n";
        }

        Debug.Log("Formatted fabrics: " + result);  // Log the final formatted string
        return result.Trim();  // Trim any extra spaces or newlines
    }

}
