using UnityEngine;

public class PopOut : MonoBehaviour
{
    public GameObject winningCanvas;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the ball touched the goal trigger
        if (other.CompareTag("Ball"))
        {
            ShowWinningCanvas();
        }
    }

    private void ShowWinningCanvas()
    {
        winningCanvas.SetActive(true);
    }
}
