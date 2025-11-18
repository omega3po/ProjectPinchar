using UnityEngine;
using UnityEngine.Rendering; // URP 대응을 위해 필요

// [수정 권장] System.Grid는 C# 시스템과 혼동될 수 있으니 변경 추천
namespace System.Grid 
{
    public class GridManager : MonoBehaviour
    {
        public static GridManager Instance { get; private set; }
        [SerializeField] private int width = 10;
        [SerializeField] private int height = 10;
        public int Width => width;
        public int Height => height;
        [SerializeField] private float cellSize = 1f;
        [SerializeField] private Color gridColor = Color.white;
        
        private Material lineMaterial;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
        
        void OnEnable()
        {
            RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
        }
        
        void OnDisable()
        {
            RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
        }
        
        void OnEndCameraRendering(ScriptableRenderContext context, Camera camera)
        {
            DrawGL(camera);
        }
        
        private void OnRenderObject()
        {
            DrawGL(Camera.current);
        }
        
        private void DrawGL(Camera cam)
        {
            if (cam.cameraType != CameraType.Game && cam.cameraType != CameraType.SceneView) return;
            CreateLineMaterial();
            lineMaterial.SetPass(0);
            GL.Begin(GL.LINES);
            GL.Color(gridColor);

            int halfWidth = width / 2;
            int halfHeight = height / 2;
            
            float zPos = -1f; 
            
            for (int x = -halfWidth; x <= halfWidth + 1; x++)
            {
                float xPos = x * cellSize - (cellSize * 0.5f);
                GL.Vertex3(xPos, -(halfHeight * cellSize) - (cellSize * 0.5f), zPos);
                GL.Vertex3(xPos, (halfHeight * cellSize) + (cellSize * 0.5f), zPos);
            }

            for (int y = -halfHeight; y <= halfHeight + 1; y++)
            {
                float yPos = y * cellSize - (cellSize * 0.5f);
                GL.Vertex3(-(halfWidth * cellSize) - (cellSize * 0.5f), yPos, zPos);
                GL.Vertex3((halfWidth * cellSize) + (cellSize * 0.5f), yPos, zPos);
            }

            GL.End();
        }

        public bool IsValidPosition(int x, int y)
        {
            int halfWidth = width / 2;
            int halfHeight = height / 2;
            bool insideX = x >= -halfWidth && x <= halfWidth;
            bool insideY = y >= -halfHeight && y <= halfHeight;
            return insideX && insideY;
        }
        
        private void CreateLineMaterial()
        {
            if (!lineMaterial)
            {
                Shader shader = Shader.Find("Sprites/Default");
                if (shader == null) shader = Shader.Find("Hidden/Internal-Colored"); 
                
                lineMaterial = new Material(shader);
                lineMaterial.hideFlags = HideFlags.HideAndDontSave;
                
                lineMaterial.color = gridColor; 
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = gridColor;
            int halfWidth = width / 2;
            int halfHeight = height / 2;
            
            for (int x = -halfWidth; x <= halfWidth + 1; x++) 
            {
                Vector3 start = new Vector3(x * cellSize - (cellSize * 0.5f), -(halfHeight * cellSize) - (cellSize * 0.5f), 0);
                Vector3 end = new Vector3(x * cellSize - (cellSize * 0.5f), (halfHeight * cellSize) + (cellSize * 0.5f), 0);
                Gizmos.DrawLine(start, end);
            }
            
            for (int y = -halfHeight; y <= halfHeight + 1; y++)
            {
                Vector3 start = new Vector3(-(halfWidth * cellSize) - (cellSize * 0.5f), y * cellSize - (cellSize * 0.5f), 0);
                Vector3 end = new Vector3((halfWidth * cellSize) + (cellSize * 0.5f), y * cellSize - (cellSize * 0.5f), 0);
                Gizmos.DrawLine(start, end);
            }
        }
    }
}