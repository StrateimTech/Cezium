
namespace Main
{
    public class Settings
    {
        public GeneralSettings General { get; } = new();
        
        public RustSettings Rust { get; } = new();
        
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
            
            /// <summary>
            /// Whether to invert mouse wheel when sending to separate machine.
            /// </summary>
            public bool InvertMouseWheel = false;
        }

        public class RustSettings
        {
            public enum Guns
            {
                ASSAULTRIFLE,
                M249,
                LR300,
                MP5,
                CUSTOM,
                THOMPSON
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
            
            public struct BulletCounts
            {
                public const int AssaultRifle = 30;
                public const int Custom = 24;
                public const int Lr300 = 30;
                public const int M249 = 100;
                public const int Mp5 = 30;
                public const int Thompson = 20;
            }
            
            public enum FireRate
            {
                ASSAULTRIFLE = 450,
                CUSTOM = 600,
                LR300 = 500,
                M249 = 500,
                MP5 = 600,
                THOMPSON = 462
            }

            public Guns CurrentGun;

            public Attachments CurrentAttachment;
            
            public Scopes CurrentScope;

            public int Smoothness = 6;

            public double Sensitivity = 0.5;

            public int Fov = 90;

            public bool State = true;
        }
    }
}