using Il2Cpp;
using Il2CppMirror;
using MelonLoader;
using UnityEngine;
using static UnityEngine.Object;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

namespace MorePlayers
{
    public class MorePlayers : MelonMod
    {
        int playerCount = 8;

        bool isMainMenu = false;
        NetworkManager? nm;
        GameObject? tab;
        ButtonsOptionSelect? bos;

        public override void OnInitializeMelon()
        {
            try
            {
                string text = File.ReadAllText("Mods/MaxPlayers.txt");
                if (!text.Equals(""))
                {
                    playerCount = int.Parse(text);
                }
            }
            catch (Exception e)
            {
                LoggerInstance.Msg("File not found! Setting defualt max player count");
            }

            LoggerInstance.Msg("Max Players Set to: " + playerCount);
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (sceneName == "MainMenu")
            {
                isMainMenu = true;
                MelonEvents.OnGUI.Subscribe(DrawDebugMenu, 100);

                nm = FindObjectOfType<NetworkManager>();
                GameNetworkManager.MaxPlayersPerRoom = playerCount;
                nm.maxConnections = playerCount;
                LoggerInstance.Msg("WARNING: No testing has been done past 7 players so crashes or lag may occur past 7 players!");
            }
            else
            {
                isMainMenu = false;
                MelonEvents.OnGUI.Unsubscribe(DrawDebugMenu);
            }
        }

        public override void OnUpdate()
        {
            if (Keyboard.current.backquoteKey.wasPressedThisFrame && isMainMenu)
            {
                tab = GameObject.Find("SlotOption_PLAYERS");
                bos = tab.GetComponentInChildren<ButtonsOptionSelect>();
                bos.SelectedOption = playerCount;
            }
        }

        private void DrawDebugMenu()
        {
            GUI.Label(new Rect(10, 10, 1000, 1000), "<b><size=25>ITB MorePlayers</size></b>"
                + "\n<b><size=20>by @Mr. Monocle#0433</size></b>"
                + "\n<b><size=15>How to Use</size></b>"
                + "\n<size=15>Press Host then press ~ (Tilde) to set the players to " + playerCount + "</size>"
                + "\n\n<size=15>WARNING: No testing has been done past 8 players so crashes or lag may occur past 8 players!</size>"
                );
        }
    }
}