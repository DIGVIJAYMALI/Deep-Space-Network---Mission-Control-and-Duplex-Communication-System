# Deep Space Network : Mission Control and Duplex Communication System For Spacecrafts
SpaceXYZ is in the business of launching rockets and satellites into space for its customers. The launch vehicle or the "Rocket" and the payload that may be a satellite are two components of each SpaceXYZ spacecraft launch. SpaceXYZ has a Deep Space Network (DSN) facility with a communication system and mission control system from which it launches and interacts with its spacecraft. SpaceXYZ wants to design a software system to run their operations.
SpaceXYZ is in the business of launching rockets and satellites into space for its customers. The launch vehicle or the "Rocket" and the payload that may be a satellite are two components of each SpaceXYZ spacecraft launch. SpaceXYZ has a Deep Space Network (DSN) facility with a communication system and mission control system from which it launches and interacts with its spacecraft. SpaceXYZ wants to design a software system to run their operations.
SpaceXYZ wants you to design a software system to run their operations. This software system can be classified as follows: 

**1) DSN Software Component Features:** 
1. Able to show dashboard for 
1. All current active spacecrafts. 
2. All spacecrafts waiting to be launched. 
2. Able to select a specific active spacecraft and look at its data. 
3. Able to send command to a specific spacecraft.
4. Able to launch a new spacecraft.
 
**2) Launch-Vehicle Software Component Features:** 
1. Able to receive and process commands from DSN. 
2. Able to send real-time telemetry of itself back to DSN. 

**3) Payload/Satellite Software Component Features:** 
1. Able to receive and process commands from DSN. 
2. Able to send real-time telemetry of itself back to DSN. 
3. Able to send its Data back to DSN. 


**Design Rules:** 

Configuration:  

**A Launch-Vehicle configuration should be defined in a config-file with following information** 
1. Name: Name of launch vehicle. 
2. Orbit Info: Radius of orbit in (km). 
3. Payload Config-File: Pointer to the configuration file for payload.  

**A Payload configuration should be defined in a config-file with following information**
1. Name: Name of payload 
2. Type: Type of payload (Possible Values: Scientific, Communication, Spy) 
Data sent by actual "Payload" can be modeled randomly as follows:
Scientific: Periodic scientific data of your choice and format Example, Solar-Activity in solar-flares per second every 3 seconds or Weather-data (%Rain, %Humidity, %Snow) every 1 min. 
Communication: Periodic communication utilization data of your choice and format Example, Bandwidth utilization (Uplink and downlink data rates) every 5 seconds. 
Spy: Periodic image data of your choice and format Example: An image every 10 seconds. 

**Launch Sequence:** 

DSN launches a Launch-Vehicle as follows:  
User should be able to select a Launch-Vehicle configuration file and launch.  
DSN software should then start the program/executable for Launch-Vehicle with the selected configuration-file.  
Using the orbit info in the configuration Launch-Vehicle will "fly" till the orbit is reached. This needs to be simulated as follows: o Using orbit info launch vehicle will calculate time-to orbit as follows: t = (Orbit Radius in km / 3600 + 10) seconds  
After t seconds have elapsed only then Launch-Vehicle can then accept “DeployPayload” command from DSN.  
After t seconds have elapsed Launch-Vehicle needs to "update" DSN that it has reached its orbit. 

**Commands:**

**Launch-Vehicle software should accept following Commands from DSN:**  
[1] DeployPayload: Start the Payload software program/executable with the configured Payload Config-File.  
[2] Deorbit: End the Launch-Vehicle software program to simulate that Launch-Vehicle has been de-orbited.  
[3] StartTelemetry: Start sending (random/realistic up to you) telemetry data every second to DSN.  
[4] StopTelemetry: Stop sending telemetry data to DSN.
 
**Payload software should accept following Commands from DSN:**  
[1] StartData: Start sending data configured according to "Type" in Payload Config-File to DSN.
[2] StopData: Stop sending data to DSN  
[3] Decommission: End Payload software program to simulate that Payload has ended its mission.  
[4] StartTelemetry: Start sending (random/realistic up to you) telemetry data every second to DSN.  StopTelemetry: Stop sending telemetry data to DSN. 

**Telemetry:**

Telemetry data sent by either Launch-Vehicle or Payload can be random or realistic.  
Each piece of telemetry can include following information 
[1] Altitude: in km 
[2] Longitude: in degrees (-90 deg (South) to +90 deg (North)) 
[3] Latitude: in degrees (-180 deg (West) to 180 deg (East)) 
[4] Temperature: in kelvin 
[5] Time to Orbit: in seconds counting down to 0 (This must be the from above t calculation)  

Example:

**INSTRUCTIONS TO RUN THE PROJECT**

[1] **FRONT END** : **WPF** (SpaceXYZ (Vehicle Launch), SpaceNetwork (DSN), Payload (Payload Component))

[2] **BACK END** : **WCF** (Messaging Service, Interface)

[3] SET ‘MessagingServer’ and ‘SpaceNetwork’ as start projects which will act as a server and client respectively.

[4] We have to update a path of the .exe file in the SpaceXYZ and SpaceNetwork project.

Ex: 
**Process p = new Process();
 p.StartInfo = new ProcessStartInfo("C:\\Users\\dmali\\source\\repos\\SpaceXYZ\\SpaceXYZ\\bin\\Debug\\SpaceXYZ.exe");**  
 
[5] If we exit the Spacecraft and Payload WPF files then DSN will automatically be called ‘Deorbit’ Instruction.

[6] For more details I have also uploaded a video demo.

