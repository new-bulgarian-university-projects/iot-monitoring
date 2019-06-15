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

  getSensorData(deviceId: string, sensor: string): Observable<ChartData[]> {
    return this.httpClient.get<ChartData[]>(this.baseUrl + `/devices/${deviceId}/sensors/${sensor}`)
      .pipe(map(
        r => r.map(
          (x => { return { date: new Date(x.date), value: x.value } })
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
    this.httpClient.delete<Device>(url).pipe(first()).subscribe(() => {this.onDelete.next(deviceId)});
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
