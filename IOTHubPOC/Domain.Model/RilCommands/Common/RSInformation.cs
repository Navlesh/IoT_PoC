using System;
using System.Collections.Generic;

namespace IOTMockDataSerializer.Models.Common
{
    public class RSInformation
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CreatorId { get; set; } = " BA - REGCONF";
        public string Timestamp { get; set; } = "2016-10-06T15:32:05.257809Z";
        public string ProcessingMode { get; set; } = "PRODUCTION";
        public string RUDI { get; set; } = "REF^0123456789^1234-12";
        public string NickName { get; set; } = "Cavallo";
        public string Environment { get; set; } = "PROD";

        public Device Device { get; set; } = new Device();
        public Customer Customer { get; set; } = new Customer();

        public string[] RoutingCountry { get; set; } = new string[] { "DK" };
        public string[] PrimaryLanguage { get; set; } = new string[] { "da" };
        public string[] SupplementaryLanguage { get; set; } = new string[] { "da" };
        public string[] UILanguage { get; set; } = new string[] { "da" };

        public Conformance Conformance { get; set; } = new Conformance();
        public RemoteAccess RemoteAccess { get; set; } = new RemoteAccess();
        public string Area { get; set; } = "NHS";

        public Connection Connections { get; set; } = new Connection();

        public int HeartbeatPeriod { get; set; } = 5;
    }

    public class Conformance
    {
        public string[] SupportedResourceType { get; set; } = new string[] {
                "DownloadPackageCommand",
                "HandleReferencedUploadCommand",
                "HandleEmbeddedUploadCommand",
                "PatchRSInformationCommand",
                "StartApplicationTunnelingCommand",
                "StopApplicationTunnelingCommand",
                "SetRSInformationCommand",
                "CommandAcknowledgement",
                "StoragePathRequest",
                "StoragePathResponse",
                "SynchronizePackagesCommand",
                "GetDataCommand",
                "RebootCommand",
                "DeletePackageCommand",
                "ExpirePackageCommand"
        };

        public string[] SupportedDistributionContentClass { get; set; } = new string[] {
            "eLibraryPackage",
            "SWIDPackage"
        };
    }

    public class RemoteAccess
    {
        public string IPAddress { get; set; } = "172.18.12.30";
        public string Hostname { get; set; } = "cavallosrv";
        public string[] Application { get; set; } = new string[] { "VNC" };
    }

    public class Connection
    {
        public ProxySettings ProxySettings { get; set; } = new ProxySettings();
        public Uplink Uplink { get; set; } = new Uplink();
        public List<Downlink> Downlink { get; set; } = new List<Downlink> { new Downlink() };
    }

    public class ProxySettings
    {
        public string Host { get; set; } = "53e5a75e464264fa96a955b9aca37f4fe238e08b6345c";
        public int Port { get; set; } = 8080;
        public string Username { get; set; } = "ca37f4fe238e08b6345c6ab15b2ee4b4f104a805";
        public string Password { get; set; } = "b2697d958bb12d6d0de34bcc68a2ab15b2ee4b4f";
    }

    public class Uplink
    {
        public string RUDI { get; set; } = "REF^07269234001^SCL75041";
        public bool IsProxy { get; set; } = false;
    }

    public class Downlink
    {
        public string RUDI { get; set; } = "REF^0123456789^666-25";
        public int Position { get; set; } = 1;
    }
}
