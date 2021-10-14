using System;
using System.Collections.Generic;
using System.Threading;
using Main.HID;

namespace Main.Rust
{
    public class RustHandler
    {
        private readonly Settings _settings;
        
        private readonly HidHandler _hidHandler;
        
        /// <summary>
        /// The current bullet that the gun is on
        /// </summary>
        private int _bullet = 0;
        
        /// <summary>
        /// <param name="gunType">provides a enum from on the current gun and its name</param>
        /// <param name="xyTable">xy movement table of the current weapon</param>
        /// <param name="fireRate">the current gun's fire rate in rounds per minute</param>
        /// <param name="maxBullets">the current gun's maximum bullets in a magazine</param>
        /// <param name="scopeMultiplier">the gun's scope multiplier applied to when calculating the gun's pixel table</param>
        /// <param name="attachmentMultiplier">the gun's attachment multiplier applied to when calculating the gun's pixel table</param>
        /// </summary>
        public (Settings.RustSettings.Guns, List<Tuple<double, double>> xyTable, Settings.RustSettings.FireRate fireRate, int maxBullets, double scopeMultiplier, double attachmentMultiplier) CurrentWeapon
                        = new(Settings.RustSettings.Guns.ASSAULTTIFLE, RustTables.AssaultRifle, Settings.RustSettings.FireRate.ASSAULTRIFLE, Settings.RustSettings.BulletCounts.ASSAULTRIFLE, Settings.RustSettings.Scopes.Default, Settings.RustSettings.Attachments.Default);

        /// <summary>
        /// <param name="Tuple">XY Pixel coordinates</param>
        /// </summary>
        public List<Tuple<double, double>>? PixelTable;
        
        public RustHandler(Settings settings, HidHandler hidHandler)
        {
            _settings = settings;
            _hidHandler = hidHandler;
            
            PixelTable = CalculatePixelTables(CurrentWeapon.Item2);
        }

        public void Start()
        {
            var rustApiThreadHandler = new Thread(() => new RustAPI(this, _settings))
            {
                            IsBackground = true
            };
            rustApiThreadHandler.Start();
            
            while (true)
            {
                if(!_settings.Rust.State || !_hidHandler.Mouse.LeftButton || !_hidHandler.Mouse.RightButton)
                    Thread.Sleep(1);
                if(!_settings.Rust.State)
                    continue;

                if (_hidHandler.Mouse.LeftButton && _hidHandler.Mouse.RightButton)
                {
                    if (PixelTable == null)
                        continue;
                    if (_bullet >= CurrentWeapon.Item4 - 1)
                        _bullet = 0;
                        
                    var gunPixelX = PixelTable[_bullet].Item1 * CurrentWeapon.Item5 * CurrentWeapon.Item6;
                    var gunPixelY = PixelTable[_bullet].Item2 * CurrentWeapon.Item5 * CurrentWeapon.Item6;
                    
                    var delay = 60000.0 / (int)CurrentWeapon.Item3;
                    var smoothing = _settings.Rust.Smoothness;

                    for (int i = 0; i < smoothing; i++)
                    {
                        if(!_hidHandler.Mouse.LeftButton || !_hidHandler.Mouse.LeftButton)
                            continue;
                        _hidHandler.WriteMouseReport(_hidHandler.Mouse with
                        {
                                        X = Convert.ToInt32(gunPixelX / smoothing),
                                        Y = Convert.ToInt32(gunPixelY / smoothing),
                                        Wheel = 0
                        });
                        Thread.Sleep((int)(delay / smoothing));
                    }
                    _bullet++;
                }
                else
                {
                    _bullet = 0;
                }
            }
        }

        public List<Tuple<double, double>> CalculatePixelTables(List<Tuple<double, double>> angleTable)
        {
            var pixelTable = new List<Tuple<double, double>>();
            var lastShot = new Tuple<double, double>(0.0, 0.0);

            var localSens = _settings.Rust.Sensitivity;
            localSens += (CurrentWeapon.Item1.Equals(Settings.RustSettings.Guns.THOMPSON) && !CurrentWeapon.Item5.Equals(Settings.RustSettings.Scopes.HoloSight) ? .10 : 0);
            
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
            double scopeValue = 0;
            double attachmentValue = 0;
            var gunEnum = (Settings.RustSettings.Guns)Enum.Parse(typeof(Settings.RustSettings.Guns), gunString.ToUpper());
            foreach (var field in typeof(Settings.RustSettings.Scopes).GetFields())
            {
                if (scopeString.Equals(field.Name, StringComparison.OrdinalIgnoreCase))
                {
                    scopeValue = (double)field.GetValue(null)!;
                }
            }
            if (scopeValue == 0)
                scopeValue = 1;
            foreach (var field in typeof(Settings.RustSettings.Attachments).GetFields())
            {
                if (attachmentString.Equals(field.Name, StringComparison.OrdinalIgnoreCase))
                {
                    attachmentValue = (double)field.GetValue(null)!;
                }
            }
            if (attachmentValue == 0)
                attachmentValue = 1;

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