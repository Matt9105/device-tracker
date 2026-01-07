using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.VisualBasic;


enum DeviceStatus
{
    Delivered,
    Reserved,
    InTesting,
    ReturnedToBoss,
    Sold
}

enum DeviceLocation
{
    Office,
    BossWarehouse,
    Andzej,
    Mateusz,
    Kasia,
    Sent
}
class Device
{
    public Guid Id { get; set; }
    public required string Name {get; set; }
    public required string SerialNumber { get; set; }
 
    public DeviceStatus Status { get; set; }
    public DeviceLocation Location { get; set; }
    public List<DeviceHistory> History {get; set; } = new();
    }

    class DeviceHistory
{
    public DateTime Date { get; set; }
    public required string Action { get; set; }
    public required string PerformedBy { get; set; }
    
    public DeviceStatus Status { get; set; }
    public DeviceLocation Location { get; set; }
        
}
    

class Program
{
    static void Main()
    {
        var devices = new List<Device>();
        var device1 = new Device
        {
            Id = Guid.NewGuid(),
            Name = "Kasa fiskalna XYZ",
            SerialNumber = "SN-001"
        };

        devices.Add(device1);

        var device2 = new Device
        {
            Id = Guid.NewGuid(),
            Name = "Elzab Cube Online",
            SerialNumber = "SN:abcdefg"
        };

        devices.Add(device2);
        
        ChangeDeviceState(
            device2,
            "Przywiezione do biura",
            "Szef",
            DeviceStatus.Delivered,
            DeviceLocation.Office
            
        );

        ChangeDeviceState(
            device2,
            "Przekazane do sprzedaży",
            "Mateusz",
            DeviceStatus.Reserved,
            DeviceLocation.Office
        );

        ChangeDeviceState(
            device1,
            "Przywiezione do biura",
            "Szef",
            DeviceStatus.Delivered,
            DeviceLocation.Office
        );
        ChangeDeviceState(
            device1,
            "Przesnaczone dla klienta ABC",
            "Mateusz",
            DeviceStatus.Reserved,
            DeviceLocation.Office
            );

        ChangeDeviceState(
            device1,
            "Sprzedane - FV/01/2026",
            "Andrzej",
            DeviceStatus.Sold,
            DeviceLocation.Office
        );

            
        //AddHistory(device, "Przywiezione do biura", "Szef");
        //AddHistory(device, "Przeznaczone dla klienta ABS", "Mateusz");
        //AddHistory(device, "Sprzedane - FV/01/2026", "Andrzej");

        foreach (var device in devices)
        {
            ShowDeviceHistory(device);
            Console.WriteLine();
        }


    }

        

    static void AddHistory(Device device, string action, string user)
    {
        device.History.Add(new DeviceHistory
        {
            Date = DateTime.Now,
            Action = action,
            PerformedBy = user
        });
    }    
    static void ShowDeviceHistory(Device device)
    {
       Console.WriteLine($"Historia urządzenia: {device.Name}");
       Console.WriteLine("----------------------------------");

       foreach (var h in device.History)
       {
          Console.WriteLine($"{h.Date:G} | {h.Action} | {h.PerformedBy} | {h.Status} | {h.Location}");
       }
    }
    static void ChangeDeviceState(
        Device device,
        string action,
        string user,
        DeviceStatus status,
        DeviceLocation location)
    {
        device.Status = status;
        device.Location = location;

        device.History.Add(new DeviceHistory
        {
            Date = DateTime.Now,
            Action = action,
            PerformedBy = user,
            Status = status,
            Location = location
            
        });
    }
}