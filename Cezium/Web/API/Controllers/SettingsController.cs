using System;
using System.Collections.Generic;
using System.Linq;
using Cezium.Rust;
using Microsoft.AspNetCore.Mvc;

namespace Cezium.Web.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SettingsController : Controller
{
    [HttpGet("/api/settings/rust/Sensitivity/")]
    public double GetSensitivity()
    {
        return ApiHandler.RustHandler.Settings.Sensitivity;
    }

    [HttpPost("/api/settings/rust/Sensitivity/")]
    public void SetSensitivity(double sensitivity)
    {
        ApiHandler.RustHandler.Settings.Sensitivity = sensitivity;
        ApiHandler.RustHandler.UpdateWeapon(ApiHandler.RustHandler.Settings.Gun,
            ApiHandler.RustHandler.Settings.GunScope, ApiHandler.RustHandler.Settings.GunAttachment);
    }

    [HttpGet("/api/settings/rust/Smoothness/")]
    public int GetSmoothness()
    {
        return ApiHandler.RustHandler.Settings.Smoothness;
    }

    [HttpPost("/api/settings/rust/Smoothness/")]
    public void SetSmoothness(int smoothness)
    {
        ApiHandler.RustHandler.Settings.Smoothness = smoothness;
        ApiHandler.RustHandler.UpdateWeapon(ApiHandler.RustHandler.Settings.Gun,
            ApiHandler.RustHandler.Settings.GunScope, ApiHandler.RustHandler.Settings.GunAttachment);
    }

    [HttpGet("/api/settings/rust/Fov/")]
    public int GetFov()
    {
        return ApiHandler.RustHandler.Settings.Fov;
    }

    [HttpPost("/api/settings/rust/Fov/")]
    public void SetFov(int fov)
    {
        ApiHandler.RustHandler.Settings.Fov = fov;
        ApiHandler.RustHandler.UpdateWeapon(ApiHandler.RustHandler.Settings.Gun,
            ApiHandler.RustHandler.Settings.GunScope, ApiHandler.RustHandler.Settings.GunAttachment);
    }

    [HttpGet("/api/settings/rust/State/")]
    public bool GetState()
    {
        return ApiHandler.RustHandler.Settings.State;
    }

    [HttpPost("/api/settings/rust/State/")]
    public void SetState(bool state)
    {
        ApiHandler.RustHandler.Settings.State = state;
    }

    [HttpGet("/api/settings/rust/Debug/")]
    public bool GetDebug()
    {
        return ApiHandler.RustHandler.Settings.DebugState;
    }

    [HttpPost("/api/settings/rust/Debug/")]
    public void SetDebug(bool debug)
    {
        ApiHandler.RustHandler.Settings.DebugState = debug;
    }

    [HttpGet("/api/settings/rust/Attachment/")]
    public string GetAttachment()
    {
        return ApiHandler.RustHandler.Settings.GunAttachment.ToString();
    }

    [HttpPost("/api/settings/rust/Attachment/")]
    public void SetAttachment(RustSettings.Attachment? attachment)
    {
        if (attachment == null)
        {
            ApiHandler.RustHandler.Settings.GunAttachment = null;
        }
        else
        {
            ApiHandler.RustHandler.Settings.GunAttachment = attachment;
        }

        ApiHandler.RustHandler.UpdateWeapon(ApiHandler.RustHandler.Settings.Gun,
            ApiHandler.RustHandler.Settings.GunScope, ApiHandler.RustHandler.Settings.GunAttachment);
    }

    [HttpGet("/api/settings/rust/Scope/")]
    public string GetScope()
    {
        return ApiHandler.RustHandler.Settings.GunScope.ToString();
    }

    [HttpPost("/api/settings/rust/Scope/")]
    public void SetScope(RustSettings.Scope? scope)
    {
        if (scope == null)
        {
            ApiHandler.RustHandler.Settings.GunScope = null;
        }
        else
        {
            ApiHandler.RustHandler.Settings.GunScope = scope;
        }

        ApiHandler.RustHandler.UpdateWeapon(ApiHandler.RustHandler.Settings.Gun,
            ApiHandler.RustHandler.Settings.GunScope, ApiHandler.RustHandler.Settings.GunAttachment);
    }

    [HttpGet("/api/settings/rust/InfiniteAmmo/")]
    public bool GetInfiniteAmmo()
    {
        return ApiHandler.RustHandler.Settings.InfiniteAmmo;
    }

    [HttpPost("/api/settings/rust/InfiniteAmmo/")]
    public void SetInfiniteAmmo(bool infiniteAmmo)
    {
        ApiHandler.RustHandler.Settings.InfiniteAmmo = infiniteAmmo;
    }
    
    [HttpGet("/api/settings/rust/Tapping/")]
    public bool GetTapping()
    {
        return ApiHandler.RustHandler.Settings.Tapping;
    }

    [HttpPost("/api/settings/rust/Tapping/")]
    public void SetTapping(bool tapping)
    {
        ApiHandler.RustHandler.Settings.Tapping = tapping;
    }

    [HttpGet("/api/settings/rust/Randomization/")]
    public bool GetRandomization()
    {
        return ApiHandler.RustHandler.Settings.Randomization;
    }

    [HttpPost("/api/settings/rust/Randomization/")]
    public void SetRandomization(bool randomization)
    {
        ApiHandler.RustHandler.Settings.Randomization = randomization;
    }

    [HttpGet("/api/settings/rust/ReverseRandomization/")]
    public bool GetReverseRandomization()
    {
        return ApiHandler.RustHandler.Settings.ReverseRandomization;
    }

