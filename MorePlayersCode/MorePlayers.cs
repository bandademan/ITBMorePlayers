using MelonLoader;


namespace MorePlayers
{
    public class Main: MelonMod
    {
        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (sceneName == "MainMenu")
            {
                GameNetworkManager.MaxPlayersPerRoom = 15;
                LoggerInstance.Msg("WARNING: No testing has been done past 7 players so crashes or lag may occur past 7 players!");
            }
        }
    }
}
