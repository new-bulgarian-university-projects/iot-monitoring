import { Component, NgZone, AfterViewInit, OnDestroy, OnInit } from '@angular/core';
import * as am4core from "@amcharts/amcharts4/core";
import * as am4charts from "@amcharts/amcharts4/charts";
import am4themes_animated from "@amcharts/amcharts4/themes/animated";
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { DeviceService } from '../device/device.service';
import { Subscription } from 'rxjs';
import { ChartData } from '../models/chartData.model';

am4core.useTheme(am4themes_animated);

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.less']
})
export class ChartComponent implements AfterViewInit, OnInit, OnDestroy {

  private chart: am4charts.XYChart;
  private interval: NodeJS.Timer;
  private deviceId: string;
  private sensor: string;
  private isLive: boolean = false;

  private httpSub = new Subscription();


  constructor(private deviceService: DeviceService,
    private route: ActivatedRoute,
    private zone: NgZone) { }

  ngAfterViewInit() {
    this.zone.runOutsideAngular(() => {
      let chart = am4core.create("chartdiv", am4charts.XYChart);

      chart.paddingRight = 20;

      const sub = this.deviceService.getSensorData(this.deviceId, this.sensor)
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
            dateAxis.zoom({ start: 0.9999, end: 1.00001 }, false, false);
          });

          let bullet = series.createChild(am4charts.CircleBullet);
          bullet.circle.radius = 5;
          bullet.fillOpacity = 1;
          bullet.fill = chart.colors.getIndex(0);
          bullet.isMeasured = false;

          series.events.on("validated", () => {
            bullet.moveTo(series.dataItems.last.point);
            bullet.validatePosition();
          })

        });

      this.httpSub.add(sub);
    });
  }

  onChange() {
    if (this.isLive) {
      this.startLive();
    }
    else {
      this.stopLive();
    }
  }
  stopLive() {
    clearInterval(this.interval);
  }

  startLive(): NodeJS.Timer {
    this.interval = setInterval(() => {
      console.log('add');

      this.chart.addData(
        { date: Date.now(), value: Math.random() * 100 }
      );
    }, 1000)
    return this.interval;
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

    if (this.interval) {
      clearInterval(this.interval);
    }
    this.httpSub.unsubscribe();
  }
}