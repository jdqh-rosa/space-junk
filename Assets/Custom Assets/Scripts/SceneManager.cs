using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public Animator Transition;
    public float TransitionTime = 1;

    /// <summary>
    /// Public function for loading a level with a transition
    /// </summary>
    /// <param name="scene"></param>
    public void LoadLevel(int scene)
    {
        StartCoroutine(startTransition(scene));
    }

    /// <summary>
    /// Function to start a transition that will load the next scene after X amount of time
    /// </summary>
    /// <param name="index">Index of the scene to be loaded</param>
    /// <returns>New scene</returns>
    private IEnumerator startTransition(int index)
    {
        Transition.SetTrigger("Start");

        yield return new WaitForSeconds(TransitionTime);

        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }
}
