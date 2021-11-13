
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

using System;

namespace Main
{
    public class Settings
    {
        public string Version = "1.0.0";
        
        public GeneralSettings General { get; } = new();
        
        public RustSettings Rust { get; } = new();
        
        public class GeneralSettings
        {
            public MouseSettings Mouse { get; } = new();
            public KeyboardSettings Keyboard { get; } = new();
            
            public class MouseSettings
            {
                /// <summary>
                /// Whether to use the mouse or not.
                /// </summary>
                public bool MouseState = true;
            
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
                
                /// <summary>
                /// Whether or not to output debug information to console.
                /// </summary>
                public bool DebugState = false;
            }
            
            public class KeyboardSettings
            {
                /// <summary>
                /// Whether to use the keyboard or not.
                /// </summary>
                public bool KeyboardState = true;
                
                /// <summary>
                /// Whether or not to output debug information to console.
                /// </summary>
                public bool DebugState = false;
            }
        }

        public class RustSettings
        {
            /// <summary>
            /// Whether or not to output debug information to console.
            /// </summary>
            public bool DebugState = false;
            
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
                /// <summary>
                /// <returns>Multiplier</returns>
                /// <returns>Timings</returns>
                /// </summary>
                public static readonly (double, double) MuzzleBoost = (1, 0.9);
                public static readonly (double, double) MuzzleBrake = (0.5, 1);
                public static readonly (double, double) Silencer = (0.8, 1);
            }
            
            public struct Scopes
            {
                public const double Zoom8Scope = 3.83721;
                public const double Zoom16Scope = 7.65116;
                public const double HandmadeSight = 0.8;
                public const double HoloSight = 1.18605;
            }
            
            public enum BulletCounts
            {
                ASSAULTRIFLE = 30,
                CUSTOM = 24,
                LR300 = 30,
                M249 = 100,
                MP5 = 30,
                THOMPSON = 20,
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
			
            
            public bool AmmoReset = false;
            
            public int Smoothness = 6;

            public double Sensitivity = 0.5;

            public int Fov = 90;

            public bool State = true;
			
			
            public bool Randomization = false;
            
            public bool ReverseRandomization = false;
            
            public Tuple<int, int> RandomizationAmountX = new(1, 5);
            
            public Tuple<int, int> RandomizationAmountY = new(1, 5);
            
            public Tuple<double, double> RecoilModifier = new(1, 1);
        }
    }
}