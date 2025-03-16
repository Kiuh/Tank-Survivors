using UnityEngine;

namespace Common
{
    [AddComponentMenu("Scripts/Common/Common.FpsCounter")]
    internal class FpsCounter : MonoBehaviour
    {
        [SerializeField]
        private float updateInterval = 0.1f;

        private float accumulation;
        private int frames;
        private float timeLeft;
        private float fps;
        private GUIStyle textStyle = new();

        private float accumulationF;
        private int framesF;
        private float timeLeftF;
        private float fpsF;

        public static FpsCounter Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            timeLeft = updateInterval;
            timeLeftF = updateInterval;
            textStyle.fontStyle = FontStyle.Bold;
            textStyle.normal.textColor = Color.black;
            textStyle.fontSize = 40;
        }

        private void Update()
        {
            timeLeft -= Time.deltaTime;
            accumulation += Time.timeScale / Time.deltaTime;
            frames++;

            if (timeLeft <= 0.0)
            {
                fps = accumulation / frames;
                timeLeft = updateInterval;
                accumulation = 0.0f;
                frames = 0;
            }
        }

        private void FixedUpdate()
        {
            timeLeftF -= Time.fixedDeltaTime;
            accumulationF += Time.timeScale / Time.fixedDeltaTime;
            framesF++;

            if (timeLeftF <= 0.0)
            {
                fpsF = accumulationF / framesF;
                timeLeftF = updateInterval;
                accumulationF = 0.0f;
                framesF = 0;
            }
        }

        private void OnGUI()
        {
            string fpsString = "Fps: " + fps.ToString("F2");
            fpsString += "\nFFps: " + fpsF.ToString("F2");
            GUI.Label(new Rect(10, 10, 200, 25), fpsString, textStyle);
        }
    }
}
