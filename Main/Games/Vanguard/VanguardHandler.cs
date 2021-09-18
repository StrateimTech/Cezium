using Main.HID;

namespace Main.Games.Vanguard
{
    public class VanguardHandler
    {
        private readonly Settings _settings;
        
        private readonly HidHandler _hidHandler;
        
        public VanguardHandler(Settings settings, HidHandler hidHandler)
        {
            _settings = settings;
            _hidHandler = hidHandler;
        }

        public void Start()
        {
            while (true)
            {
                
            }
        }
    }
}