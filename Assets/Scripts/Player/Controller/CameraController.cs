using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject mesh3rdFace;
    [SerializeField] GameObject mesh3rdHair;
    
    public CinemachineVirtualCamera _3rdPersonPos;
    public CinemachineVirtualCamera _1rdPersonPos;

    bool isFPS = false;

    void Start()
    {
        SetActiveCamera(true);
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Mouse1))
        // {
        //     isFPS = !isFPS;
        //     SetActiveCamera(isFPS);
        // }
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isFPS = !isFPS;
            SetActiveCamera(isFPS);
        }
    }

    void SetActiveCamera(bool activate)
    {
        if (activate)
        {
            _3rdPersonPos.Priority = 20;
            _1rdPersonPos.Priority = 10;
            
            mesh3rdFace.SetActive(true);
            mesh3rdHair.SetActive(true);
        }
        else
        {
            _3rdPersonPos.Priority = 10;
            _1rdPersonPos.Priority = 20;
            
            mesh3rdFace.SetActive(false);
            mesh3rdHair.SetActive(false);
        }
    }
}