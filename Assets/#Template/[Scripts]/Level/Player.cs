using DancingLineFanmade.Trigger;
using DancingLineFanmade.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace DancingLineFanmade.Level
{
    [DisallowMultipleComponent, RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        private Transform selfTransform;

        public static Player Instance { get; private set; }
        public static Rigidbody Rigidbody { get; private set; }

        private GameObject tailPrefab;
        private GameObject cubesPrefab;
        private GameObject dustParticle;
        private GameObject uiPrefab;
        private GameObject startPrefab;
        private GameObject loadingPrefab;

        [Title("Data")]
        [Required("需要加载关卡文件")] public LevelData levelData;

        [Title("Settings")]
        public Camera sceneCamera;
        public Light sceneLight;
        public Material characterMaterial;
        public Vector3 startPosition = Vector3.zero;
        public Vector3 firstDirection = new Vector3(0, 90, 0);
        public Vector3 secondDirection = Vector3.zero;
        [MinValue(1)] public int poolSize = 100;
        public Color debugTextColor = Color.black;
        public List<Animator> playOnStartAnimators = new List<Animator>();
        public List<Animator> stopOnDieAnimators = new List<Animator>();
        public bool allowTurn = true;
        public bool noDeath = false;

        internal int speed { get; set; }
        internal AudioSource track { get; private set; }
        internal int trackProgress { get; set; }
        internal int blockCount { get; set; }
        internal UnityEvent onTurn { get; private set; }
        internal List<Checkpoint> checkpoints { get; set; }

        private BoxCollider characterCollider;
        private Vector3 tailPosition;
        private Transform tail;
        private ObjectPool<Transform> tailPool = new ObjectPool<Transform>();
        private List<float> startAnimatorProgresses = new List<float>();
        private StartPage startPage;
        private bool debug = true;
        private bool loading = false;

        private float TailDistance
        {
            get => new Vector2(tailPosition.x - selfTransform.position.x, tailPosition.z - selfTransform.position.z).magnitude;
        }

        private bool previousFrameIsGrounded;
        private float groundedRayDistance = 0.05f;
        private ValueTuple<Vector3, Ray>[] groundedTestRays;
        private RaycastHit[] groundedTestResults = new RaycastHit[1];
        public bool Falling
        {
            get
            {
                for (int i = 0; i < groundedTestRays.Length; i++)
                {
                    groundedTestRays[i].Item2.origin = selfTransform.position + selfTransform.localRotation * groundedTestRays[i].Item1;
                    if (Physics.RaycastNonAlloc(groundedTestRays[i].Item2, groundedTestResults, groundedRayDistance + 0.1f, -257, QueryTriggerInteraction.Ignore) > 0)
                        return false;
                }
                return true;
            }
        }

        private int frame;
        private float lastTime;
        private float fps;
        private const float timeInterval = 0.1f;

        private void Awake()
        {
            if (!levelData)
            {
                Debug.LogError("没有关卡数据加载！");
                LevelManager.DialogBox("����", "�޷���ȡ�ؿ���Ϣ����ȷ���ؿ������ļ���Level Data����ѡ��ȷ�Ҳ�Ϊ��", "ȷ��", true);
                return;
            }
            DOTween.Clear();
            Instance = this;
            Rigidbody = GetComponent<Rigidbody>();
            loading = false;
            checkpoints = new List<Checkpoint>();
            onTurn = new UnityEvent();
            selfTransform = transform;

            characterCollider = GetComponent<BoxCollider>();
            groundedTestRays = new ValueTuple<Vector3, Ray>[]
            {
                new ValueTuple<Vector3, Ray>(characterCollider.center - new Vector3(characterCollider.size.x * 0.5f, characterCollider.size.y * 0.5f - 0.1f, characterCollider.size.z * 0.5f), new Ray(Vector3.zero, selfTransform.localRotation * Vector3.down)),
                new ValueTuple<Vector3, Ray>(characterCollider.center - new Vector3(characterCollider.size.x * -0.5f, characterCollider.size.y * 0.5f - 0.1f, characterCollider.size.z * 0.5f), new Ray(Vector3.zero, selfTransform.localRotation * Vector3.down)),
                new ValueTuple<Vector3, Ray>(characterCollider.center - new Vector3(characterCollider.size.x * 0.5f, characterCollider.size.y * 0.5f - 0.1f, characterCollider.size.z * -0.5f), new Ray(Vector3.zero, selfTransform.localRotation * Vector3.down)),
                new ValueTuple<Vector3, Ray>(characterCollider.center - new Vector3(characterCollider.size.x * -0.5f, characterCollider.size.y * 0.5f - 0.1f, characterCollider.size.z * -0.5f), new Ray(Vector3.zero, selfTransform.localRotation * Vector3.down))
            };
            previousFrameIsGrounded = Falling;

            foreach (Animator animator in playOnStartAnimators) animator.speed = 0f;

            LoadingPage.Instance?.Fade(0f, 0.4f);

            lastTime = Time.realtimeSinceStartup;
        }

        private void Start()
        {
            levelData.SetLevelData();
            firstDirection = firstDirection.Convert();
            secondDirection = secondDirection.Convert();
            tailPool.Size = poolSize;
            LevelManager.InitPlayerPosition(this, startPosition, false);
            tailPrefab = Resources.Load<GameObject>("Prefabs/Tail");
            cubesPrefab = Resources.Load<GameObject>("Prefabs/Remain");
            dustParticle = Resources.Load<GameObject>("Prefabs/Dust");
            uiPrefab = Resources.Load<GameObject>("Prefabs/LevelUI");
            startPrefab = Resources.Load<GameObject>("Prefabs/StartPage");
            loadingPrefab = Resources.Load<GameObject>("Prefabs/LoadingPage");

            selfTransform.GetComponent<MeshRenderer>().material = characterMaterial;
            tailPrefab.GetComponent<MeshRenderer>().material = characterMaterial;
            selfTransform.eulerAngles = firstDirection;
            LevelManager.GameState = GameStatus.Waiting;
            Instantiate(uiPrefab);
            startPage = Instantiate(startPrefab).GetComponent<StartPage>();
            if (!LoadingPage.Instance) DontDestroyOnLoad(Instantiate(loadingPrefab));
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.R) && !loading)
            {
                loading = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if (Input.GetKeyDown(KeyCode.C) && LevelManager.GameState == GameStatus.Playing) Debug.Log("音频时间" + track.time);
            if (Input.GetKeyDown(KeyCode.D)) debug = !debug;
            if (Input.GetKeyDown(KeyCode.K) && LevelManager.GameState == GameStatus.Playing) LevelManager.PlayerDeath(this, DieReason.Hit, cubesPrefab, null, false);
            
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Time.timeScale += 0.1f;
                FindObjectOfType<AudioSource>().pitch = Time.timeScale;
                Debug.Log("TimeScale Added, Now: " + Time.timeScale);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Time.timeScale -= 0.1f;
                FindObjectOfType<AudioSource>().pitch = Time.timeScale;
                Debug.Log("TimeScale Minused, Now: " + Time.timeScale);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Time.timeScale = 1f;
                FindObjectOfType<AudioSource>().pitch = Time.timeScale;
                Debug.Log("TimeScale Reset, Now: " + Time.timeScale);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Time.timeScale = 2f;
                FindObjectOfType<AudioSource>().pitch = Time.timeScale;
                Debug.Log("TimeScale Reset, Now: " + Time.timeScale);
            }


            GetFrame();
#endif
            if (allowTurn && !LevelManager.IsPointedOnUI())
            {
                switch (LevelManager.GameState)
                {
                    case GameStatus.Waiting:
                        if (LevelManager.Clicked && !Falling)
                        {
                            LevelManager.GameState = GameStatus.Playing;
                            if (!track) track = AudioManager.PlayClip(levelData.soundTrack, 1f); else track.Play();
                            if (playOnStartAnimators != null) foreach (Animator animator in playOnStartAnimators) animator.speed = 1f;
                            foreach (PlayAnimator a in FindObjectsOfType<PlayAnimator>(true)) foreach (SingleAnimator s in a.animators) if (s.played) s.PlayAnimator();
                            foreach (FakePlayer f in FindObjectsOfType<FakePlayer>(true)) if (f.playing) f.state = FakePlayerState.Moving;
                            CreateTail();
                            if (startPage)
                            {
                                startPage.Hide();
                                startPage = null;
                            }
                        }
                        break;
                    case GameStatus.Playing: if (LevelManager.Clicked && !Falling) Turn(); break;
                }
            }
            if (LevelManager.GameState == GameStatus.Playing || LevelManager.GameState == GameStatus.Moving)
            {
                selfTransform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
                if (tail && !Falling)
                {
                    tail.position = (tailPosition + selfTransform.position) * 0.5f;
                    tail.localScale = new Vector3(tail.localScale.x, tail.localScale.y, TailDistance);
                    tail.position = new Vector3(tail.position.x, selfTransform.position.y, tail.position.z);
                    tail.LookAt(selfTransform);
                }
                if (previousFrameIsGrounded != Falling)
                {
                    previousFrameIsGrounded = Falling;
                    if (Falling) tail = null;
                    else
                    {
                        CreateTail();
                        Destroy(Instantiate(dustParticle, new Vector3(selfTransform.localPosition.x, selfTransform.localPosition.y - selfTransform.lossyScale.y * 0.5f + 0.2f, selfTransform.localPosition.z), Quaternion.Euler(90f, 0f, 0f)), 2f);
                    }
                }
            }
            if (LevelManager.GameState == GameStatus.Playing) trackProgress = track ? (int)(AudioManager.Progress * 100) : 0;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Obstacle") && !noDeath && LevelManager.GameState == GameStatus.Playing)
            {
                if (checkpoints.Count <= 0) LevelManager.PlayerDeath(this, DieReason.Hit, cubesPrefab, collision, false);
                else LevelManager.PlayerDeath(this, DieReason.Hit, cubesPrefab, collision, true);
            }
        }

        internal void Turn()
        {
            selfTransform.eulerAngles = selfTransform.eulerAngles == firstDirection ? secondDirection : firstDirection;
            CreateTail();
            onTurn.Invoke();
        }

        private void CreateTail()
        {
            Quaternion now = Quaternion.Euler(selfTransform.localEulerAngles);
            float offset = tailPrefab.transform.localScale.z * 0.5f;

            if (tail)
            {
                Quaternion last = Quaternion.Euler(tail.transform.localEulerAngles);
                float angle = Quaternion.Angle(last, now);
                if (angle >= 0f && angle <= 90f) offset = 0.5f * Mathf.Tan(Mathf.PI / 180f * angle * 0.5f);
                else offset = -0.5f * Mathf.Tan(Mathf.PI / 180f * ((180f - angle) * 0.5f));
                Vector3 end = tailPosition + last * Vector3.forward * (TailDistance + offset);
                tail.position = (tailPosition + end) * 0.5f;
                tail.position = new Vector3(tail.position.x, selfTransform.position.y, tail.position.z);
                tail.localScale = new Vector3(tail.localScale.x, tail.localScale.y, Vector3.Distance(tailPosition, end));
                tail.LookAt(selfTransform.position);
            }
            tailPosition = selfTransform.position + now * Vector3.back * Mathf.Abs(offset);
            if (!tailPool.Full)
            {
                tail = Instantiate(tailPrefab, selfTransform.position, selfTransform.rotation).transform;
                tailPool.Add(tail);
            }
            else
            {
                tail = tailPool.First();
                tailPool.Add(tail);
            }
        }

        internal void RevivePlayer(Checkpoint checkpoint)
        {
            checkpoint.Revival();
        }

        internal void ClearPool()
        {
            tailPool.DestoryAll();
            tail = null;
        }

        internal void GetAnimatorProgresses()
        {
            startAnimatorProgresses.Clear();
            foreach (Animator a in playOnStartAnimators) startAnimatorProgresses.Add(a.GetCurrentAnimatorStateInfo(0).normalizedTime);
        }

        internal void SetAnimatorProgresses()
        {
            for (int a = 0; a < playOnStartAnimators.Count; a++)
            {
                playOnStartAnimators[a].Play(playOnStartAnimators[a].GetCurrentAnimatorClipInfo(0)[0].clip.name, 0, startAnimatorProgresses[a]);
            }
        }

