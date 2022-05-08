using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] Animator transitionAnimator;
    [SerializeField] AnimationClip transitionAnimationEnd;

    void OnEnable()
    {
        transitionAnimator.SetTrigger("End");
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionAnimationEnd.length);
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadSpecificLevel(int levelIndex)
    {
        StartCoroutine(LoadSpecific(levelIndex));
    }

    IEnumerator LoadSpecific(int levelIndex)
    {
        transitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionAnimationEnd.length);
        SceneManager.LoadScene(levelIndex);
    }
}
