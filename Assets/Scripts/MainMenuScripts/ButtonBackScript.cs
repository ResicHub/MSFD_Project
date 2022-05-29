using System.Collections;
using UnityEngine;

public class ButtonBackScript : MonoBehaviour
{
    [SerializeField]
    private CameraController camera;

    [SerializeField]
    private GameObject board;

    private void OnMouseDown()
    {
        camera.MoveBack(camera.transform.position, camera.transform.rotation);
        StartCoroutine(HideCoroutine());
    }

    private IEnumerator HideCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        board.SetActive(false);
    }
}
