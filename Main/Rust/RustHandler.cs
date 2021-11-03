using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Main.HID;
using Main.Utils;

namespace Main.Rust
{
    public class RustHandler
    {
        private readonly Settings _settings;
        
        private readonly HidHandler _hidHandler;

        public readonly RustApiHandler RustApiHandler;
        
        /// <summary>
        /// The current bullet that the gun is on
        /// </summary>
        private int _bullet = 0;
        
        /// <summary>
        /// <param name="gunType">provides a enum from on the current gun and its name</param>
        /// <param name="xyTable">xy movement table of the current weapon</param>
        /// <param name="fireRate">the current gun's fire rate in rounds per minute</param>
        /// <param name="maxBullets">the current gun's maximum bullets in a magazine</param>
        /// <param name="scope">the gun's scope multiplier applied to when calculating the gun's pixel table</param>
        /// <param name="attachment">the gun's attachment (multiplier, and timings) applied to when calculating the gun's pixel table and delays between shots</param>
        /// </summary>
        public (Settings.RustSettings.Guns, List<Tuple<double, double>> xyTable, Settings.RustSettings.FireRate fireRate, int maxBullets, double scope, (double, double) attachment) CurrentWeapon
                        = new(Settings.RustSettings.Guns.ASSAULTRIFLE, RustTables.AssaultRifle, Settings.RustSettings.FireRate.ASSAULTRIFLE, (int)Settings.RustSettings.BulletCounts.ASSAULTRIFLE, 1, (1, 1));

        /// <summary>
        /// <param name="Tuple">Pixels coordinates for each relative mouse movement for every bullet</param>
        /// </summary>
        public List<Tuple<double, double>>? PixelTable;

        private Tuple<int, int> _lastRandomization = new(0, 0);
        private bool _reverseRandom = false;
        
        private readonly Random _random = new Random();
        
        public RustHandler(Settings settings, HidHandler hidHandler)
        {
            _settings = settings;
            _hidHandler = hidHandler;
            
            PixelTable = CalculatePixelTables(CurrentWeapon.Item2);
            RustApiHandler = new RustApiHandler(this, settings);
        }

        public void Start()
        {
            while (true)
            {
                if(!_settings.Rust.State || !_hidHandler.HidMouseHandler.Mouse.LeftButton || !_hidHandler.HidMouseHandler.Mouse.RightButton)
                    Thread.Sleep(1);
                if(!_settings.Rust.State)
                    continue;

                if (_hidHandler.HidMouseHandler.Mouse.LeftButton && _hidHandler.HidMouseHandler.Mouse.RightButton)
                {
                    if (PixelTable == null)
                        continue;
                    if (_bullet >= CurrentWeapon.Item4 - 1)
                    {
                        if (_settings.Rust.AmmoReset)
                            _bullet = 0;
                        else
                            continue;
                    }

                    var pixelXTable = PixelTable[_bullet].Item1;
                    var pixelYTable = PixelTable[_bullet].Item2;

                    pixelXTable *= _settings.Rust.RecoilModifier.Item1;
                    pixelYTable *= _settings.Rust.RecoilModifier.Item2;

                    var gunPixelX = pixelXTable * CurrentWeapon.Item5 * CurrentWeapon.Item6.Item1;
                    var gunPixelY = pixelYTable * CurrentWeapon.Item5 * CurrentWeapon.Item6.Item1;

                    if (_settings.Rust.ReverseRandomization && _settings.Rust.Randomization && _reverseRandom)
                    {
                        var invertedLastX = _lastRandomization.Item1 * -1;
                        var invertedLastY = _lastRandomization.Item2 * -1;
                        
                        ConsoleUtils.WriteCentered($"InvertedLastX: {invertedLastX}, InvertedLastY: {invertedLastY}");

                        gunPixelX += invertedLastX;
                        gunPixelY += invertedLastY;
                    }
                    
                    if (_settings.Rust.Randomization)
                    {
                        var xRandom = _settings.Rust.RandomizationAmountX.Item2 != 0 ? _random.Next(_settings.Rust.RandomizationAmountX.Item1, _settings.Rust.RandomizationAmountX.Item2) : 0;
                        var yRandom = _settings.Rust.RandomizationAmountY.Item2 != 0 ? _random.Next(_settings.Rust.RandomizationAmountY.Item1, _settings.Rust.RandomizationAmountY.Item2) : 0;
                        
                        var xBool = _random.Next() > (Int32.MaxValue / 2);
                        var yBool = _random.Next() > (Int32.MaxValue / 2);
                        
                        xRandom = xBool ? xRandom : xRandom * -1;
                        yRandom = yBool ? yRandom : yRandom * -1;
                        
                        ConsoleUtils.WriteCentered($"xRandom: {xRandom}, yRandom: {yRandom}");
                        
                        gunPixelX += xRandom;
                        gunPixelY += yRandom;
                        
                        _lastRandomization = new(xRandom, yRandom);
                        _reverseRandom = true;
                    }

                    var delay = 60000.0 / (int)CurrentWeapon.Item3;
                    var smoothing = _settings.Rust.Smoothness;
                    var sleep = (delay / smoothing) * CurrentWeapon.Item6.Item2;

                    for (int i = 0; i < smoothing; i++)
                    {
                        if(!_hidHandler.HidMouseHandler.Mouse.LeftButton || !_hidHandler.HidMouseHandler.Mouse.LeftButton)
                            continue;
                        _hidHandler.WriteMouseReport(_hidHandler.HidMouseHandler.Mouse with
                        {
                                        X = Convert.ToInt32(gunPixelX / smoothing),
                                        Y = Convert.ToInt32(gunPixelY / smoothing),
                                        Wheel = 0
                        });
                    
                        var stopwatch = Stopwatch.StartNew();
                        while (stopwatch.ElapsedTicks * 1000000.0 / Stopwatch.Frequency <= sleep * 1000);
                    }
                    _bullet++;
                    _reverseRandom = false;
                }
                else
                {
                    _bullet = 0;
                    _reverseRandom = false;
                }
            }
        }

