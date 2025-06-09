using System.Collections;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public Image panelImage; // Le panel noir
    public TextMeshProUGUI text; // Le texte à faire apparaître
    public float fadeDuration = 2f; // Durée du fade in

    public AudioSource audioSource;
    public AudioClip EndSong;

    void Start()
    {
        // Commencer complètement transparents
        SetAlpha(0f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.ActivePlayer.drilling = false;
        GameManager.Instance.ActivePlayer.Ending = true;
        RythmManager.Instance.gameObject.transform.parent.gameObject.SetActive(false);
        GameManager.Instance.ActivePlayer.Impulsion = 3f;
        GameManager.Instance.ActivePlayer.ImpulsionReduction = 0.005f;
        StartCoroutine(FadeIn());
        audioSource.PlayOneShot(EndSong, 0.1f);
    }

    void SetAlpha(float alpha)
    {
        if (panelImage != null)
        {
            Color c = panelImage.color;
            c.a = alpha;
            panelImage.color = c;
        }

        if (text != null)
        {
            Color c = text.color;
            c.a = alpha;
            text.color = c;
        }
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(2.0f);

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            float alpha = elapsed / fadeDuration;
            SetAlpha(alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        SetAlpha(1f); // Assure que tout est bien visible à la fin

        yield return new WaitForSeconds(8.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}

