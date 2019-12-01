import { AlarmHistory } from "./alarm-history.model";

export class AlarmRecord {
  public deviceId: string;
  public deviceName: string;
  public sensorName: string;
  public minValue?: number;
  public maxValue?: number;
  public alertHistory: AlarmHistory[];
}
