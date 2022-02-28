using System;

namespace Cezium.Rust
{
    public class RustSettings
    {
        public (Guns, BulletCount, FireRate) Gun = (Guns.ASSAULT_RIFLE, BulletCount.ASSAULT_RIFLE,
            FireRate.ASSAULT_RIFLE);

        public Attachment? GunAttachment = null;

        public Scope? GunScope = null;

        public int Smoothness = 6;

        public double Sensitivity = 0.5;

        public int Fov = 90;

        /// <summary>
        /// Whether or not to output debug information to console.
        /// </summary>
        public bool DebugState = false;

        public bool State = true;

        public enum Guns
        {
            M249,

            ASSAULT_RIFLE,
            LR300,

            MP5,
            CUSTOM,
            THOMPSON

            // REVOLVER,
            // M92,
            // PYTHON,
            // SEMI_PISTOL,
            //
            // SEMI_RIFLE,
            // M39
        }

        public enum Scope
        {
            Scope8X,
            Scope16X,
            Handmade,
            Holo,
        }

        public enum Attachment
        {
            MuzzleBoost,
            MuzzleBrake,
            Silencer
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
            public const double Scope8X = 3.83721;
            public const double Scope16X = 7.65116;
            public const double Handmade = 0.8;
            public const double Holo = 1.18605;
        }

        public enum BulletCount
        {
            M249 = 100,

            ASSAULT_RIFLE = 30,
            LR300 = 30,

            MP5 = 30,
            CUSTOM = 24,
            THOMPSON = 20

            // REVOLVER = 8,
            // M92 = 15,
            // PYTHON = 6,
            // SEMI_PISTOL = 10,
            //
            // SEMI_RIFLE = 16,
            // M39 = 20
        }

        public enum FireRate
        {
            M249 = 500,

            ASSAULT_RIFLE = 450,
            LR300 = 500,

            MP5 = 600,
            CUSTOM = 600,
            THOMPSON = 462

            // REVOLVER = 343,
            // M92 = 400,
            // PYTHON = 400,
            // SEMI_PISTOL = 400,
            //
            // SEMI_RIFLE = 343,
            // M39 = 300
        }

        public bool InfiniteAmmo = false;
        
        public bool Tapping = false;

        public bool Randomization = false;

        public bool ReverseRandomization = false;

        public Tuple<int, int> RandomizationX = new(1, 5);

        public Tuple<int, int> RandomizationY = new(1, 5);

        public Tuple<double, double> RecoilModifier = new(1, 1);
    }
}