using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField]
    private CameraController camera;

    [SerializeField]
    private GameObject board;

    [SerializeField]
    private Vector3 cameraGoalPosition;
    [SerializeField]
    private Vector3 cameraGoalRotation;

    private void OnMouseDown()
    {
        board.SetActive(true);
        camera.Move(cameraGoalPosition, cameraGoalRotation);
    }
}
