using UnityEngine;

public class TeleportMovement : MonoBehaviour
{
    [SerializeField] Transform recticle;
    [SerializeField] GameObject ground;


    private LineRenderer lineRenderer;
    private int telport;


    public void StartTeleport(LineRenderer line)
    {
        telport++;
        lineRenderer = line;

        if (telport == 1) EnableTeleport();
    }
    public void EndTeleport()
    {
        telport--;
        if (telport == 0) DisableTeleport();
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


    [SerializeField] Transform leftPhysicalHand;
    [SerializeField] Transform rightPhysicalHand;
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
        leftPhysicalHand.position = teleportPosition;
        rightPhysicalHand.position = teleportPosition;
    }


    [SerializeField] Transform MainCamera;
    [SerializeField] Camera mc_camera;
    [SerializeField] AudioListener mc_AudioListener;
    [SerializeField] Transform DashCamera;
    [SerializeField] Camera dc_camera;
    [SerializeField] AudioListener dc_AudioListener;
    [SerializeField] Renderer leftPhysicalHandRenderer;
    [SerializeField] Renderer rightPhysicalHandRenderer;

    private Vector3 oldMcPosition;

    private void DashTeleport()
    {
        teleportTime = 0.25f;

        leftPhysicalHandRenderer.enabled = false;
        rightPhysicalHandRenderer.enabled = false;

        mc_AudioListener.enabled = false;
        mc_camera.enabled = false;
        dc_AudioListener.enabled = true;
        dc_camera.enabled = true;

        oldMcPosition = MainCamera.position;

        DashCamera.position = MainCamera.position;
        DashCamera.rotation = MainCamera.rotation;

        TeleportBody();
    }

    private void DashTeleporting()
    {
        if(teleportTime > 0)
        {
            teleportTime -= Time.deltaTime * 1;

            DashCamera.rotation = MainCamera.rotation;

            DashCamera.position = Vector3.Lerp(MainCamera.position, oldMcPosition, teleportTime * 4);
        }
        else
        {
            leftPhysicalHandRenderer.enabled = true;
            rightPhysicalHandRenderer.enabled = true;

            mc_AudioListener.enabled = true;
            mc_camera.enabled = true;
            dc_AudioListener.enabled = false;
            dc_camera.enabled = false;

            teleporing = false;
        }
    }


    [SerializeField] TeleportBlinkCanvas blinkCanvas;
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
        if (telport > 0) TeleportRay();

        if(teleporing)
        {
            if (dash) DashTeleporting();
            else BlinkTeleporting();
        }
    }
}
