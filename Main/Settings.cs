using System;

namespace Main
{
    public class Settings
    {
        public GeneralSettings General { get; set; } = new();
        
        public RustSettings Rust { get; set; } = new();
        
        public class GeneralSettings
        {
            public enum Game
            {
                Rust
            }
            
            /// <summary>
            /// Whether to invert mouse y when sending to separate machine.
            /// </summary>
            public bool InvertMouseY = false;

            /// <summary>
            /// Whether to invert mouse x when sending to separate machine.
            /// </summary>
            public bool InvertMouseX = false;

            /// <summary>
            /// Current game cheating on.
            /// </summary>
            public Game CurrentGame;
        }

        public class RustSettings
        {
            public enum Guns
            {
                AssaultRifle,
                M249,
                Lr300,
                Mp5,
                Custom,
                Thompson
            }

            public struct Attachments
            {
                public const double MuzzleBoost = 0.9; // This is timing
                public const double MuzzleBrake = 0.5;
                public const double Silencer = 0.8;
            
                public const double Default = 1.0;
            }
            
            public struct Scopes
            {
                public const double Zoom8Scope = 3.83721;
                public const double Zoom16Scope = 7.65116;
                public const double HandmadeSight = 0.8;
                public const double HoloSight = 1.18605;
            
                public const double Default = 1.0;
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