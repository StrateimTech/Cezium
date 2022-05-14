using System;
using System.Text.Json;
using Cezium.Rust;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cezium.Web.Front.Pages;

public class Rust : PageModel
{
    public void OnGet()
    {
    }

    public class BoolSchema
    {
        public bool Value { get; set; }
    }

    public class DoubleSchema
    {
        public double Value { get; set; }
    }

    public class IntSchema
    {
        public int Value { get; set; }
    }

    public class RandomizationSchema
    {
        public int Item1 { get; set; }
        public int Item2 { get; set; }
    }

    public class StringSchema
    {
        public string Value { get; set; }
    }

    #region Get

    public IActionResult OnGetState()
    {
        return new JsonResult(FrontHandler.RustHandler.Settings.State);
    }

    public IActionResult OnGetDebug()
    {
        return new JsonResult(FrontHandler.RustHandler.Settings.DebugState);
    }

    public IActionResult OnGetFov()
    {
        return new JsonResult(FrontHandler.RustHandler.Settings.Fov);
    }

    public IActionResult OnGetSensitivity()
    {
        return new JsonResult(FrontHandler.RustHandler.Settings.Sensitivity);
    }

    public IActionResult OnGetSmoothness()
    {
        return new JsonResult(FrontHandler.RustHandler.Settings.Smoothness);
    }

    public IActionResult OnGetHorizontal()
    {
        return new JsonResult(FrontHandler.RustHandler.Settings.RecoilModifier.Item1);
    }

    public IActionResult OnGetVertical()
    {
        return new JsonResult(FrontHandler.RustHandler.Settings.RecoilModifier.Item2);
    }

    public IActionResult OnGetInfiniteAmmo()
    {
        return new JsonResult(FrontHandler.RustHandler.Settings.InfiniteAmmo);
    }
    
    public IActionResult OnGetTapping()
    {
        return new JsonResult(FrontHandler.RustHandler.Settings.Tapping);
    }
    
    public IActionResult OnGetGlobalCompensation()
    {
        return new JsonResult(FrontHandler.RustHandler.Settings.GlobalCompensation);
    }
    
    public IActionResult OnGetLocalCompensation()
    {
        return new JsonResult(FrontHandler.RustHandler.Settings.LocalCompensation);
    }

    public IActionResult OnGetRandomization()
    {
        return new JsonResult(FrontHandler.RustHandler.Settings.Randomization);
    }

    public IActionResult OnGetReverseRandomization()
    {
        return new JsonResult(FrontHandler.RustHandler.Settings.ReverseRandomization);
    }
    
    public IActionResult OnGetStaticRandomization()
    {
        return new JsonResult(FrontHandler.RustHandler.Settings.StaticRandomization);
    }

    public IActionResult OnGetRandomizationX()
    {
        return new JsonResult(JsonSerializer.Serialize(FrontHandler.RustHandler.Settings.RandomizationX));
    }

    public IActionResult OnGetRandomizationY()
    {
        return new JsonResult(JsonSerializer.Serialize(FrontHandler.RustHandler.Settings.RandomizationY));
    }
    
    public IActionResult OnGetRandomizationTiming()
    {
        return new JsonResult(JsonSerializer.Serialize(FrontHandler.RustHandler.Settings.RandomizationTiming));
    }

    public IActionResult OnGetGun()
    {
        return new JsonResult(FrontHandler.RustHandler.Settings.Gun.Item1.ToString());
    }

    public IActionResult OnGetScope()
    {
        if (FrontHandler.RustHandler.Settings.GunScope == null)
        {
            return new JsonResult("EmptyScope");
        }

        return new JsonResult(FrontHandler.RustHandler.Settings.GunScope.ToString());
    }

    public IActionResult OnGetAttachment()
    {
        if (FrontHandler.RustHandler.Settings.GunAttachment == null)
        {
            return new JsonResult("EmptyAttachment");
        }

        return new JsonResult(FrontHandler.RustHandler.Settings.GunAttachment.ToString());
    }

    #endregion

    #region Post

    public void OnPostState([FromBody] BoolSchema data)
    {
        FrontHandler.RustHandler.Settings.State = data.Value;
    }

    public void OnPostDebug([FromBody] BoolSchema data)
    {
        FrontHandler.RustHandler.Settings.DebugState = data.Value;
    }

    public void OnPostInfiniteAmmo([FromBody] BoolSchema data)
    {
        FrontHandler.RustHandler.Settings.InfiniteAmmo = data.Value;
    }

    public void OnPostTapping([FromBody] BoolSchema data)
    {
        FrontHandler.RustHandler.Settings.Tapping = data.Value;
    }
    
