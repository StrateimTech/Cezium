using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Cezium.Rust
{
    public class RustSettings
    {
        public (Guns, BulletCount, FireRate) Gun { get; set; } = (Guns.ASSAULT_RIFLE, BulletCount.ASSAULT_RIFLE,
            FireRate.ASSAULT_RIFLE);

        public Attachment? GunAttachment { get; set; } = null;

        public Scope? GunScope { get; set; } = null;

        public int Smoothness { get; set; } = 6;

        public double Sensitivity { get; set; } = 0.5;

        public int Fov { get; set; } = 90;

        /// <summary>
        /// Whether or not to output debug information to console.
        /// </summary>
        public bool DebugState { get; set; } = false;

        public bool State { get; set; } = true;

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

        public bool InfiniteAmmo { get; set; } = false;
        
        public bool Tapping { get; set; } = false;
        
        public bool AdjustCompensation { get; set; } = true;

        public bool Randomization { get; set; } = false;

        public bool ReverseRandomization { get; set; } = false;
        
        public bool StaticRandomization { get; set; } = false;
        
        public Tuple<int, int> RandomizationTiming { get; set; } = new(100, 100);

        public Tuple<int, int> RandomizationX { get; set; } = new(1, 5);

        public Tuple<int, int> RandomizationY { get; set; } = new(1, 5);

        public List<Tuple<int, int, double>> RandomizationTable = new();
        public Tuple<double, double> RecoilModifier { get; set; } = new(1, 1);
    }
}