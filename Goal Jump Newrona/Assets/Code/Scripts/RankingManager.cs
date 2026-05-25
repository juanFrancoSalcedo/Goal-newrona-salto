using System;
using System.Collections.Generic;
using B_Extensions;
using Services;
using UnityEngine;

namespace Features.Score
{
    public class RankingManager : Singleton<RankingManager>
    {
        [SerializeField] private List<CardRanking> cardRankings = new List<CardRanking>();
        [SerializeField] CardRanking currentPlayerCard;
        private const int MaxRankingPositions = 5;

        private PlayerData _bufferPlayerData;

        public void RegisterCurrentPlayer(string nombre, string correo, string telefono)
        {
            var uid = Guid.NewGuid().ToString();
            //float time = timer != null ? timer.GetCurrentTime() : 0f;
            _bufferPlayerData = new PlayerData(uid, nombre, correo, telefono, 0, 0);

        }

        public void UpdateRanking()
        {
            //UpdateRanking();
            //_bufferPlayerData.score = ScoreManager.Instance.TotalScore;

            if (GameStateContext.State != GameEventType.GameFinished)
            {
                print("RankingManager: Ignoring ranking update because the game is not finished.");
                return;
            }
            _bufferPlayerData.tiempo = ScreenResult.Instance.GetHighestScore();
            CsvPlayerSaver.Save(_bufferPlayerData);
            FirestoreService.Instance.SendPlayerData(_bufferPlayerData);
            List<PlayerData> players = CsvPlayerSaver.GetSavedPlayers();

            for (int i = 0; i < cardRankings.Count; i++)
            {
                if (i < MaxRankingPositions && i < players.Count)
                { 
                    if (_bufferPlayerData != null && _bufferPlayerData.uid.Equals(players[i].uid))
                        currentPlayerCard.SetData(i+1,_bufferPlayerData.nombre, _bufferPlayerData.score);
                
                    cardRankings[i].SetData(i + 1, players[i].nombre, players[i].score);
                }
                else
                    cardRankings[i].Clear();

            }
        }
    }
}