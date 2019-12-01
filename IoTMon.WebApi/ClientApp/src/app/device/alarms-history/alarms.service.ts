import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AlarmRecord } from "src/app/models/alarm-record.model";
import { AppConstants } from "src/app/helpers/constants";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: "root"
})
export class AlarmsService {
  constructor(private httpClient: HttpClient) {}

  getAlarmsHistory(
    deviceId: string,
    sensorName: string
  ): Observable<AlarmRecord> {
    const url =
      AppConstants.baseUrl +
      `/devices/${deviceId}/sensors/${sensorName}/alerts-history`;

      console.log('url ', url);
    return this.httpClient.get<AlarmRecord>(url);
  }
}
