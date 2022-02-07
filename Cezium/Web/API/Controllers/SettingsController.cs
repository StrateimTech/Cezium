using System;
using System.Collections.Generic;
using System.Linq;
using Cezium.Rust;
using HID_API;
using Microsoft.AspNetCore.Mvc;

namespace Cezium.Web.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SettingsController : Controller
{
    [HttpGet("/api/settings/Sensitivity/")]
    public double GetSensitivity()
    {
        return ApiHandler.RustHandler.Settings.Sensitivity;
    }

    [HttpPost("/api/settings/Sensitivity/")]
    public void SetSensitivity(double sensitivity)
    {
        ApiHandler.RustHandler.Settings.Sensitivity = sensitivity;
        ApiHandler.RustHandler.UpdateWeapon(ApiHandler.RustHandler.Settings.Gun, ApiHandler.RustHandler.Settings.GunScope, ApiHandler.RustHandler.Settings.GunAttachment);
    }
    
    [HttpGet("/api/settings/Smoothness/")]
    public int GetSmoothness()
    {
        return ApiHandler.RustHandler.Settings.Smoothness;
    }

    [HttpPost("/api/settings/Smoothness/")]
    public void SetSmoothness(int smoothness)
    {
        ApiHandler.RustHandler.Settings.Smoothness = smoothness;
        ApiHandler.RustHandler.UpdateWeapon(ApiHandler.RustHandler.Settings.Gun, ApiHandler.RustHandler.Settings.GunScope, ApiHandler.RustHandler.Settings.GunAttachment);
    }
    
    [HttpGet("/api/settings/Fov/")]
    public int GetFov()
    {
        return ApiHandler.RustHandler.Settings.Fov;
    }

    [HttpPost("/api/settings/Fov/")]
    public void SetFov(int fov)
    {
        ApiHandler.RustHandler.Settings.Fov = fov;
        ApiHandler.RustHandler.UpdateWeapon(ApiHandler.RustHandler.Settings.Gun, ApiHandler.RustHandler.Settings.GunScope, ApiHandler.RustHandler.Settings.GunAttachment);
    }

    [HttpGet("/api/settings/State/")]
    public bool GetState()
    {
        return ApiHandler.RustHandler.Settings.State;
    }

    [HttpPost("/api/settings/State/")]
    public void SetState(bool state)
    {
        ApiHandler.RustHandler.Settings.State = state;
    }

    [HttpGet("/api/settings/Debug/")]
    public bool GetDebug()
    {
        return ApiHandler.RustHandler.Settings.DebugState;
    }

    [HttpPost("/api/settings/Debug/")]
    public void SetDebug(bool debug)
    {
        ApiHandler.RustHandler.Settings.DebugState = debug;
    }

    [HttpGet("/api/settings/Attachment/")]
    public string GetAttachment()
    {
        return ApiHandler.RustHandler.Settings.GunAttachment.ToString();
    }

    [HttpPost("/api/settings/Attachment/")]
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
        ApiHandler.RustHandler.UpdateWeapon(ApiHandler.RustHandler.Settings.Gun, ApiHandler.RustHandler.Settings.GunScope, ApiHandler.RustHandler.Settings.GunAttachment);
    }

    [HttpGet("/api/settings/grab/Attachments/")]
    public List<RustSettings.Attachment> GetAllAttachments()
    {
        return Enum.GetValues(typeof(RustSettings.Attachment))
            .Cast<RustSettings.Attachment>()
            .ToList();
    }

    [HttpGet("/api/settings/Scope/")]
    public string GetScope()
    {
        return ApiHandler.RustHandler.Settings.GunScope.ToString();
    }

    [HttpPost("/api/settings/Scope/")]
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
        ApiHandler.RustHandler.UpdateWeapon(ApiHandler.RustHandler.Settings.Gun, ApiHandler.RustHandler.Settings.GunScope, ApiHandler.RustHandler.Settings.GunAttachment);
    }

    [HttpGet("/api/settings/grab/Scopes/")]
    public List<RustSettings.Scope> GetAllScopes()
    {
        return Enum.GetValues(typeof(RustSettings.Scope))
            .Cast<RustSettings.Scope>()
            .ToList();
    }

    [HttpGet("/api/settings/InfiniteAmmo/")]
    public bool GetInfiniteAmmo()
    {
        return ApiHandler.RustHandler.Settings.InfiniteAmmo;
    }

    [HttpPost("/api/settings/InfiniteAmmo/")]
    public void SetInfiniteAmmo(bool infiniteAmmo)
    {
        ApiHandler.RustHandler.Settings.InfiniteAmmo = infiniteAmmo;
    }

    [HttpGet("/api/settings/Randomization/")]
    public bool GetRandomization()
    {
        return ApiHandler.RustHandler.Settings.Randomization;
    }

    [HttpPost("/api/settings/Randomization/")]
    public void SetRandomization(bool randomization)
    {
        ApiHandler.RustHandler.Settings.Randomization = randomization;
    }

    [HttpGet("/api/settings/ReverseRandomization/")]
    public bool GetReverseRandomization()
    {
        return ApiHandler.RustHandler.Settings.ReverseRandomization;
    }

    [HttpPost("/api/settings/ReverseRandomization/")]
    public void SetReverseRandomization(bool reverseRandomization)
    {
        ApiHandler.RustHandler.Settings.ReverseRandomization = reverseRandomization;
    }

    [HttpGet("/api/settings/RandomizationAmountX/")]
    public Tuple<int, int> GetRandomizationAmountX()
    {
        return ApiHandler.RustHandler.Settings.RandomizationAmountX;
    }

    [HttpGet("/api/settings/RandomizationAmountY/")]
    public Tuple<int, int> GetRandomizationAmountY()
    {
        return ApiHandler.RustHandler.Settings.RandomizationAmountY;
    }

    [HttpPost("/api/settings/RandomizationAmountX/")]
    public void SetRandomizationAmountX(int min, int max)
    {
        ApiHandler.RustHandler.Settings.RandomizationAmountX = new Tuple<int, int>(min, max);
    }

    [HttpPost("/api/settings/RandomizationAmountY/")]
    public void SetRandomizationAmountY(int min, int max)
    {
        ApiHandler.RustHandler.Settings.RandomizationAmountY = new Tuple<int, int>(min, max);
    }

    [HttpGet("/api/settings/RecoilModifier/")]
    public Tuple<double, double> GetRecoilModifier()
    {
        return ApiHandler.RustHandler.Settings.RecoilModifier;
    }

    [HttpPost("/api/settings/RecoilModifier/")]
    public void SetRecoilModifier(double x, double y)
    {
        ApiHandler.RustHandler.Settings.RecoilModifier = new Tuple<double, double>(x, y);
    }

    [HttpPost("/api/settings/Gun/")]
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
        ApiHandler.RustHandler.UpdateWeapon(ApiHandler.RustHandler.Settings.Gun, ApiHandler.RustHandler.Settings.GunScope, ApiHandler.RustHandler.Settings.GunAttachment);
    }

    [HttpGet("/api/settings/Gun/")]
    public string GetGun()
    {
        return ApiHandler.RustHandler.Settings.Gun.Item1.ToString();
    }
}