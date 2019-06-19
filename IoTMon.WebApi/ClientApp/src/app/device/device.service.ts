import { Injectable } from '@angular/core';
import { Observable, from, Subscription, Subject } from 'rxjs';
import { HttpClient, HttpRequest } from '@angular/common/http';
import { Device } from '../models/device.model';
import { map, first } from 'rxjs/operators';
import { ChartData } from '../models/chartData.model';
import { AppConstants } from '../helpers/constants';
import { Sensor } from '../models/sensor.model';



@Injectable({
  providedIn: 'root'
})
export class DeviceService {
  private readonly baseUrl = 'https://localhost:44311/api';
  public readonly onDelete: Subject<string>;

  constructor(private httpClient: HttpClient) {
    this.onDelete = new Subject<string>();
  }

  getSensorData(deviceId: string, sensor: string, from?: Date, to?: Date): Observable<ChartData[]> {
    let url = this.baseUrl + `/devices/${deviceId}/sensors/${sensor}`;
    if (from) {
      url += `?from=${from.toISOString()}`;
    }
    if (to) {
      url += `&to=${to.toISOString()}`;
    }
    return this.httpClient.get<ChartData[]>(url)
      .pipe(map(
        r => r.map(
          ((x: ChartData) => {
            const time = new Date(x.date).toISOString().replace("Z", "");
            const chartData = new ChartData(new Date(time), x.value);
            return chartData;
          })
        )
      ));
  }

  createDevice(device: Device): Observable<Device> {
    if (!device) {
      return null;
    }
    return this.httpClient.post<Device>(this.baseUrl + '/devices', device);
  }

  deleteDevice(deviceId: string): Subscription {
    if (!deviceId) {
      return null;
    }
    const url = this.baseUrl + `/devices/${deviceId}`;
    this.httpClient.delete<Device>(url).pipe(first()).subscribe(() => { this.onDelete.next(deviceId) });
  }

  getAllSensors(): Observable<Sensor[]> {
    return this.httpClient.get<Sensor[]>(this.baseUrl + '/sensors');
  }

  getAllDevices(): Observable<Device[]> {
    return this.httpClient.get<Device[]>(this.baseUrl + '/devices');
  }

  getIcon(sensorLabel: string): string {
    return AppConstants.sensorIconMap[sensorLabel];
  }

}
