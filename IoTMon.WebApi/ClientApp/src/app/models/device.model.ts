import { Sensor } from './sensor.model';

export class Device {
    constructor() {
        this.sensors = [];
        this.sensorIds = [];
    }
    public id: string;
    public deviceName: string;
    public intervalInSeconds: number;
    public isActivated: boolean;
    public isPublic: boolean;
    public userId: string;

    public sensors: Sensor[];
    public sensorIds: string[];
}