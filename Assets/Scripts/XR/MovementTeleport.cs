using UnityEngine;

public class MovementTeleport : MonoBehaviour
{
    [SerializeField] Transform recticle;
    [SerializeField] GameObject ground;


    private LineRenderer lineRenderer;
    private int teleport;


    public void StartTeleport(LineRenderer line)
    {
        teleport++;
        lineRenderer = line;

        if (teleport == 1) EnableTeleport();
    }
    public void EndTeleport()
    {
        teleport--;
        if (teleport == 0) DisableTeleport();
    }

    private void EnableTeleport()
    {
        ground.SetActive(true);
    }
    private void DisableTeleport()
    {
        ground.SetActive(false);

        if (RecticleIsAcive) { Teleport(recticle.position); }

        SetRecticleActive(false);
    }

    private void SetRecticleActive(bool active) => recticle.gameObject.SetActive(active);
    private bool RecticleIsAcive { get => recticle.gameObject.activeInHierarchy; }

    private void TeleportRay()
    {
        if (lineRenderer == null) return;

        Vector3 lastLinePosition = lineRenderer.GetPosition(lineRenderer.positionCount - 1);

        float lastLinePositionHight = lastLinePosition.y;
        if (lastLinePositionHight < 0.01f && lastLinePositionHight > -0.01f)
        {
            SetRecticleActive(true);
            recticle.position = new Vector3(lastLinePosition.x, 0, lastLinePosition.z);
        }
        else
        {
            SetRecticleActive(false);
        }
    }




    
    [SerializeField] Transform[] physicalHands;
    public bool dash { get; set; }
    private bool teleporing = false;
    private Vector3 teleportPosition;
    private float teleportTime;

    private void Teleport(Vector3 position)
    {
        if (teleporing) return;

        teleportPosition = position;

        if (dash) DashTeleport();
        else BlinkTeleport();

        teleporing = true;
    }

    private void TeleportBody() 
    {
        transform.position = teleportPosition;

        foreach (Transform pHand in physicalHands)
            pHand.position = teleportPosition;
    }





    [Header("")]
    [SerializeField] Transform mainCamera;
    [ReadOnly, SerializeField] Camera mainCamera_camera;
    [ReadOnly, SerializeField] AudioListener mainCamera_AudioListener;

    [SerializeField] Transform dashCamera;
    [ReadOnly, SerializeField] Camera dashCamera_camera;
    [ReadOnly, SerializeField] AudioListener dashCamera_AudioListener;

    [SerializeField] Renderer[] physicalHandRenderers;

    private Vector3 oldMcPosition;

    private void DashTeleport()
    {
        teleportTime = 0.25f;

        SetPhysicalHandRenderersEnabled(false);
        SetDashAndMainCameraEnabled(false);

        oldMcPosition = mainCamera.position;

        dashCamera.rotation = mainCamera.rotation;
        dashCamera.position = mainCamera.position;

        TeleportBody();
    }

    private void DashTeleporting()
    {
        if (teleportTime > 0)
        {
            teleportTime -= Time.deltaTime * 1;

            dashCamera.rotation = mainCamera.rotation;
            dashCamera.position = Vector3.Lerp(mainCamera.position, oldMcPosition, teleportTime * 4);
        }
        else 
        {
            SetPhysicalHandRenderersEnabled(true);
            SetDashAndMainCameraEnabled(true);

            teleporing = false;
        }
    }

    private void SetPhysicalHandRenderersEnabled(bool enabled)
    {
        foreach (Renderer renderer in physicalHandRenderers)
            renderer.enabled = enabled;
    }
    private void SetDashAndMainCameraEnabled(bool mainEnabled)
    {
        mainCamera_AudioListener.enabled = mainEnabled;
        mainCamera_camera.enabled = mainEnabled;
        dashCamera_AudioListener.enabled = !mainEnabled;
        dashCamera_camera.enabled = !mainEnabled;
    }





    [Header("")]
    [SerializeField] MovementTeleportBlinkCanvas blinkCanvas;
    private bool blinkClosing = true;

    private void BlinkTeleport()
    {
        blinkCanvas.gameObject.SetActive(true);
        blinkCanvas.SetAlpha(0);
        blinkClosing = true;
        teleportTime = 0.125f;
    }

    private void BlinkTeleporting()
    {
        teleportTime -= Time.deltaTime * 1;

        if (blinkClosing)
        {
            if (teleportTime > 0)
            {
                float alpha = Mathf.Lerp(1, 0, teleportTime * 8);
                blinkCanvas.SetAlpha(alpha);
            }
            else
            {
                blinkClosing = false;
                teleportTime = 0.125f;
                TeleportBody();
            }
        }
        else
        {
            if (teleportTime > 0)
            {
                float alpha = Mathf.Lerp(0, 1, teleportTime * 8);
                blinkCanvas.SetAlpha(alpha);
            }
            else
            {
                teleporing = false;
                blinkCanvas.gameObject.SetActive(false);
            }
        }
    }





    private void Update()
    {
        if (teleport > 0) TeleportRay();

        if(teleporing)
        {
            if (dash) DashTeleporting();
            else BlinkTeleporting();
        }
    }


    private void OnValidate()
    {
        if(mainCamera != null) 
        {
            if (mainCamera_camera == null) mainCamera_camera = mainCamera.GetComponent<Camera>();
            if (mainCamera_AudioListener == null) mainCamera_AudioListener = mainCamera.GetComponent<AudioListener>();
        }
        if (dashCamera != null)
        {
            if (dashCamera_camera == null) dashCamera_camera = dashCamera.GetComponent<Camera>();
            if (dashCamera_AudioListener == null) dashCamera_AudioListener = dashCamera.GetComponent<AudioListener>();
        }
    }
}
