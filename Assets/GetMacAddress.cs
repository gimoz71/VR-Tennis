using System.Net.NetworkInformation;

var macAddr = 
	(
		from nic in NetworkInterface.GetAllNetworkInterfaces()
		where nic.OperationalStatus == OperationalStatus.Up
		select nic.GetPhysicalAddress().ToString()
	).FirstOrDefault();