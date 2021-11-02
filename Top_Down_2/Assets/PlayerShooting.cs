using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Material weaponTracerMaterial;
    [SerializeField] private LayerMask shootMask;
    //[SerializeField] private Transform barrelPoint;



    public void Shoot(Vector3 fromPos, Vector3 targetPos)
    {
        fromPos = new Vector3(fromPos.x, fromPos.y, 0);
        targetPos = new Vector3(targetPos.x, targetPos.y, 0);
        Vector3 direction = (targetPos - fromPos).normalized;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(fromPos, direction, 100f, shootMask);
        Debug.DrawLine(fromPos, direction * 100f,Color.red, .3f);

        if (raycastHit2D.collider != null)
        {
            CreateWeaponTracer(fromPos, raycastHit2D.point);
        }
        else
        {
            CreateWeaponTracer(fromPos, direction * 100f);
        }
    }

    public void CreateWeaponTracer(Vector3 fromPos, Vector3 targetPos)
    {
        fromPos = new Vector3(fromPos.x, fromPos.y, 0);
        targetPos = new Vector3(targetPos.x, targetPos.y, 0);
        Vector3 direction = (targetPos - fromPos).normalized;
        float eulerZ = UtilsClass.GetAngleFromVectorFloat(direction) - 90f;
        float distance = Vector3.Distance(fromPos, targetPos);
        Vector3 tracerSpawnPos = fromPos + direction * distance * .5f;
        Material tmpWeaponTracerMaterial = new Material(weaponTracerMaterial);
        tmpWeaponTracerMaterial.SetTextureScale("_MainTex", new Vector2(1f, distance / 64f));

        World_Mesh worldMesh = World_Mesh.Create(tracerSpawnPos, eulerZ, .5f, distance, tmpWeaponTracerMaterial, null,10000);
        
        int frame = 0;
        float framerate = .032f;
        float timer = framerate;
        worldMesh.SetUVCoords(new World_Mesh.UVCoords(0, 0, 16, 256));
        FunctionUpdater.Create(() =>
        {

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                frame++;
                timer += framerate;
                if (frame >= 4)
                {
                    worldMesh.DestroySelf();
                    return true;
                }
                else
                {
                    worldMesh.SetUVCoords(new World_Mesh.UVCoords(16 * frame, 0, 16, 256));
                }
            }
            return false;
        });
    }
}