#if UNITY_EDITOR
        private void GetFrame()
        {
            frame++;
            if (Time.realtimeSinceStartup - lastTime < timeInterval) return;

            float time = Time.realtimeSinceStartup - lastTime;
            fps = frame / time;

            lastTime = Time.realtimeSinceStartup;
            frame = 0;
        }

        private void OnGUI()
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = debugTextColor;
            style.fontSize = 32;

            int finalFps = fps > 999f ? 999 : (int)fps;
            if (debug)
            {
                GUI.Label(new Rect(10, 10, 120, 50), "帧数：" + finalFps, style);
                GUI.Label(new Rect(10, 40, 120, 50), "游戏进度：" + trackProgress + "%", style);
                GUI.Label(new Rect(10, 100, 120, 50), "线头位置：" + selfTransform.localPosition, style);
                GUI.Label(new Rect(10, 130, 120, 50), "线头朝向：" + selfTransform.localEulerAngles, style);
                GUI.Label(new Rect(10, 160, 120, 50), "方块数量：" + blockCount + "/10", style);
                GUI.Label(new Rect(10, 190, 120, 50), "相机位置：" + CameraFollower.Instance.rotator.localPosition, style);
                GUI.Label(new Rect(10, 220, 120, 50), "相机旋转：" + CameraFollower.Instance.rotator.localEulerAngles, style);
                GUI.Label(new Rect(10, 250, 120, 50), "相机缩放：" + CameraFollower.Instance.scale.localScale, style);
                GUI.Label(new Rect(10, 280, 120, 50), "相机视场：" + sceneCamera.fieldOfView, style);
                GUI.Label(new Rect(10, 310, 120, 50), "当前倍速：" + Time.timeScale, style);
            }
        }
#endif

        [Button("Get Start Position", ButtonSizes.Large)]
        private void GetStartPosition()
        {
            startPosition = transform.position;
        }
    }
}