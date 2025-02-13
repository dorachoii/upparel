using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoginController : MonoBehaviour
{
    public void OnLoginButtonPressed()
    {
        // Scene 2로 전환
        SceneManager.LoadScene("Scene2");
    }
}
