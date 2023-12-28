using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Loadingscript : MonoBehaviour
{
    public Image FillImage;
    float time, second;
    public AudioSource logoSound;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
      //  animator.SetTrigger("GETUP");
    }
    private void Awake()
    {
        logoSound = GetComponent<AudioSource>();
       
        logoSound.Play();
        second = 5;
        Invoke("LoadGame", 5f);
    }
    // Update is called once per frame
    void Update()
    {
        if (time < 5)
        {
            time += Time.deltaTime;
            FillImage.fillAmount = time / second;
        }
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
