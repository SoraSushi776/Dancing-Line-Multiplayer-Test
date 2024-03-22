using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    void Update()
    {
        // 获取屏幕安全区域
        Rect safeArea = Screen.safeArea;
        // 获取屏幕宽高
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        // 计算安全区域的宽高
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;
        // 转换为锚点
        anchorMin.x /= screenSize.x;
        anchorMin.y /= screenSize.y;
        anchorMax.x /= screenSize.x;
        anchorMax.y /= screenSize.y;
        // 设置安全区域
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
    }
}
