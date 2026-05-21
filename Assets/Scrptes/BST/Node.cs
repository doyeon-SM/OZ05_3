// ============================================================
// Phase 1: Node.cs
// MonoBehaviour를 상속받지 않는 순수 C# 데이터 클래스
// ============================================================

using UnityEngine;

public class Node
{
    // 이 노드가 저장하는 정수값
    public int value;

    // 왼쪽 자식 노드 (현재 값보다 작은 값)
    public Node left;

    // 오른쪽 자식 노드 (현재 값보다 큰 값)
    public Node right;

    // 시각화를 위한 참조 (Phase 3에서 사용)
    public GameObject nodeObject;

    // 트리 내 깊이 (root = 0)
    public int depth;

    // 화면상 X 위치 오프셋 (부모 기준)
    public float xOffset;

    // 생성자: 값만 넣으면 나머지는 null/0으로 초기화
    public Node(int newValue)
    {
        value = newValue;
        left = null;
        right = null;
        nodeObject = null;
        depth = 0;
        xOffset = 0f;
    }
}