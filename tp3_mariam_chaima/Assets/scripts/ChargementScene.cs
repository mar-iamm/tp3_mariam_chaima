using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChargementScene : MonoBehaviour
{
    public Animator canvas;

    public void OnPress()
    {
        StartCoroutine("chargerNiveau");
    }

    IEnumerator chargerNiveau()
    {
        canvas.SetTrigger("Debut");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(1);

        yield break;
    }
}