    [HttpPost("/api/settings/rust/ReverseRandomization/")]
    public void SetReverseRandomization(bool reverseRandomization)
    {
        ApiHandler.RustHandler.Settings.ReverseRandomization = reverseRandomization;
    }

    [HttpGet("/api/settings/rust/RandomizationX/")]
    public Tuple<int, int> GetRandomizationAmountX()
    {
        return ApiHandler.RustHandler.Settings.RandomizationX;
    }

    [HttpGet("/api/settings/rust/RandomizationY/")]
    public Tuple<int, int> GetRandomizationAmountY()
    {
        return ApiHandler.RustHandler.Settings.RandomizationY;
    }

    [HttpPost("/api/settings/rust/RandomizationX/")]
    public void SetRandomizationAmountX(int min, int max)
    {
        ApiHandler.RustHandler.Settings.RandomizationX = new Tuple<int, int>(min, max);
    }

    [HttpPost("/api/settings/rust/RandomizationY/")]
    public void SetRandomizationAmountY(int min, int max)
    {
        ApiHandler.RustHandler.Settings.RandomizationY = new Tuple<int, int>(min, max);
    }
    
    [HttpGet("/api/settings/rust/HorizontalModifier/")]
    public double GetHorizontalRecoilModifier()
    {
        return ApiHandler.RustHandler.Settings.RecoilModifier.Item1;
    }
    
    [HttpGet("/api/settings/rust/VerticalModifier/")]
    public double GetVerticalRecoilModifier()
    {
        return ApiHandler.RustHandler.Settings.RecoilModifier.Item2;
    }
    
    [HttpPost("/api/settings/rust/HorizontalModifier/")]
    public void SetHorizontalRecoilModifier(double x)
    {
        ApiHandler.RustHandler.Settings.RecoilModifier = new Tuple<double, double>(x, ApiHandler.RustHandler.Settings.RecoilModifier.Item2);
    }
    
    [HttpPost("/api/settings/rust/VerticalModifier/")]
    public void SetVerticalRecoilModifier(double y)
    {
        ApiHandler.RustHandler.Settings.RecoilModifier = new Tuple<double, double>(ApiHandler.RustHandler.Settings.RecoilModifier.Item1, y);
    }

    [HttpPost("/api/settings/rust/Gun/")]
    public void SetGun(RustSettings.Guns gun)
    {
        var bulletCount = RustSettings.BulletCount.ASSAULT_RIFLE;
        if (Enum.TryParse(gun.ToString(), true, out RustSettings.BulletCount bulletCountEnum))
        {
            bulletCount = bulletCountEnum;
        }

        var fireRate = RustSettings.FireRate.ASSAULT_RIFLE;
        if (Enum.TryParse(gun.ToString(), true, out RustSettings.FireRate fireRateEnum))
        {
            fireRate = fireRateEnum;
        }

        ApiHandler.RustHandler.Settings.Gun = new(gun, bulletCount, fireRate);
        ApiHandler.RustHandler.UpdateWeapon(ApiHandler.RustHandler.Settings.Gun,
            ApiHandler.RustHandler.Settings.GunScope, ApiHandler.RustHandler.Settings.GunAttachment);
    }

    [HttpGet("/api/settings/rust/Gun/")]
    public string GetGun()
    {
        return ApiHandler.RustHandler.Settings.Gun.Item1.ToString();
    }
    
    
    [HttpGet("/api/settings/mouse/InvertMouseX/{id}")]
    public bool? GetInvertMouseX(int id)
    {
        if (ApiHandler.HidHandler.HidMouseHandlers.Count <= id)
        {
            var mouseHandler = ApiHandler.HidHandler.HidMouseHandlers[id];
            return mouseHandler.Mouse.InvertMouseX;
        }
        return null;
    }
    
    [HttpGet("/api/settings/mouse/InvertMouseY/{id}")]
    public bool? GetInvertMouseY(int id)
    {
        if (ApiHandler.HidHandler.HidMouseHandlers.Count <= id)
        {
            var mouseHandler = ApiHandler.HidHandler.HidMouseHandlers[id];
            return mouseHandler.Mouse.InvertMouseY;
        }
        return null;
    }
    
    [HttpGet("/api/settings/mouse/InvertMouseWheel/{id}")]
    public bool? GetInvertMouseWheel(int id)
    {
        if (ApiHandler.HidHandler.HidMouseHandlers.Count <= id)
        {
            var mouseHandler = ApiHandler.HidHandler.HidMouseHandlers[id];
            return mouseHandler.Mouse.InvertMouseWheel;
        }
        return null;
    }
    
    [HttpPost("/api/settings/mouse/InvertMouseX/{id}/{state}")]
    public void SetInvertMouseX(int id, bool state)
    {
        if (ApiHandler.HidHandler.HidMouseHandlers.Count <= id)
        {
            var mouseHandler = ApiHandler.HidHandler.HidMouseHandlers[id];
            mouseHandler.Mouse.InvertMouseX = state;
        }
    }
    
    [HttpPost("/api/settings/mouse/InvertMouseY/{id}/{state}")]
    public void SetInvertMouseY(int id, bool state)
    {
        if (ApiHandler.HidHandler.HidMouseHandlers.Count <= id)
        {
            var mouseHandler = ApiHandler.HidHandler.HidMouseHandlers[id];
            mouseHandler.Mouse.InvertMouseY = state;
        }
    }
    
    [HttpPost("/api/settings/mouse/InvertMouseWheel/{id}/{state}")]
    public void SetInvertMouseWheel(int id, bool state)
    {
        if (ApiHandler.HidHandler.HidMouseHandlers.Count <= id)
        {
            var mouseHandler = ApiHandler.HidHandler.HidMouseHandlers[id];
            mouseHandler.Mouse.InvertMouseWheel = state;
        }
    }
}