        public List<Tuple<double, double>> CalculatePixelTables(List<Tuple<double, double>> angleTable)
        {
            var pixelTable = new List<Tuple<double, double>>();
            var lastShot = new Tuple<double, double>(0.0, 0.0);

            var localSens = _settings.Rust.Sensitivity;

            switch (CurrentWeapon.Item1)
            {
                case Settings.RustSettings.Guns.THOMPSON:
                    localSens += !CurrentWeapon.Item5.Equals(Settings.RustSettings.Scopes.HoloSight) ? .10 : 0;
                    break;
                case Settings.RustSettings.Guns.CUSTOM:
                    localSens += !CurrentWeapon.Item5.Equals(Settings.RustSettings.Scopes.HoloSight) ? .10 : 0;
                    break;
            }
            
            foreach (var tableValue in angleTable)
            {
                var deltaX = tableValue.Item1 - lastShot.Item1;
                var deltaY = tableValue.Item2 - lastShot.Item2;

                var screenMultiplier = -0.03f * (localSens * 3.0f) * (_settings.Rust.Fov / 100.0f);
                
                var xPixels = (deltaX / screenMultiplier);
                var yPixels = (deltaY / screenMultiplier);
                
                var tuple = new Tuple<double, double>(xPixels, yPixels);
                    
                pixelTable.Add(tuple);
                lastShot = new(tableValue.Item1, tableValue.Item2);
            }
            return pixelTable;
        }
        
        public void UpdateGun(string gunString, string scopeString, string attachmentString)
        {
            double scopeValue = 1;
            (double, double) attachmentValue = (1, 1);
            var gunEnum = (Settings.RustSettings.Guns)Enum.Parse(typeof(Settings.RustSettings.Guns), gunString.ToUpper());
            foreach (var field in typeof(Settings.RustSettings.Scopes).GetFields())
            {
                if (scopeString.Equals(field.Name, StringComparison.OrdinalIgnoreCase))
                {
                    scopeValue = (double)field.GetValue(null)!;
                }
            }
            foreach (var field in typeof(Settings.RustSettings.Attachments).GetFields())
            {
                if (attachmentString.Equals(field.Name, StringComparison.OrdinalIgnoreCase))
                {
                    attachmentValue = ((double, double))field.GetValue(null)!;
                }
            }

            List<Tuple<double, double>> recoilTable = RustTables.AssaultRifle;
            foreach (var field in typeof(RustTables).GetFields())
            {
                if (gunString.Equals(field.Name, StringComparison.OrdinalIgnoreCase))
                {
                    recoilTable = (List<Tuple<double, double>>)field.GetValue(null)!;
                }
            }

            var fireRate = (Settings.RustSettings.FireRate)Enum.Parse(typeof(Settings.RustSettings.FireRate), gunString.ToUpper());
            var bulletCount = (int)Enum.Parse(typeof(Settings.RustSettings.BulletCounts), gunString.ToUpper());
            
            CurrentWeapon = new(
                            gunEnum, 
                            recoilTable,
                            fireRate,
                            bulletCount,
                            scopeValue, 
                            attachmentValue);
            PixelTable = CalculatePixelTables(recoilTable);
        }
    }
}