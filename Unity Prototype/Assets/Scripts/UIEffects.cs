using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Handles effects and animations for the UI such as the buttons
/// </summary>
public class UIEffects : MonoBehaviour
{
    public GameObject exitButton;
    public GameObject scanButton;

    private Color transparent = new Color(255, 255, 255, 0);
    private Color vivid = new Color(255, 255, 255, 255);
    /// <summary>
    /// Exits AR Mode.
    /// </summary>
    public void Exit()
    {
        exitButton.GetComponent<Button>().image.color = transparent;
        exitButton.SetActive(true);

        exitButton.GetComponent<Animator>().enabled = true;
        exitButton.GetComponent<Animator>().SetBool("Exit", false);

        scanButton.GetComponent<Animator>().enabled = true;
        scanButton.GetComponent<Animator>().SetBool("Exit", true);

        Invoke("RevertExit", 2);
    }

    /// <summary>
    ///  Goes back into AR Mode
    /// </summary>
    public void RevertExit()
    {
        exitButton.GetComponent<Animator>().enabled = false;
        scanButton.GetComponent<Animator>().enabled = false;

        exitButton.GetComponent<Button>().image.color = vivid;

        scanButton.SetActive(false);
    }

    /// <summary>
    /// Activate Scan UI
    /// </summary>
    public void Scan()
    {
        scanButton.GetComponent<Button>().image.color = new Color(0, 0, 0, 255);
        scanButton.SetActive(true);

        scanButton.GetComponent<Animator>().enabled = true;
        scanButton.GetComponent<Animator>().SetBool("Exit", false);

        exitButton.GetComponent<Animator>().enabled = true;
        exitButton.GetComponent<Animator>().SetBool("Exit", true);

        Invoke("RevertScan", 2);
    }

    /// <summary>
    /// Deactivate Scan UI
    /// </summary>
    public void RevertScan()
    {
        exitButton.GetComponent<Animator>().enabled = false;
        scanButton.GetComponent<Animator>().enabled = false;

        scanButton.GetComponent<Button>().image.color = new Color(0,0,0,255);

        exitButton.SetActive(false);
    }
}
