using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

public class PipeManager : AbstractManager {
    [SerializeField] private Transform gameView;
    [SerializeField] private GameManager gameManager;
    private PipeControl pipeControl;
    private bool startGame;
    private bool stopPipe;
    private LinkedList<AbstractControl> pipeLinkedList;
    private List<AbstractControl> pipeList;
    

    public override void Setup() {
        pipeList=new List<AbstractControl>();
        pipeLinkedList = new LinkedList<AbstractControl>();
        for (int i = 0; i < 3; i++) {
            Vector3 pos;
            if (i == 0) {
                pos = new Vector3(gameView.localPosition.x + 40, gameView.localPosition.y + GetRandomLocalY());
            }
            else {
                pos = new Vector3(gameView.localPosition.x + i * 40 + GetRandomLocalX(),
                    gameView.localPosition.y + GetRandomLocalY());
            }

            pipeControl = PoolController.Use(ControlPrefab, pos) as PipeControl;
            pipeControl.PipeOffScreen += OnPipeOffScreen;
            pipeLinkedList.AddLast(pipeControl);
            pipeList.Add(pipeControl);
        }
        if (!startGame) {
            StopPipe();
        }
    }
    public void StopPipe() {
        StopPrefab(pipeList);
    }

    public override void Paused() {
        StopPrefab(pipeList);
    }

    public override void Play() {
        PlayPrefab(pipeList);
        startGame = true;
    }
    private void OnPipeOffScreen() {
        ReusePrefab();
    }
    public override void ReusePrefab() {
        AbstractControl lastPipe = pipeList[pipeList.Count - 1];
        Vector3 pos = new Vector2(lastPipe.transform.localPosition.x + GetRandomLocalX(),
            gameView.transform.localPosition.y + GetRandomLocalY());
        AbstractControl go = PoolController.Use(ControlPrefab, pos) as PipeControl;
        pipeList.Add(go);
        pipeList.Remove(pipeList[pipeList.Count - 1]);
        pipeLinkedList.AddLast(go);
    }

    public void Update() {
        if (startGame && pipeLinkedList.First.Value.transform.localPosition.x < 
            - 30) {
            pipeLinkedList.RemoveFirst();
            gameManager.PassedAnObstacle();
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            StopPipe();
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            Play();
        }
    }

    private int GetRandomLocalX() {
        int random = 7 * Random.Range(4, 6);
        return random;
    }

    private int GetRandomLocalY() {
        int random = 7 * Random.Range(-3, 4);
        return random;
    }
}
