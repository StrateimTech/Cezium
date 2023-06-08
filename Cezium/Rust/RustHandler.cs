using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using Cezium.Utils;
using HID_API;

namespace Cezium.Rust
{
    public class RustHandler
    {
        public RustSettings Settings { get; } = new();

        private readonly HidHandler _hidHandler;

        /// <summary>
        /// The current bullet that the gun is on
        /// </summary>
        private int _bullet;

        private bool _state = true;

        private readonly Stopwatch _recoilWatch = new();

        /// <summary>
        /// <param name="table"> 1 x, 2 y, 3 timing</param>
        /// <param name="scope">the gun's scope multiplier applied to when calculating the gun's pixel table</param>
        /// <param name="attachment">the gun's attachment (multiplier, and timings) applied to when calculating the gun's pixel table and delays between shots</param>
        /// </summary>
        // private (List<Tuple<double, double, double>> table, double scope, (double, double) attachment) _weapon;
        private (List<Tuple<double, double>> angleTable, double scope, (double, double) attachment) _weapon;

        private Tuple<int, int> _lastRandomization = new(0, 0);
        private bool _reverseRandom;

        private readonly Random _random = new();

        public RustHandler(HidHandler hidHandler)
        {
            UpdateWeapon(Settings.Gun, null, null);
            _hidHandler = hidHandler;
        }

