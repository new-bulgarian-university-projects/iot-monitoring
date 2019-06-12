import { Sensor } from './sensor';

export class Device {
    public id: string;
    public deviceName: string;
    public intervalInSeconds: number;
    public isActivated: boolean;
    public isPublic: boolean;
    public sensors: Sensor[];
}