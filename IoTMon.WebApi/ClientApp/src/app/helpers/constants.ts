export class AppConstants {
    
    public static readonly baseUrl: string = "https://localhost:44311/api";
    public static readonly socket: string = "https://localhost:44311/live";
    
    public static readonly jwtKey: string = "jwtToken";

    public static readonly sensorIconMap: any = {
        'temp': ' wb_sunny',
        'openclose': 'lock_open',
        'sound': 'volume_up',
        'hum': 'wb_cloudy',
        'no2': 'dock',
        'so2': 'dock',
        'o3': 'dock',
        'co2': 'dock'
    };
}