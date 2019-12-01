import { AlertTypeEnum } from './enums/alert-type-enum';

export class AlarmHistory {
  public value: number;
  public alertType: AlertTypeEnum;
  public started: Date;
  public closed?: Date;
}
