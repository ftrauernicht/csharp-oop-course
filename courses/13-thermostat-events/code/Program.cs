// Course 13 - Thermostat Events: a device that announces its own state
// changes to whoever's listening, without knowing who that is.

var thermostat = new Thermostat();
var display = new Display();
var logger = new Logger();

thermostat.TemperatureChanged += display.ShowTemperature;
thermostat.TemperatureChanged += logger.LogChange;

Console.WriteLine("Setting temperature to 20...");
thermostat.Temperature = 20;

// --- Optional: unsubscribing ---
Console.WriteLine("Logger stops listening...");
thermostat.TemperatureChanged -= logger.LogChange;

Console.WriteLine("Setting temperature to 25...");
thermostat.Temperature = 25; // only Display reacts now

// --- Challenge: a third subscriber, zero changes to Thermostat ---
var alarm = new Alarm(threshold: 22);
thermostat.TemperatureChanged += alarm.CheckTemperature;

Console.WriteLine("Setting temperature to 30...");
thermostat.Temperature = 30;
