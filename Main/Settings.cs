using System;

namespace Main
{
    public class Settings
    {
        public GeneralSettings General { get; set; } = new();
        
        public RustSettings Rust { get; set; } = new();
        
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

        public class RustSettings
        {
            public enum Guns
            {
            }

            public enum Attachments
            {
            }
            
            public enum Scopes
            {
            }

            public Guns CurrentGun;

            public Attachments CurrentAttachment;
            
            public Scopes CurrentScope;

            public int Smoothness = 6;

            public double Sensitivity = 0.5;

            public int Fov = 90;

            public bool State = false;
        }
    }
}