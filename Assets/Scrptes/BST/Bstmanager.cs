// ============================================================
// Phase 2~5: BSTManager.cs
// Binary Search TreeÀÇ ÇÙ½É ·ÎÁ÷ + ½Ã°¢È­ + Å½»ö + ºñ±³
// ============================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BSTManager : MonoBehaviour
{
    // ¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // [Inspector] Phase 3 - ½Ã°¢È­ ¼³Á¤
    // ¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    [Header("Node Prefab & Spawn")]
    [Tooltip("¿øÇü UI ¶Ç´Â 3D TextMeshPro°¡ ºÙÀº Node Prefab")]
    public GameObject nodePrefab;

    [Tooltip("³ëµåµéÀÌ »ý¼ºµÉ ºÎ¸ð Transform (Canvas ¶Ç´Â ºó GameObject)")]
    public Transform nodeParent;

    [Header("Layout")]
    [Tooltip("Root ³ëµåÀÇ ¿ùµå/¾ÞÄ¿ À§Ä¡")]
    public Vector2 rootPosition = new Vector2(0f, 300f);

    [Tooltip("Depth°¡ ÇÑ ´Ü°è ³»·Á°¥ ¶§ Y °¨¼Ò·®")]
    public float ySpacing = 120f;

    [Tooltip("Depth 0¡æ1 ¿¡¼­ÀÇ X ºÐ±â Æø (±í¾îÁú¼ö·Ï Àý¹Ý¾¿ °¨¼Ò)")]
    public float xBaseSpacing = 200f;

    // ¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // [Inspector] Phase 4 - Å½»ö UI
    // ¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    [Header("Search UI")]
    [Tooltip("Å½»öÇÒ ¼ýÀÚ¸¦ ÀÔ·ÂÇÏ´Â TMP_InputField")]
    public TMP_InputField searchInputField;

    [Tooltip("'Search' ¹öÆ°")]
    public Button searchButton;

    [Tooltip("BST ºñ±³ È½¼ö Ãâ·Â ÅØ½ºÆ®")]
    public TMP_Text bstCompareCountText;

    // ¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // [Inspector] Phase 5 - ºñ±³ UI
    // ¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    [Header("Comparison UI")]
    [Tooltip("Linear Search ºñ±³ È½¼ö Ãâ·Â ÅØ½ºÆ®")]
    public TMP_Text linearCompareCountText;

    [Tooltip("°á°ú ¿ä¾à ÅØ½ºÆ® (¿¹: 'BST°¡ X¹ø ºü¸¨´Ï´Ù')")]
    public TMP_Text resultSummaryText;

    // ¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // ³»ºÎ »óÅÂ
    // ¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡

    // Phase 2: BSTÀÇ ÃÖ»ó´Ü ³ëµå
    private Node root = null;

    // Phase 5: µ¿ÀÏ µ¥ÀÌÅÍ¸¦ ´ãÀº ¼±Çü ¸®½ºÆ®
    private List<int> linearList = new List<int>();

    // ÇöÀç Å½»ö ÄÚ·çÆ¾ ½ÇÇà ¿©ºÎ (Áßº¹ ½ÇÇà ¹æÁö)
    private bool isSearching = false;

    // ³ëµå »ö»ó »ó¼ö
    private static readonly Color COLOR_DEFAULT = Color.white;
    private static readonly Color COLOR_VISITING = Color.red;
    private static readonly Color COLOR_FOUND = Color.green;
    private static readonly Color COLOR_NOTFOUND = new Color(1f, 0.5f, 0f); // ÁÖÈ²

    // ¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // Unity »ý¸íÁÖ±â
    // ¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    private void Start()
    {
        // ¹öÆ° ÀÌº¥Æ® µî·Ï
        if (searchButton != null)
            searchButton.onClick.AddListener(OnSearchButtonClicked);

        // Phase 2: ¿ä±¸»çÇ× µ¥ÀÌÅÍ »ðÀÔ [50, 30, 70, 20, 40, 60, 80]
        // Insert ´Ü°è¿¡¼­´Â ³ëµå GameObject¸¸ »ý¼ºÇÏ°í ¼±Àº ¾ÆÁ÷ ±×¸®Áö ¾ÊÀ½
        int[] initialData = { 50, 30, 70, 20, 40, 60, 80 };
        foreach (int val in initialData)
        {
            Insert(val);
            linearList.Add(val); // Phase 5: µ¿½Ã¿¡ List¿¡µµ Ãß°¡
        }

        // Phase 3: ¸ðµç ³ëµå Instantiate ¿Ï·á ÈÄ ¼±À» ÀÏ°ý »ý¼º
        // anchoredPositionÀÌ ·¹ÀÌ¾Æ¿ô¿¡ ¹Ý¿µµÈ ´ÙÀ½ ÇÁ·¹ÀÓ¿¡ ±×¸®±â À§ÇØ ÄÚ·çÆ¾ »ç¿ë
        StartCoroutine(DrawLinesNextFrame());
    }

    /// <summary>
    /// 1ÇÁ·¹ÀÓ ´ë±â ÈÄ ¼±À» ÀÏ°ý »ý¼ºÇÕ´Ï´Ù.
    /// Instantiate Á÷ÈÄ¿¡´Â RectTransformÀÇ anchoredPositionÀÌ
    /// ·¹ÀÌ¾Æ¿ô ½Ã½ºÅÛ¿¡ ¿ÏÀüÈ÷ ¹Ý¿µµÇÁö ¾ÊÀ» ¼ö ÀÖ±â ¶§¹®ÀÔ´Ï´Ù.
    /// </summary>
    private IEnumerator DrawLinesNextFrame()
    {
        yield return null; // 1ÇÁ·¹ÀÓ ´ë±â
        Debug.Log("[BST] DrawAllLines ½ÃÀÛ");
        DrawAllLines(root);
        Debug.Log("[BST] DrawAllLines ¿Ï·á");
    }

    // ¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // Phase 2: Insert ·ÎÁ÷ (Àç±Í ¹æ½Ä)
    // ¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡

    /// <summary>
    /// ¿ÜºÎ¿¡¼­ È£ÃâÇÏ´Â ÁøÀÔÁ¡. root°¡ nullÀÌ¸é »õ ³ëµå°¡ root°¡ µË´Ï´Ù.
    /// </summary>
    public void Insert(int newValue)
    {
        // depth 0, xOffset 0 ¿¡¼­ ½ÃÀÛ
        root = InsertRecursive(root, newValue, depth: 0, xOffset: 0f);
    }

    /// <summary>
    /// Àç±Í »ðÀÔ ÇÔ¼ö.
    /// ÇöÀç ³ëµå°¡ nullÀÌ¸é »õ ³ëµå¸¦ ¸¸µé°í Áï½Ã ½Ã°¢È­ÇÕ´Ï´Ù.
    /// ¾Æ´Ï¸é BST ±ÔÄ¢(ÀÛÀ¸¸é left, Å©¸é right)¿¡ µû¶ó ³»·Á°©´Ï´Ù.
    /// </summary>
    private Node InsertRecursive(Node current, int newValue, int depth, float xOffset)
    {
        // ºó ÀÚ¸® ¹ß°ß ¡æ »õ ³ëµå »ý¼º
        if (current == null)
        {
            Node newNode = new Node(newValue);
            newNode.depth = depth;
            newNode.xOffset = xOffset;

            // Phase 3: Áï½Ã ½Ã°¢È­
            SpawnNodeObject(newNode);
            return newNode;
        }

        // BST ÇÙ½É ±ÔÄ¢
        if (newValue < current.value)
        {
            // ÀÛÀ¸¸é ¿ÞÂÊÀ¸·Î, X´Â ¿ÞÂÊ(-), ´ÙÀ½ depth¿¡¼­ ÆøÀ» Àý¹Ý
            current.left = InsertRecursive(
                current.left,
                newValue,
                depth + 1,
                xOffset - (xBaseSpacing / Mathf.Pow(2, depth))
            );
        }
        else if (newValue > current.value)
        {
            // Å©¸é ¿À¸¥ÂÊÀ¸·Î, X´Â ¿À¸¥ÂÊ(+)
            current.right = InsertRecursive(
                current.right,
                newValue,
                depth + 1,
                xOffset + (xBaseSpacing / Mathf.Pow(2, depth))
            );
        }
        // °°Àº °ªÀº ¹«½Ã (BST ±âº» Á¤Ã¥)

        return current;
    }

    // ¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // Phase 3: ½Ã°¢È­ - ³ëµå ¿ÀºêÁ§Æ® »ý¼º & À§Ä¡ ¹èÄ¡
    // ¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡

    /// <summary>
    /// Node µ¥ÀÌÅÍ·ÎºÎÅÍ PrefabÀ» InstantiateÇÏ°í À§Ä¡¸¦ ¹èÄ¡ÇÕ´Ï´Ù.
    /// ¼± ¿¬°áÀº ¸ðµç ³ëµå »ý¼ºÀÌ ³¡³­ µÚ DrawAllLines()¿¡¼­ ÀÏ°ý Ã³¸®ÇÕ´Ï´Ù.
    /// </summary>
    private void SpawnNodeObject(Node node)
    {
        if (nodePrefab == null || nodeParent == null)
        {
            Debug.LogError($"[BST] SpawnNodeObject ½ÇÆÐ: nodePrefab={nodePrefab}, nodeParent={nodeParent}");
            return;
        }

        // À§Ä¡ °è»ê
        Vector2 spawnPos = new Vector2(
            rootPosition.x + node.xOffset,
            rootPosition.y - node.depth * ySpacing
        );

        // Instantiate
        GameObject obj = Instantiate(nodePrefab, nodeParent);
        obj.name = $"Node_{node.value}";

        // RectTransform À§Ä¡ ¼³Á¤
        RectTransform rt = obj.GetComponent<RectTransform>();
        if (rt != null)
        {
            rt.anchoredPosition = spawnPos;
            Debug.Log($"[BST] Node {node.value} »ý¼º ¡æ anchoredPos={spawnPos}");
        }
        else
        {
            obj.transform.localPosition = new Vector3(spawnPos.x, spawnPos.y, 0f);
            Debug.Log($"[BST] Node {node.value} »ý¼º (RectTransform ¾øÀ½) ¡æ localPos={spawnPos}");
        }

        // TextMeshPro¿¡ °ª Ç¥½Ã
        TMP_Text label = obj.GetComponentInChildren<TMP_Text>();
        if (label != null)
            label.text = node.value.ToString();
        else
            Debug.LogWarning($"[BST] Node {node.value} Prefab¿¡ TMP_Text ÄÄÆ÷³ÍÆ®°¡ ¾ø½À´Ï´Ù.");

        // Node¿¡ ¿ÀºêÁ§Æ® ÂüÁ¶ ÀúÀå
        node.nodeObject = obj;
    }

    /// <summary>
    /// Æ®¸® ÀüÃ¼¸¦ ¼øÈ¸ÇÏ¸ç ¸ðµç ºÎ¸ð-ÀÚ½Ä ³ëµå »çÀÌ¿¡ ¼±À» ±×¸³´Ï´Ù.
    /// ¸ðµç ³ëµå Instantiate°¡ ¿Ï·áµÈ µÚ Start()¿¡¼­ ÇÑ ¹ø¸¸ È£ÃâÇÕ´Ï´Ù.
    /// </summary>
    private void DrawAllLines(Node node)
    {
        if (node == null) return;

        // ¿ÞÂÊ ÀÚ½Ä°ú ¼± ¿¬°á
        if (node.left != null)
        {
            DrawLineBetween(node, node.left);
            DrawAllLines(node.left);
        }

        // ¿À¸¥ÂÊ ÀÚ½Ä°ú ¼± ¿¬°á
        if (node.right != null)
        {
            DrawLineBetween(node, node.right);
            DrawAllLines(node.right);
        }
    }

    /// <summary>
    /// ºÎ¸ð ³ëµå¿Í ÀÚ½Ä ³ëµå »çÀÌ¿¡ UI Image ±â¹Ý Èò»ö ¼±À» ±×¸³´Ï´Ù.
    /// anchoredPositionÀ» Á÷Á¢ ÂüÁ¶ÇÏ¹Ç·Î Instantiate ÀÌÈÄ¿¡ È£ÃâÇØ¾ß ÇÕ´Ï´Ù.
    /// </summary>
    private void DrawLineBetween(Node parent, Node child)
    {
        if (parent.nodeObject == null || child.nodeObject == null)
        {
            Debug.LogWarning($"[BST] DrawLineBetween ½ÇÆÐ: nodeObject°¡ nullÀÔ´Ï´Ù. parent={parent.value}, child={child.value}");
            return;
        }

        RectTransform parentRT = parent.nodeObject.GetComponent<RectTransform>();
        RectTransform childRT = child.nodeObject.GetComponent<RectTransform>();

        if (parentRT == null || childRT == null)
        {
            Debug.LogWarning($"[BST] DrawLineBetween ½ÇÆÐ: RectTransform ¾øÀ½. parent={parent.value}, child={child.value}");
            return;
        }

        Vector2 fromPos = parentRT.anchoredPosition;
        Vector2 toPos = childRT.anchoredPosition;

        Debug.Log($"[BST] ¼± »ý¼º: {parent.value}({fromPos}) ¡æ {child.value}({toPos})");

        UILineDrawer.DrawLine(nodeParent, fromPos, toPos, thickness: 3f, color: Color.white);
    }

    /// <summary>
    /// [±âÁ¸ È£È¯¿ë - ´õ ÀÌ»ó Á÷Á¢ È£ÃâÇÏÁö ¾ÊÀ½]
    /// ºÎ¸ð ³ëµå¿Í ÇöÀç ³ëµå »çÀÌ¿¡ UI Image ±â¹Ý Èò»ö ¼±À» ±×¸³´Ï´Ù.
    /// </summary>
    private void DrawLineToParent(Node node)
    {
        if (node.depth == 0) return;

        Node parent = FindParent(root, node.value);
        if (parent == null || parent.nodeObject == null || node.nodeObject == null) return;

        RectTransform parentRT = parent.nodeObject.GetComponent<RectTransform>();
        RectTransform childRT = node.nodeObject.GetComponent<RectTransform>();

        if (parentRT == null || childRT == null) return;

        UILineDrawer.DrawLine(nodeParent, parentRT.anchoredPosition, childRT.anchoredPosition, thickness: 3f, color: Color.white);
    }

    /// <summary>
    /// ÁÖ¾îÁø °ªÀ» °¡Áø ³ëµåÀÇ ºÎ¸ð¸¦ Àç±ÍÀûÀ¸·Î Ã£½À´Ï´Ù.
    /// </summary>
    private Node FindParent(Node current, int targetValue)
    {
        if (current == null) return null;

        if ((current.left != null && current.left.value == targetValue) ||
            (current.right != null && current.right.value == targetValue))
            return current;

        if (targetValue < current.value)
            return FindParent(current.left, targetValue);
        else
            return FindParent(current.right, targetValue);
    }

    // ¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // Phase 4: Search ¾Ë°í¸®Áò + ½Ã°¢Àû ÇÇµå¹é (Coroutine)
    // ¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡

    /// <summary>
    /// UI ¹öÆ° Å¬¸¯ ½Ã È£ÃâµÇ´Â ÁøÀÔÁ¡
    /// </summary>
    private void OnSearchButtonClicked()
    {
        if (isSearching) return; // Å½»ö Áß Áßº¹ Å¬¸¯ ¹æÁö

        if (searchInputField == null) return;

        if (!int.TryParse(searchInputField.text, out int target))
        {
            Debug.LogWarning("¼ýÀÚ¸¦ ÀÔ·ÂÇÏ¼¼¿ä.");
            return;
        }

        // ¸ðµç ³ëµå »ö»ó ÃÊ±âÈ­
        ResetAllNodeColors(root);

        // Phase 4: BST ½Ã°¢Àû Å½»ö (Coroutine)
        // Phase 5: Linear Search´Â Áï½Ã ½ÇÇà ÈÄ °á°ú¸¸ Ç¥½Ã
        StartCoroutine(SearchCoroutine(target));
    }

    /// <summary>
    /// Phase 4: 0.5ÃÊ °£°ÝÀ¸·Î Å½»ö °úÁ¤À» ½Ã°¢È­ÇÏ´Â ÄÚ·çÆ¾
    /// µ¿½Ã¿¡ Phase 5ÀÇ Linear Searchµµ ½ÇÇàÇÏ¿© °á°ú¸¦ ºñ±³ÇÕ´Ï´Ù.
    /// </summary>
    private IEnumerator SearchCoroutine(int target)
    {
        isSearching = true;

        // ¦¡¦¡ Phase 5: Linear Search (Áï½Ã ½ÇÇà, °á°ú¸¸ Ä«¿îÆ®) ¦¡¦¡
        int linearCount = LinearSearch(target);

        // ¦¡¦¡ Phase 4: BST Search (½Ã°¢ÀûÀ¸·Î ´Ü°èº° Å½»ö) ¦¡¦¡
        int bstCount = 0;
        Node current = root;
        bool found = false;

        while (current != null)
        {
            bstCount++;

            // ÇöÀç ³ëµå ¹æ¹® »ö»ó: »¡°£»ö
            SetNodeColor(current, COLOR_VISITING);
            yield return new WaitForSeconds(0.5f);

            if (target == current.value)
            {
                // Ã£À½: ³ì»öÀ¸·Î º¯°æ
                SetNodeColor(current, COLOR_FOUND);
                found = true;
                break;
            }
            else if (target < current.value)
            {
                // ¹æ¹®ÇÑ ³ëµå¸¦ ´Ù½Ã Èò»öÀ¸·Î º¹¿ø ÈÄ ¿ÞÂÊÀ¸·Î ÀÌµ¿
                SetNodeColor(current, COLOR_DEFAULT);
                current = current.left;
            }
            else
            {
                SetNodeColor(current, COLOR_DEFAULT);
                current = current.right;
            }
        }

        // Ã£Áö ¸øÇÑ °æ¿ì
        if (!found && current == null)
        {
            Debug.Log($"[BST] {target} NULL");
        }

        // ¦¡¦¡ UI °á°ú Ãâ·Â ¦¡¦¡
        // Phase 4: BST ºñ±³ È½¼ö
        if (bstCompareCountText != null)
            bstCompareCountText.text = $"BST CompareCount: {bstCount} ({(found ? "Find" : "Null")})";

        // Phase 5: Linear ºñ±³ È½¼ö
        if (linearCompareCountText != null)
            linearCompareCountText.text = $"Linear CompareCount: {linearCount}";

        // Phase 5: ¿ä¾à ºñ±³
        if (resultSummaryText != null)
        {
            int diff = linearCount - bstCount;
            if (diff > 0)
                resultSummaryText.text = $"ResultSummary: LinearCount - bstCount = {diff}";
            else if (diff < 0)
                resultSummaryText.text = $"ResultSummary: LinearCount - bstCount = {Mathf.Abs(diff)}";
            else
                resultSummaryText.text = "ResultSummary: LinearCount - bstCount = 0";
        }

        isSearching = false;
    }

    // ¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // Phase 5: Linear Search
    // ¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡

    /// <summary>
    /// ListÀÇ 0¹ø ÀÎµ¦½ººÎÅÍ ¼øÂ÷ Å½»öÇÕ´Ï´Ù.
    /// ºñ±³ È½¼ö¸¦ ¹ÝÈ¯ÇÕ´Ï´Ù.
    /// </summary>
    private int LinearSearch(int target)
    {
        int count = 0;
        foreach (int val in linearList)
        {
            count++;
            if (val == target)
                break;
        }
        return count;
    }

    // ¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // ÇïÆÛ: ³ëµå »ö»ó Á¦¾î
    // ¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡

    /// <summary>
    /// ³ëµå GameObjectÀÇ Image ÄÄÆ÷³ÍÆ® »ö»óÀ» º¯°æÇÕ´Ï´Ù.
    /// </summary>
    private void SetNodeColor(Node node, Color color)
    {
        if (node?.nodeObject == null) return;

        // UI Image ¹æ½Ä
        Image img = node.nodeObject.GetComponent<Image>();
        if (img != null)
        {
            img.color = color;
            return;
        }

        // 3D Sprite Renderer ¹æ½Ä (Prefab¿¡ µû¶ó ºÐ±â)
        SpriteRenderer sr = node.nodeObject.GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.color = color;
    }

    /// <summary>
    /// Æ®¸® ³» ¸ðµç ³ëµåÀÇ »ö»óÀ» ±âº»°ª(Èò»ö)À¸·Î ÃÊ±âÈ­ÇÕ´Ï´Ù.
    /// </summary>
    private void ResetAllNodeColors(Node node)
    {
        if (node == null) return;
        SetNodeColor(node, COLOR_DEFAULT);
        ResetAllNodeColors(node.left);
        ResetAllNodeColors(node.right);
    }
}