    public void OnPostGlobalCompensation([FromBody] BoolSchema data)
    {
        FrontHandler.RustHandler.Settings.GlobalCompensation = data.Value;
    }
    
    public void OnPostLocalCompensation([FromBody] BoolSchema data)
    {
        FrontHandler.RustHandler.Settings.LocalCompensation = data.Value;
    }

    public void OnPostRandomization([FromBody] BoolSchema data)
    {
        FrontHandler.RustHandler.Settings.Randomization = data.Value;
    }

    public void OnPostReverseRandomization([FromBody] BoolSchema data)
    {
        FrontHandler.RustHandler.Settings.ReverseRandomization = data.Value;
    }
    
    public void OnPostStaticRandomization([FromBody] BoolSchema data)
    {
        FrontHandler.RustHandler.Settings.StaticRandomization = data.Value;
        if (data.Value)
        {
            FrontHandler.RustHandler.ComputeRandomizationTable();
        }
    }

    public void OnPostFov([FromBody] IntSchema data)
    {
        FrontHandler.RustHandler.Settings.Fov = data.Value;
        FrontHandler.RustHandler.UpdateWeapon(FrontHandler.RustHandler.Settings.Gun,
            FrontHandler.RustHandler.Settings.GunScope, FrontHandler.RustHandler.Settings.GunAttachment);
    }

    public void OnPostSensitivity([FromBody] DoubleSchema data)
    {
        FrontHandler.RustHandler.Settings.Sensitivity = data.Value;
        FrontHandler.RustHandler.UpdateWeapon(FrontHandler.RustHandler.Settings.Gun,
            FrontHandler.RustHandler.Settings.GunScope, FrontHandler.RustHandler.Settings.GunAttachment);
    }

    public void OnPostSmoothness([FromBody] IntSchema data)
    {
        FrontHandler.RustHandler.Settings.Smoothness = data.Value;
    }

    public void OnPostHorizontal([FromBody] DoubleSchema data)
    {
        FrontHandler.RustHandler.Settings.RecoilModifier =
            new Tuple<double, double>(data.Value, FrontHandler.RustHandler.Settings.RecoilModifier.Item2);
    }

    public void OnPostVertical([FromBody] DoubleSchema data)
    {
        FrontHandler.RustHandler.Settings.RecoilModifier =
            new Tuple<double, double>(FrontHandler.RustHandler.Settings.RecoilModifier.Item1, data.Value);
    }

    public void OnPostRandomizationX([FromBody] RandomizationSchema data)
    {
        FrontHandler.RustHandler.Settings.RandomizationX = new Tuple<int, int>(data.Item1, data.Item2);
    }

    public void OnPostRandomizationY([FromBody] RandomizationSchema data)
    {
        FrontHandler.RustHandler.Settings.RandomizationY = new Tuple<int, int>(data.Item1, data.Item2);
    }
    
    public void OnPostRandomizationTiming([FromBody] RandomizationSchema data)
    {
        FrontHandler.RustHandler.Settings.RandomizationTiming = new Tuple<int, int>(data.Item1, data.Item2);
    }

    public void OnPostGun([FromBody] StringSchema data)
    {
        Enum.TryParse(data.Value, out RustSettings.Guns gun);
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

        FrontHandler.RustHandler.Settings.Gun = new(gun, bulletCount, fireRate);
        FrontHandler.RustHandler.UpdateWeapon(FrontHandler.RustHandler.Settings.Gun,
            FrontHandler.RustHandler.Settings.GunScope, FrontHandler.RustHandler.Settings.GunAttachment);
    }

    public void OnPostScope([FromBody] StringSchema data)
    {
        if (data.Value.ToLower().Contains("empty"))
        {
            FrontHandler.RustHandler.Settings.GunScope = null;
        }
        else
        {
            Enum.TryParse(data.Value, out RustSettings.Scope scope);
            FrontHandler.RustHandler.Settings.GunScope = scope;
        }

        FrontHandler.RustHandler.UpdateWeapon(FrontHandler.RustHandler.Settings.Gun,
            FrontHandler.RustHandler.Settings.GunScope, FrontHandler.RustHandler.Settings.GunAttachment);
    }

    public void OnPostAttachment([FromBody] StringSchema data)
    {
        if (data.Value.ToLower().Contains("empty"))
        {
            FrontHandler.RustHandler.Settings.GunAttachment = null;
        }
        else
        {
            Enum.TryParse(data.Value, out RustSettings.Attachment attachment);
            FrontHandler.RustHandler.Settings.GunAttachment = attachment;
        }

        FrontHandler.RustHandler.UpdateWeapon(FrontHandler.RustHandler.Settings.Gun,
            FrontHandler.RustHandler.Settings.GunScope, FrontHandler.RustHandler.Settings.GunAttachment);
    }

    #endregion
}