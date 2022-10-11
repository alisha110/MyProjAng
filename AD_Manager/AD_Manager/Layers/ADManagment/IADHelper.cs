using AD_Manager.Layers.Model;
using System.DirectoryServices;

namespace AD_Manager.Layers.ADManagment
{
    public interface IADHelper
    {
        DirectoryEntry GetDirectoryEntry(string? LdapConnection);
        DirectoryEntry CreateUser(AdUserDTO userDTO, out string message);
        Boolean ActivateUser(string username, out string message);
        Boolean DeactivateUser(string username, out string message);
        DirectoryEntry? FindUser(string CN);
    }
}
