using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
[ExecuteInEditMode]
public class CameraSynchronizer : MonoBehaviour
{
    public Camera mainCamera;
    [SerializeField, EditorSetter("EnableSynchronize")]
    private bool enableSynchronize = false;

    public bool copyCameraConfig = false;
    public bool EnableSynchronize
    {
        set
        {
            enableSynchronize = value;
            if (copyCameraConfig)
                mainCamera.CopyFrom(SceneView.lastActiveSceneView.camera);
        }
    }
    [ShowAsFunction("Synchronize to Current Camera", "Synchronize2CurrentCamera")]
    public bool synchronize2CurrentCamera;

    [ShowAsFunction("Synchronize to Current View", "Synchronize2CurrentView")]
    public bool synchronize2CurrentView;

    public void Synchronize2CurrentCamera()
    {
        if (copyCameraConfig)
            SceneView.lastActiveSceneView.camera.CopyFrom(mainCamera);
        SceneView.lastActiveSceneView.AlignViewToObject(mainCamera.transform);
    }
    public void Synchronize2CurrentView()
    {
        if (mainCamera)
        {
            if (copyCameraConfig)
                mainCamera.CopyFrom(SceneView.lastActiveSceneView.camera);
            mainCamera.transform.position = SceneView.lastActiveSceneView.camera.transform.position;
            mainCamera.transform.rotation = SceneView.lastActiveSceneView.camera.transform.rotation;
        }
    }


    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        enabled = !EditorApplication.isPlaying;
    }




    private void Update()
    {
        if (enableSynchronize)
        {

            Synchronize2CurrentView();
        }
    }
}
#endif