namespace Main
{
    public class Settings
    {
        public GeneralSettings General { get; set; } = new();
        
        public class GeneralSettings
        {
            /// <summary>
            /// Whether to invert mouse y when sending to separate machine.
            /// </summary>
            public bool InvertMouseY = false;

            /// <summary>
            /// Whether to invert mouse x when sending to separate machine.
            /// </summary>
            public bool InvertMouseX = false;
        }
    }
}