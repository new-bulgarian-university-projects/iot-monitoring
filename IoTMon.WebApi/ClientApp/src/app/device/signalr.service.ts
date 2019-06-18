import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { ChartData } from '../models/chartData.model';
import { AppConstants } from '../helpers/constants';
import * as am4charts from "@amcharts/amcharts4/charts";
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {

  public data: ChartData[];
  private hubConnection: signalR.HubConnection;
  private subs: Subscription = new Subscription();

  constructor(private httpClient: HttpClient) {

  }

  public startConnection = (deviceId: string, sensor: string): Promise<any> => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(AppConstants.socket)
      .build();

    return this.hubConnection
      .start()
      .then(() => {
        console.log('Connection started')
        const connectionId = this.hubConnection.invoke("getConnectionId");
        return connectionId;
      })
      .then((connId: string) => {
        return connId;
      })
      .catch(err => console.log('Error while starting connection: ' + err))

  }

  public stopConnection() {
    if (this.hubConnection) {
      this.hubConnection.stop();
      this.hubConnection = null;
    }
  }

  public addTransferChartDataListener = (chart: am4charts.XYChart) => {
    this.hubConnection.on('transferchartdata', (data) => {
      this.data = data;
      const d: ChartData[] = data.map(
        ((x: ChartData) => {
          const time = new Date(x.date).toISOString().replace("Z", "");
          const chartData = new ChartData(new Date(time), x.value);
          return chartData;
        })
      )
      if (d && d.length > 0) {
        console.log("add to chart ", d);
        chart.addData(d);
      }
    });
  }
}
