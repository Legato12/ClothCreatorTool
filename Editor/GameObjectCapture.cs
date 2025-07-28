using UnityEngine;
//using UnityEngine.Rendering.Universal;

namespace Framework.Utility
{
    public class GameObjectCapture
    {
        private Vector2 cameraPosition = Vector2.zero;
        private const float mainCameraZ = -10;
        private static Camera mainCamera;

        private static Camera MainCamera
        {
            get
            {
                if (mainCamera == null)
                    mainCamera = Camera.main;
                return mainCamera;
            }
        }
        public static Rect CalculateSpritesView(GameObject gameObject)
        {
            Rect rect = new Rect();
            float minX = Mathf.Infinity;
            float minY = Mathf.Infinity;
            float maxX = -Mathf.Infinity;
            float maxY = -Mathf.Infinity;

            SpriteRenderer[] renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();

            if (renderers.Length == 0) return rect;

            foreach (SpriteRenderer sr in renderers)
            {
                if (sr.sprite == null)
                    continue;

                if (sr.bounds.min.x < minX)
                    minX = sr.bounds.min.x;
                if (sr.bounds.min.y < minY)
                    minY = sr.bounds.min.y;
                if (sr.bounds.max.x > maxX)
                    maxX = sr.bounds.max.x;
                if (sr.bounds.max.y > maxY)
                    maxY = sr.bounds.max.y;
            }

            rect.width = maxX - minX;
            rect.height = maxY - minY;
            rect.center = new Vector2(minX + rect.width / 2, minY + rect.height / 2);
            return rect;
        }

        public Texture2D Capture(int width, int height, GameObject gameObject, bool autoResize = true, float size = 4,
            float offsetX = 0, float offsetY = 0)
        {
            return Capture(width, height, gameObject, new Color(0, 0, 0, 0), autoResize, size, offsetX, offsetY);
        }

        public RenderTexture CaptureRenderTexture(int width, int height, GameObject gameObject, bool autoResize = true,
            float size = 4, float offsetX = 0, float offsetY = 0)
        {
            return CaptureRenderTexture(width, height, gameObject, new Color(0, 0, 0, 0), autoResize, size, offsetX,
                offsetY);
        }

        public Texture2D Capture(int width, int height, GameObject gameObject, Vector2 position, bool autoResize = true,
            float size = 4, float offsetX = 0, float offsetY = 0)
        {
            cameraPosition = position;
            Texture2D texture2D = Capture(width, height, gameObject, new Color(0, 0, 0, 0), autoResize, size, offsetX,
                offsetY, true);

            return texture2D;
        }

        private static Camera tempCamera;

        public Texture2D Capture(int width, int height, GameObject gameObject, Color bgColor, bool autoResize = true,
            float size = 4, float offsetX = 0, float offsetY = 0, bool setCameraPosition = false)
        {
            RenderTexture renderTexture = new RenderTexture(width, height, 16);

            if (tempCamera == null)
                tempCamera = CreateNewCamera();

            if (setCameraPosition)
            {
                tempCamera.transform.position = new Vector3(cameraPosition.x, cameraPosition.y, mainCameraZ);
            }
            else
            {
                tempCamera.transform.position = MainCamera.transform.position - Vector3.down * 1000;
            }

            tempCamera.backgroundColor = bgColor;
            tempCamera.targetTexture = renderTexture;

            Vector3 initialPos = gameObject.transform.position;
            Vector3 offset = new Vector3(offsetX, offsetY);
            Vector3 camTarget = new Vector3(tempCamera.transform.position.x, tempCamera.transform.position.y);

            if (autoResize)
            {
                Rect rect = CalculateSpritesView(gameObject);

                offset = (gameObject.transform.position - (Vector3) rect.center);
                gameObject.transform.position = camTarget + offset;

                if (rect.width != 0 && rect.height != 0)
                {
                    tempCamera.orthographicSize = rect.height / 2;
                    float cameraWidth = (2f * tempCamera.orthographicSize) * tempCamera.aspect;
                    if (cameraWidth < rect.width)
                        tempCamera.orthographicSize = (rect.width / tempCamera.aspect) / 2;
                }
            }
            else
            {
                gameObject.transform.position = tempCamera.transform.position + offset + (Vector3.forward * 2);
                tempCamera.orthographicSize = size;
            }

            tempCamera.enabled = true;
            tempCamera.RenderWithoutUIUpdate();
            tempCamera.targetTexture = null;
            tempCamera.enabled = false;
            gameObject.transform.position = initialPos;

            RenderTexture.active = renderTexture;

            Texture2D virtualPhoto =
                new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
            virtualPhoto.wrapMode = TextureWrapMode.Clamp;
            virtualPhoto.name = "photograph of" + gameObject.name;
            virtualPhoto.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            virtualPhoto.Apply(false, true);

            RenderTexture.active = null;
            Object.DestroyImmediate(renderTexture);
            return virtualPhoto;
        }

        public RenderTexture CaptureRenderTexture(int width, int height, GameObject gameObject, Color bgColor,
            bool autoResize = true, float size = 4, float offsetX = 0, float offsetY = 0,
            bool setCameraPosition = false)
        {
            RenderTexture renderTexture = new RenderTexture(width, height, 16);

            if (tempCamera == null)
                tempCamera = CreateNewCamera();

            if (setCameraPosition)
            {
                tempCamera.transform.position = new Vector3(cameraPosition.x, cameraPosition.y, mainCameraZ);
            }
            else
            {
                tempCamera.transform.position = MainCamera.transform.position - Vector3.down * 1000;
            }

            tempCamera.backgroundColor = bgColor;
            tempCamera.targetTexture = renderTexture;

            Vector3 initialPos = gameObject.transform.position;
            Vector3 offset = new Vector3(offsetX, offsetY);
            Vector3 camTarget = new Vector3(tempCamera.transform.position.x, tempCamera.transform.position.y);

            if (autoResize)
            {
                Rect rect = CalculateSpritesView(gameObject);

                offset = (gameObject.transform.position - (Vector3) rect.center);
                gameObject.transform.position = camTarget + offset;

                if (rect.width != 0 && rect.height != 0)
                {
                    tempCamera.orthographicSize = rect.height / 2;
                    float cameraWidth = (2f * tempCamera.orthographicSize) * tempCamera.aspect;
                    if (cameraWidth < rect.width)
                        tempCamera.orthographicSize = (rect.width / tempCamera.aspect) / 2;
                }
            }
            else
            {
                gameObject.transform.position = tempCamera.transform.position + offset + (Vector3.forward * 2);
                tempCamera.orthographicSize = size;
            }

            tempCamera.enabled = true;
            tempCamera.RenderWithoutUIUpdate();
            tempCamera.targetTexture = null;
            tempCamera.enabled = false;
            gameObject.transform.position = initialPos;

            renderTexture.name = "photograph of" + gameObject.name;

            return renderTexture;
        }

        private Camera CreateNewCamera()
        {
            Camera newCamera = null;
            GameObject photoGameObject = new GameObject("CaptureCamera");
            newCamera = photoGameObject.AddComponent<Camera>();
            newCamera.orthographic = MainCamera.orthographic;
            newCamera.depth = MainCamera.depth;
            newCamera.nearClipPlane = MainCamera.nearClipPlane;
            newCamera.farClipPlane = MainCamera.farClipPlane;
            //var cameraData = newCamera.GetUniversalAdditionalCameraData();
            //cameraData.renderPostProcessing = true;

            newCamera.transform.position = MainCamera.transform.position - Vector3.down * 1000;
            Object.DontDestroyOnLoad(photoGameObject);
            return newCamera;
        }
    }
}