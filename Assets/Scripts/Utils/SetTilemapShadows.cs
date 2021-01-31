using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SetTilemapShadows : MonoBehaviour
{
    public void Start()
    {
        CompositeCollider2D tilemapCollider;
        tilemapCollider = GetComponent<CompositeCollider2D>();
        GameObject shadowCasterContainer = gameObject;
        for (int i = 0; i < tilemapCollider.pathCount; i++)
        {
            Vector2[] pathVertices = new Vector2[tilemapCollider.GetPathPointCount(i)];
            tilemapCollider.GetPath(i, pathVertices);
            GameObject shadowCaster = new GameObject("shadow_caster_" + i);
            PolygonCollider2D shadowPolygon = (PolygonCollider2D)shadowCaster.AddComponent(typeof(PolygonCollider2D));
            shadowCaster.transform.parent = shadowCasterContainer.transform;
            shadowPolygon.points = pathVertices;
            shadowPolygon.enabled = false;
            ShadowCaster2D shadowCasterComponent = shadowCaster.AddComponent<ShadowCaster2D>();
            shadowCasterComponent.selfShadows = true;
        }
    }
}