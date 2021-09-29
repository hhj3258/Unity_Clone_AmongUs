using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoatingCrew : MonoBehaviour
{
    public EPlayerColor playerColor;

    SpriteRenderer spriteRenderer;
    Vector3 direction;      // 방향
    float floatingSpeed;    // 이동 속도
    float rotateSpeed;      // 회전 속도

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetFloatingCrew(Sprite sprite, EPlayerColor playerColor, 
        Vector3 direction, float floatingSpeed, float rotateSpeed, float size)
    {
        this.playerColor = playerColor;
        this.direction = direction;
        this.floatingSpeed = floatingSpeed;
        this.rotateSpeed = rotateSpeed;

        spriteRenderer.sprite = sprite;
        spriteRenderer.material.SetColor("_PlayerColor", PlayerColor.GetColor(playerColor));

        transform.localScale = new Vector3(size, size, size);
        spriteRenderer.sortingOrder = (int)Mathf.Lerp(1, 32767, size);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * floatingSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, rotateSpeed));
    }
}