        public void Start()
        {
            if (_hidHandler.HidMouseHandlers.Count <= 0)
            {
                ConsoleUtils.WriteLine("Failed to find any mouses connected", true);
            }

            while (_state)
            {
                if (_hidHandler.HidMouseHandlers.Count <= 0 || !Settings.State ||
                    !_hidHandler.HidMouseHandlers[0].Mouse.LeftButton ||
                    !_hidHandler.HidMouseHandlers[0].Mouse.RightButton)
                {
                    Thread.Sleep(1);
                }

                if (_hidHandler.HidMouseHandlers.Count <= 0 || !Settings.State)
                {
                    continue;
                }

                if (_hidHandler.HidMouseHandlers[0].Mouse.LeftButton &&
                    _hidHandler.HidMouseHandlers[0].Mouse.RightButton)
                {
                    if (_recoilWatch.IsRunning)
                    {
                        _recoilWatch.Reset();
                    }

                    if (_bullet >= (int) Settings.Gun.Item2 - 1)
                    {
                        if (Settings.InfiniteAmmo)
                        {
                            _bullet = 0;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    var calculatedRealTime = CalculatePixel(_bullet, _weapon.angleTable);
                    
                    var pixelXTable = calculatedRealTime.Item1;
                    var pixelYTable = calculatedRealTime.Item2;
                    var pixelControlTiming = calculatedRealTime.Item3;

                    pixelXTable *= Settings.RecoilModifier.Item1;
                    pixelYTable *= Settings.RecoilModifier.Item2;

                    var gunPixelX = pixelXTable * _weapon.scope * _weapon.attachment.Item1;
                    var gunPixelY = pixelYTable * _weapon.scope * _weapon.attachment.Item1;

                    var smoothing = Settings.Smoothness;

                    if (Settings.DebugState)
                    {
                        ConsoleUtils.WriteLine(
                            $"Weapon: {Settings.Gun.Item1} | Scope: {(Settings.GunScope == null ? "Empty" : Settings.GunScope)} | Attachment: {(Settings.GunAttachment == null ? "Empty" : Settings.GunAttachment)}");

                        ConsoleUtils.WriteLine(
                            $"Bullet: {_bullet}, Smoothing: {smoothing}, X: {gunPixelX}, Y: {gunPixelY}");
                    }

                    if (Settings.ReverseRandomization && Settings.Randomization && _reverseRandom)
                    {
                        var invertedLastX = _lastRandomization.Item1 * -1;
                        var invertedLastY = _lastRandomization.Item2 * -1;

                        if (Settings.DebugState)
                        {
                            ConsoleUtils.WriteLine(
                                $"Reverse Randomization: InvertedLastX: {invertedLastX}, InvertedLastY: {invertedLastY}");
                        }

                        gunPixelX += invertedLastX;
                        gunPixelY += invertedLastY;
                        _reverseRandom = false;
                    }

                    if (Settings.Randomization)
                    {
                        int xRandom;
                        int yRandom;

                        if (Settings.StaticRandomization)
                        {
                            xRandom = Settings.RandomizationTable[_bullet].Item1;
                            yRandom = Settings.RandomizationTable[_bullet].Item2;
                        }
                        else
                        {
                            xRandom = Settings.RandomizationX.Item2 != 0
                                ? _random.Next(Settings.RandomizationX.Item1, Settings.RandomizationX.Item2 + 1)
                                : 0;
                            yRandom = Settings.RandomizationY.Item2 != 0
                                ? _random.Next(Settings.RandomizationY.Item1, Settings.RandomizationY.Item2 + 1)
                                : 0;

                            bool xBool = _random.Next() > (Int32.MaxValue / 2);
                            bool yBool = _random.Next() > (Int32.MaxValue / 2);

                            xRandom = xBool ? xRandom : xRandom * -1;
                            yRandom = yBool ? yRandom : yRandom * -1;
                        }

                        if (Settings.DebugState)
                        {
                            ConsoleUtils.WriteLine(
                                $"Randomization: xRandom: {xRandom}, yRandom: {yRandom}, (Static: {Settings.StaticRandomization})");
                        }

                        gunPixelX += xRandom;
                        gunPixelY += yRandom;

                        _lastRandomization = new(xRandom, yRandom);
                        _reverseRandom = true;
                    }

                    var delay = 60000.0 / (int) Settings.Gun.Item3;

                    var timing = delay - pixelControlTiming;
                    if (Settings.Randomization)
                    {
                        double timingPercentRandomization;
                        if (Settings.StaticRandomization)
                        {
                            timingPercentRandomization = Settings.RandomizationTable[_bullet].Item3;
                        }
                        else
                        {
                            timingPercentRandomization =
                                _random.NextDouble() *
                                (Settings.RandomizationTiming.Item2 - Settings.RandomizationTiming.Item1) +
                                Settings.RandomizationTiming.Item1;
                        }

                        if (Settings.DebugState)
                        {
                            ConsoleUtils.WriteLine(
                                $"Random Timing %: {timingPercentRandomization}%, Timing {timing}, Modified Timing: {timingPercentRandomization * timing / 100}, (Static: {Settings.StaticRandomization})\n");
                        }

                        timing = timingPercentRandomization * timing / 100;
                    }

                    var sleep = pixelControlTiming / smoothing * _weapon.attachment.Item2;

                    if (Settings.DebugState)
                    {
                        ConsoleUtils.WriteLine(
                            $"Timing: {timing}, Sleep: {sleep}, PixelControlTiming: {pixelControlTiming} \n");
                    }

                    double totalLocalLossAdjustX = 0;
                    double totalLocalLossAdjustY = 0;
                    
                    double totalLossAdjustX = 0;
                    double totalLossAdjustY = 0;

                    for (int i = 0; i < smoothing; i++)
                    {
                        if ((!_hidHandler.HidMouseHandlers[0].Mouse.LeftButton ||
                             !_hidHandler.HidMouseHandlers[0].Mouse.RightButton) && Settings.Tapping)
                        {
                            if (Settings.DebugState)
                            {
                                ConsoleUtils.WriteLine(
                                    $"Tapping: {i}, CompensatedX: {gunPixelX / smoothing * i}, CompensatedY: {gunPixelY / smoothing * i}");
                            }

                            if (_bullet <= 0)
                            {
                                for (int j = 0; j < i; j++)
                                {
                                    _hidHandler.WriteMouseReport(_hidHandler.HidMouseHandlers[0].Mouse with
                                    {
                                        X = Convert.ToInt16(gunPixelX / smoothing * -1),
                                        Y = Convert.ToInt16(gunPixelY / smoothing * -1),
                                        Wheel = 0
                                    });

                                    var stopwatch3 = Stopwatch.StartNew();
                                    while (stopwatch3.ElapsedTicks * 1000000.0 / Stopwatch.Frequency <= sleep * 1000) ;
                                }
                            }

                            break;
                        }

                        var intX = Convert.ToInt16(gunPixelX / smoothing);
                        var intY = Convert.ToInt16(gunPixelY / smoothing);
                        
                        totalLossAdjustX = gunPixelX / smoothing - intX;
                        totalLossAdjustY = gunPixelY / smoothing - intY;
                        
                        if (Settings.LocalCompensation && Settings.GlobalCompensation)
                        {
                            if (Settings.DebugState)
                            {
                                Console.WriteLine($"Adjusting (X: {intX}, Y: {intY})");
                            }
                            totalLocalLossAdjustX += gunPixelX / smoothing - intX;
                            totalLocalLossAdjustY += gunPixelY / smoothing - intY;

                            short abX = Convert.ToInt16(totalLocalLossAdjustX);
                            short abY = Convert.ToInt16(totalLocalLossAdjustY);
                            if (Math.Abs(totalLocalLossAdjustX) >= 1)
                            {
                                totalLocalLossAdjustX -= abX;
                                intX += abX;
                            }
    
                            if (Math.Abs(totalLocalLossAdjustY) >= 1)
                            {
                                totalLocalLossAdjustY -= abY;
                                intY += abY;
                            }

                            if (Settings.DebugState)
                            {
                                Console.WriteLine($"Compensation Adjustment (X: {intX}, Y: {intY} | abX: {abX}, abY: {abY} | TLossAdjustX: {totalLocalLossAdjustX}, TLossAdjustY: {totalLocalLossAdjustY})");
                            }
                        }

                        _hidHandler.WriteMouseReport(_hidHandler.HidMouseHandlers[0].Mouse with
                        {
                            X = intX,
                            Y = intY,
                            Wheel = 0
                        });

                        var stopwatch = Stopwatch.StartNew();
                        while (stopwatch.ElapsedTicks * 1000000.0 / Stopwatch.Frequency <= sleep * 1000) ;
                    }

                    if (Settings.GlobalCompensation && !Settings.LocalCompensation)
                    {
                        if ((!_hidHandler.HidMouseHandlers[0].Mouse.LeftButton ||
                             !_hidHandler.HidMouseHandlers[0].Mouse.RightButton) && Settings.Tapping)
                        {
                            continue;
                        }
                    
                        var adjustedX = totalLossAdjustX * smoothing;
                        var adjustedY = totalLossAdjustY * smoothing;
                    
                        if (Settings.DebugState)
                        {
                            ConsoleUtils.WriteLine($"TotalLossX: {totalLossAdjustX}, TotalLossY: {totalLossAdjustY}");
                            ConsoleUtils.WriteLine(
                                $"AdjustedX: {adjustedX}, AdjustedY: {adjustedY}, Multiplier: {smoothing}");
                            ConsoleUtils.WriteLine(
                                $"Lost: X: {adjustedX - (totalLossAdjustX * smoothing)} Y: {adjustedY - (totalLossAdjustY * smoothing)}\n");
                        }

                        _hidHandler.WriteMouseReport(_hidHandler.HidMouseHandlers[0].Mouse with
                        {
                            X = adjustedX,
                            Y = adjustedY,
                            Wheel = 0
                        });
                    }

                    var stopwatch2 = Stopwatch.StartNew();
                    while (stopwatch2.ElapsedTicks * 1000000.0 / Stopwatch.Frequency <= timing * 1000) ;
                    _bullet++;
                }
                else
                {
                    if (!_recoilWatch.IsRunning)
                    {
                        _recoilWatch.Start();
                    }

                    if (_recoilWatch.ElapsedMilliseconds > 150)
                    {
                        _bullet = 0;
                        _reverseRandom = false;
                    }
                }
            }
        }

        public void Stop()
        {
            _state = false;
        }

        private Tuple<double, double, double> CalculatePixel(int bullet, List<Tuple<double, double>> angleTable)
        {
            var localSens = Settings.Sensitivity;

            // switch (Settings.Gun.Item1)
            // {
            //     case RustSettings.Guns.CUSTOM:
            //     case RustSettings.Guns.THOMPSON:
            //         localSens += !Settings.GunScope.Equals(RustSettings.Scope.Holo) ? .10 : 0;
            //         break;
            // }

            var lastShot = new Tuple<double, double>(0.0, 0.0);
            if (bullet != 0)
            {
                lastShot = angleTable[bullet - 1];
            }

            var tableValue = angleTable[bullet];
            
            var deltaX = tableValue.Item1 - lastShot.Item1;
            var deltaY = tableValue.Item2 - lastShot.Item2;

            var controlTime = Math.Sqrt(deltaX * deltaX + deltaY * deltaY) / 0.02;
            controlTime = (60000.0 / (double)Settings.Gun.Item3) - controlTime;
            if (bullet == 0)
            {
                controlTime = 60000.0 / (double)Settings.Gun.Item3;
            }
            
            // float anim_time = (weapon.timeToTakeMin + weapon.timeToTakeMax) * 0.5f;

            if (_hidHandler.HidKeyboardHandlers.Count > 1 && _hidHandler.HidKeyboardHandlers[1].Active)
            {
                var keyboard = _hidHandler.HidKeyboardHandlers[1];
                ConsoleUtils.WriteLine("Keyboard Path: " + keyboard.Path);
                if (keyboard.IsKeyDown(Keyboard.LinuxKeyCode.KEYLEFTCTRL))
                {
                    localSens *= 2;
                }
            }

            var divisionFactor = 1.0;
            if (Settings.GunScope == null)
            {
                var zoomFactor = 1.6666666269302368;

                var offset = Settings.Fov - (Settings.Fov / zoomFactor);
                var deductedOffset = Settings.Fov - offset;
            
                divisionFactor = 45 / deductedOffset;
            }

            if (Settings.DebugState)
            {
                ConsoleUtils.WriteLine("Division Factor: " + divisionFactor);
            }
                
            var screenMultiplier = -0.03 * (localSens) * 3.0 * (Settings.Fov / 100.0) / divisionFactor;

            var xPixels = tableValue.Item1 / screenMultiplier;
            var yPixels = tableValue.Item2 / screenMultiplier;

            return new Tuple<double, double, double>(xPixels, yPixels,
                Settings.Gun.Item1 == RustSettings.Guns.ASSAULT_RIFLE
                    ? controlTime
                    : (60000.0 / (double) Settings.Gun.Item3) - controlTime);
        }

        private void UpdateWeapon((RustSettings.Guns, RustSettings.BulletCount, RustSettings.FireRate) gun,
            double scope,
            (double, double) attachment)
        {
            List<Tuple<double, double>> angleTable = RustTables.AssaultRifle;
            foreach (var field in typeof(RustTables).GetFields())
            {
                if (gun.Item1.ToString().Equals(field.Name, StringComparison.OrdinalIgnoreCase))
                {
                    angleTable = (List<Tuple<double, double>>) field.GetValue(null)!;
                }
            }

            _weapon = new(
                angleTable,
                scope,
                attachment);
        }

        public void UpdateWeapon((RustSettings.Guns, RustSettings.BulletCount, RustSettings.FireRate) gun,
            RustSettings.Scope? scope,
            RustSettings.Attachment? attachment)
        {
            double scopeValue = 1;
            if (scope != null)
            {
                foreach (var field in typeof(RustSettings.Scopes).GetFields())
                {
                    if (scope.ToString()!.Equals(field.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        scopeValue = (double) field.GetValue(null)!;
                    }
                }
            }

            (double, double) attachmentValue = (1, 1);
            if (attachment != null)
            {
                foreach (var field in typeof(RustSettings.Attachments).GetFields())
                {
                    if (attachment.ToString()!.Equals(field.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        attachmentValue = ((double, double)) field.GetValue(null)!;
                    }
                }
            }

            UpdateWeapon(gun, scopeValue, attachmentValue);
        }

        public void ComputeRandomizationTable()
        {
            for (int i = 0; i < (int) Settings.Gun.Item2 - 1; i++)
            {
                var xRandom = Settings.RandomizationX.Item2 != 0
                    ? _random.Next(Settings.RandomizationX.Item1, Settings.RandomizationX.Item2 + 1)
                    : 0;
                var yRandom = Settings.RandomizationY.Item2 != 0
                    ? _random.Next(Settings.RandomizationY.Item1, Settings.RandomizationY.Item2 + 1)
                    : 0;

                var xBool = _random.Next() > (Int32.MaxValue / 2);
                var yBool = _random.Next() > (Int32.MaxValue / 2);

                xRandom = xBool ? xRandom : xRandom * -1;
                yRandom = yBool ? yRandom : yRandom * -1;

                var timingPercentRandomization =
                    _random.NextDouble() * (Settings.RandomizationTiming.Item2 - Settings.RandomizationTiming.Item1) +
                    Settings.RandomizationTiming.Item1;
                Settings.RandomizationTable.Add(new Tuple<int, int, double>(xRandom, yRandom,
                    timingPercentRandomization));
            }
        }

        public void UpdateGranularization(int Granularization)
        {
            if (_hidHandler != null && _hidHandler.HidMouseHandlers.Count > 0)
            {
                _hidHandler.HidMouseHandlers[0].Mouse = _hidHandler.HidMouseHandlers[0].Mouse with
                {
                    SensitivityMultiplier = Granularization
                };
            }
        }
    }
}