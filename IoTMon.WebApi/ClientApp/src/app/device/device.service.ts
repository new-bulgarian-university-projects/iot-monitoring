import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Device } from '../models/device';
import { map } from 'rxjs/operators';
import { ChartData } from '../models/chartData';
import { AppConstants } from '../helpers/constants';


@Injectable({
  providedIn: 'root'
})
export class DeviceService {
  private baseUrl = 'https://localhost:44311/api';

  constructor(private httpClient: HttpClient) { }

  getSensorData(deviceId: string, sensor: string): Observable<ChartData[]> {
    return this.httpClient.get<ChartData[]>(this.baseUrl + `/devices/${deviceId}/sensors/${sensor}`)
      .pipe(map(
        r => r.map(
          (x => { return { date: new Date(x.date), value: x.value } })
        )
      ));
  }

  getAllDevices(): Observable<Device[]> {
    return this.httpClient.get<Device[]>(this.baseUrl + '/devices');
  }

  getIcon(sensorLabel: string): string{
    return AppConstants.sensorIconMap[sensorLabel];
  }

}
