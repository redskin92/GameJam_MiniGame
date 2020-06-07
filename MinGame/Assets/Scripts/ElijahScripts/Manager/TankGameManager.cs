using System;
using System.Collections.Generic;
using ElijahScripts.Tank;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ElijahScripts.Manager
{
    public class TankGameManager : MonoBehaviour
    {
        private static TankGameManager instance;
        
        [SerializeField]
        protected GameObject tankPrefab;

        [SerializeField]
        protected Transform gameRoot;

        [SerializeField]
        protected Camera gameCamera;
        
        protected List<TankEntity> playerTanks;
        protected int currentPlayerIndex = 0;
        protected int numPlayers = 2;

        private List<Color> colors = new List<Color>()
        {
            Color.white,
            Color.red,
            Color.blue,
            Color.green,
            Color.yellow,
            Color.magenta,
            Color.cyan,
        };

        public static TankGameManager Instance => instance;

        void Awake()
        {
            if(instance == null)
                instance = this;
            else
                Destroy(this);
            
            // TODO: Generate based on players.
            //InitializeGame(GameFlow.Instance.PlayerInfo.Count);
            InitializeGame(1);
        }

        void InitializeGame(int _numPlayers)
        {
            numPlayers = _numPlayers;
            playerTanks = new List<TankEntity>();
            for (int i = 0; i < numPlayers; i++)
            {
                // TODO: Get spawn points or generate them.
                GameObject tankObject = InstantiateObject(tankPrefab, new Vector3(-6, -3, 0), Quaternion.identity);
                tankObject.GetComponent<TankColor>().SetColor(colors[Random.Range(0, colors.Count - 1)]);
                playerTanks.Add(tankObject.GetComponent<TankEntity>());
                playerTanks[i].Initialize();
            }
            
            BeginTurn();
        }

        void BeginTurn()
        {
            TankEntity currPlayer = playerTanks[currentPlayerIndex];
            TankControls controls = currPlayer.GetComponent<TankControls>();
            
            controls.EnableInput(true);
            currPlayer.Select();
        }

        void EndTurn()
        {
            TankEntity currPlayer = playerTanks[currentPlayerIndex];
            TankControls controls = currPlayer.GetComponent<TankControls>();

            controls.EnableInput(false);
            IncreasePlayerIndex();
        }

        void IncreasePlayerIndex()
        {
            currentPlayerIndex++;
            if (currentPlayerIndex >= numPlayers)
                currentPlayerIndex = 0;
        }

        void SelectPlayerIndex(int index)
        {
            if(index < 0 || index >= numPlayers)
                throw new Exception($"Attempted to select a player out of range! NumPlayers: {numPlayers} Input: {index}");

            currentPlayerIndex = index;
        }
        
        public static GameObject InstantiateObject(GameObject obj, Vector3 pos, Quaternion rot, Transform specificParent = null)
        {
            return Instantiate(obj, pos, rot, specificParent == null ? instance.gameRoot : specificParent);
        }

        public static Camera GetCamera()
        {
            return instance.gameCamera;
        }

        private void OnDestroy()
        {
            if (instance == this)
                instance = null;
        }
    }
}
