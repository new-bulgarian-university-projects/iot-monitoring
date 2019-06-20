import { Component, NgZone, AfterViewInit, OnDestroy, OnInit, HostListener } from '@angular/core';
import * as am4core from "@amcharts/amcharts4/core";
import * as am4charts from "@amcharts/amcharts4/charts";
import am4themes_animated from "@amcharts/amcharts4/themes/animated";
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { DeviceService } from '../device/device.service';
import { Subscription } from 'rxjs';
import { ChartData } from '../models/chartData.model';
import { SignalrService } from '../device/signalr.service';
import { AppConstants } from '../helpers/constants';

am4core.useTheme(am4themes_animated);

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.less']
})
export class ChartComponent implements AfterViewInit, OnInit, OnDestroy {

  private chart: am4charts.XYChart;
  private deviceId: string;
  private sensor: string;
  private isLive: boolean = false;
  private connectionId: string;
  private httpSub = new Subscription();
  private dateTimeRange: Date[];
  private maxDate = new Date();


  constructor(private deviceService: DeviceService,
    private route: ActivatedRoute,
    private httpClient: HttpClient,
    private signalRService: SignalrService,
    private zone: NgZone) { }

  onRangeChange() {
    if (this.dateTimeRange.length > 0 || this.dateTimeRange[0]) {
      const sub = this.deviceService.getSensorData(this.deviceId, this.sensor, this.dateTimeRange[0], this.dateTimeRange[1])
        .subscribe((resp: ChartData[]) => {
          if (resp && resp.length > 0) {
            this.chart.data = resp;
          }
          else {
            this.chart.data = [];
          }
        });

      this.httpSub.add(sub);
    }
  }

  defaultRange() {
    const fromDate = new Date();
    fromDate.setHours(new Date().getHours() - AppConstants.sensorsHoursAgo);
    const sub = this.deviceService.getSensorData(this.deviceId, this.sensor, fromDate)
      .subscribe((resp: ChartData[]) => {
        if (resp && resp.length > 0) {
          this.chart.data = resp;
        }
      });
    this.httpSub.add(sub);
  }

  ngAfterViewInit() {
    this.zone.runOutsideAngular(() => {
      let chart = am4core.create("chartdiv", am4charts.XYChart);

      chart.paddingRight = 20;

      const fromDate = new Date();
      fromDate.setHours(new Date().getHours() - AppConstants.sensorsHoursAgo);
      const sub = this.deviceService.getSensorData(this.deviceId, this.sensor, fromDate)
        .subscribe((resp: ChartData[]) => {

          chart.data = resp;

          let dateAxis = chart.xAxes.push(new am4charts.DateAxis());
          dateAxis.dateFormats.setKey("day", "HH:mm:ss dd/MM/yyyy")
          dateAxis.periodChangeDateFormats.setKey("hour", "dd/MM/yyyy");
          dateAxis.renderer.grid.template.location = 0;


          let valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
          valueAxis.tooltip.disabled = true;
          valueAxis.renderer.minWidth = 35;


          let series = chart.series.push(new am4charts.LineSeries());
          series.dataFields.dateX = "date";
          series.dataFields.valueY = "value";

          series.tooltipText = "{valueY.value}";
          chart.cursor = new am4charts.XYCursor();

          let scrollbarX = new am4charts.XYChartScrollbar();
          scrollbarX.series.push(series);
          chart.scrollbarX = scrollbarX;
          this.chart = chart;

          this.chart.events.on("datavalidated", () => {
            dateAxis.zoom({ start: 0.65, end: 1.1 }, false, false);
          });

          let bullet = series.createChild(am4charts.CircleBullet);
          bullet.circle.radius = 5;
          bullet.fillOpacity = 1;
          bullet.fill = chart.colors.getIndex(0);
          bullet.isMeasured = false;

          series.events.on("validated", () => {
            if (series.dataItems.length > 0) {
              bullet.moveTo(series.dataItems.last.point);
              bullet.validatePosition();
            }
          })

        });

      this.httpSub.add(sub);
    });
  }

  async onChange() {
    if (this.isLive) {
      await this.startLive();
    }
    else {
      this.stopLive();
    }
  }
  stopLive() {
    this.signalRService.stopConnection();
  }

  async startLive() {
    this.signalRService.startConnection(this.deviceId, this.sensor)
      .then((connId: string) => {
        this.connectionId = connId;
        console.log(this.connectionId);
        this.signalRService.addTransferChartDataListener(this.chart);
        const url = AppConstants.baseUrl + `/chart?deviceId=${this.deviceId}&sensor=${this.sensor}&connId=${this.connectionId}`;
        this.httpClient.get(url)
          .subscribe((resp: ChartData[]) => {
            console.log("open connection");
          });
      });
  }

  ngOnInit(): void {

    this.route.params.subscribe((p) => {
      this.deviceId = p['deviceId'];
      this.sensor = p['sensor'];
    }, (err) => console.log(err));
  }

  ngOnDestroy(): void {
    this.zone.runOutsideAngular(() => {
      if (this.chart) {
        this.chart.dispose();
      }
    });
    this.signalRService.stopConnection();
    this.httpSub.unsubscribe();
  }
}