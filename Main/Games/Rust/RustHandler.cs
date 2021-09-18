using System;
using System.Collections.Generic;
using System.Threading;
using Main.HID;
using Main.Utils;

namespace Main.Games.Rust
{
    public class RustHandler
    {
        private readonly RustTables _gunTables = new();
        
        private readonly Settings _settings;
        
        private readonly HidHandler _hidHandler;
        private readonly Random _random = new Random();

        public enum GunRpm
        {
            AssaultRifle = 450,
            Custom = 600,
            Lr300 = 500,
            M249 = 500,
            Mp5 = 600,
            Thompson = 462
        }
        
        private struct GunBullet
        {
            public const int AssaultRifle = 30;
            public const int Custom = 24;
            public const int Lr300 = 30;
            public const int M249 = 100;
            public const int Mp5 = 30;
            public const int Thompson = 20;
        }
        
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
        public (Settings.RustSettings.Guns, List<Tuple<double, double>> xyTable, GunRpm fireRate, int maxBullets, double scopeMultiplier, double attachmentMultiplier) CurrentWeapon;
        
        /// <summary>
        /// <param name="Tuple">XY Pixel coordinates</param>
        /// </summary>
        public List<Tuple<double, double>> PixelTable;
        
        public RustHandler(Settings settings, HidHandler hidHandler)
        {
            _settings = settings;
            _hidHandler = hidHandler;
            _bullet = 2;
        }

        public void Start()
        {
            while (true)
            {
                if(!_settings.Rust.State)
                    continue;
                
                if (_hidHandler.LeftButton && _hidHandler.RightButton)
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
                        if(!_hidHandler.LeftButton || !_hidHandler.RightButton)
                            continue;
                        // Console.WriteLine($"MODDED: x={(gunPixelX / smoothing)} y={(gunPixelY / smoothing)} left={_hidHandler.LeftButton} right={_hidHandler.RightButton} middle={_hidHandler.MiddleButton} bullet={_bullet} smoothing={smoothing}");
                        FileUtils.write_mouse_report(_hidHandler.HidFileStream, BitUtils.ToByte(_hidHandler.ButtonBitArray), new[] {Convert.ToSByte((gunPixelX / smoothing)), Convert.ToSByte((gunPixelY / smoothing))});
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
            localSens += (CurrentWeapon.Item1.Equals(Settings.RustSettings.Guns.Thompson) && !CurrentWeapon.Item5.Equals(Settings.RustSettings.Scopes.HoloSight) ? .10 : 0);
            
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
            Settings.RustSettings.Guns gunEnum;
            double scopeValue;
            double attachmentValue;
            
            switch (gunString.ToLower())
            {
                case "assaultrifle":
                    gunEnum = Settings.RustSettings.Guns.AssaultRifle;
                    break;
                case "m249":
                    gunEnum = Settings.RustSettings.Guns.M249;
                    break;
                case "mp5":
                    gunEnum = Settings.RustSettings.Guns.Mp5;
                    break;
                case "lr300":
                    gunEnum = Settings.RustSettings.Guns.Lr300;
                    break;
                case "custom":
                    gunEnum = Settings.RustSettings.Guns.Custom;
                    break;
                case "thompson":
                    gunEnum = Settings.RustSettings.Guns.Thompson;
                    break;
                default:
                    gunEnum = Settings.RustSettings.Guns.AssaultRifle;
                    break;
            }
            
            switch (scopeString.ToLower())
            {
                case "zoom8scope":
                    scopeValue = Settings.RustSettings.Scopes.Zoom8Scope;
                    break;
                case "zoom16scope":
                    scopeValue = Settings.RustSettings.Scopes.Zoom16Scope;
                    break;
                case "handmadesight":
                    scopeValue = Settings.RustSettings.Scopes.HandmadeSight;
                    break;
                case "holosight":
                    scopeValue = Settings.RustSettings.Scopes.HoloSight;
                    break;
                default:
                    scopeValue = 1.0;
                    break;
            }
            
            switch (attachmentString.ToLower())
            {
                case "muzzleboost":
                    attachmentValue = Settings.RustSettings.Attachments.MuzzleBoost;
                    break;
                case "muzzlebrake":
                    attachmentValue = Settings.RustSettings.Attachments.MuzzleBrake;
                    break;
                case "silencer":
                    attachmentValue = Settings.RustSettings.Attachments.Silencer;
                    break;
                default:
                    attachmentValue = 1.0;
                    break;
            }
            SetGun(gunEnum, scopeValue, attachmentValue);
            PixelTable = CalculatePixelTables(CurrentWeapon.Item2);
        }
        
        private void SetGun(Settings.RustSettings.Guns gun, double scope, double attachment)
        {
            switch (gun)
            {
                case Settings.RustSettings.Guns.AssaultRifle:
                    CurrentWeapon = new(Settings.RustSettings.Guns.AssaultRifle, _gunTables.AssaultRifle, GunRpm.AssaultRifle, GunBullet.AssaultRifle, scope, attachment);
                    break;
                case Settings.RustSettings.Guns.M249:
                    CurrentWeapon = new(Settings.RustSettings.Guns.M249, _gunTables.M249, GunRpm.M249, GunBullet.M249, scope, attachment);
                    break;
                case Settings.RustSettings.Guns.Lr300:
                    CurrentWeapon = new(Settings.RustSettings.Guns.Lr300, _gunTables.Lr300, GunRpm.Lr300, GunBullet.Lr300, scope, attachment);
                    break;
                case Settings.RustSettings.Guns.Mp5:
                    CurrentWeapon = new(Settings.RustSettings.Guns.Mp5, _gunTables.Mp5, GunRpm.Mp5, GunBullet.Mp5, scope, attachment);
                    break;
                case Settings.RustSettings.Guns.Custom:
                    CurrentWeapon = new(Settings.RustSettings.Guns.Custom, _gunTables.Custom, GunRpm.Custom, GunBullet.Custom, scope, attachment);
                    break;
                case Settings.RustSettings.Guns.Thompson:
                    CurrentWeapon = new(Settings.RustSettings.Guns.Thompson, _gunTables.Thompson, GunRpm.Thompson, GunBullet.Thompson, scope, attachment);
                    break;
                default:
                    CurrentWeapon = new(Settings.RustSettings.Guns.AssaultRifle, _gunTables.AssaultRifle, GunRpm.AssaultRifle, GunBullet.AssaultRifle, scope, attachment);
                    break;
            }
        }
    }
}