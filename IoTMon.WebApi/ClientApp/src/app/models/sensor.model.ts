export class Sensor {
    public id: string;
    public label: string;
    public friendlyLabel: string;
    public description: string;
    public measurementUnit: string;
    public valueType: ValueType;
    public checked: boolean;
    public minValue: number;
    public maxValue: number;
    public isNotificationOn: boolean;
}