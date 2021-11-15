using dnlib.DotNet;

namespace Server.Obfuscation
{
    public interface IObfuscation
    {
        public void Handle(ModuleDefMD md);
    }
}