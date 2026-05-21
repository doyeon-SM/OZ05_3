// ============================================================
// UILineDrawer.cs
// LineRenderer 대신 UI Image를 길게 늘려 선처럼 보이게 만드는 방식.
// Screen Space - Overlay Canvas에서도 완벽하게 동작합니다.
// ============================================================
// 사용 방법:
//   BSTManager.cs의 DrawLineToParent() 안에서
//   UILineDrawer.DrawLine(nodeParent, parentPos, childPos) 을 호출하세요.
// ============================================================

using UnityEngine;
using UnityEngine.UI;

public static class UILineDrawer
{
    /// <summary>
    /// 두 앵커 위치(anchoredPosition) 사이에 흰색 선을 UI Image로 그립니다.
    /// parent: 선 오브젝트가 생성될 Canvas 하위 Transform
    /// fromPos: 시작점 (부모 노드의 anchoredPosition)
    /// toPos:   끝점   (자식 노드의 anchoredPosition)
    /// thickness: 선 두께 (기본 3px)
    /// </summary>
    public static GameObject DrawLine(
        Transform parent,
        Vector2 fromPos,
        Vector2 toPos,
        float thickness = 3f,
        Color? color = null)
    {
        // ── 선 오브젝트 생성 ──────────────────────────────────
        GameObject lineObj = new GameObject("UILine");
        lineObj.transform.SetParent(parent, false);

        // 노드보다 뒤에 그려지도록 sibling index를 가장 앞(0)으로 설정
        lineObj.transform.SetAsFirstSibling();

        // ── RectTransform 설정 ───────────────────────────────
        RectTransform rt = lineObj.AddComponent<RectTransform>();

        // 두 점 사이의 벡터
        Vector2 direction = toPos - fromPos;
        float distance = direction.magnitude;

        // 선의 중심점 = 두 점의 중간
        rt.anchoredPosition = fromPos + direction * 0.5f;

        // 선의 길이 = 두 점 거리, 두께는 파라미터로
        rt.sizeDelta = new Vector2(distance, thickness);

        // 선의 회전: 두 점을 잇는 방향으로 회전
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rt.localRotation = Quaternion.Euler(0f, 0f, angle);

        // ── Image 컴포넌트 ───────────────────────────────────
        Image img = lineObj.AddComponent<Image>();
        img.color = color ?? Color.white;
        // Sprite 없이도 단색 직사각형으로 렌더링됨

        return lineObj;
    }
}