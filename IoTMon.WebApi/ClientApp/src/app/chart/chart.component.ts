import { Component, NgZone, AfterViewInit, OnDestroy } from '@angular/core';
import * as am4core from "@amcharts/amcharts4/core";
import * as am4charts from "@amcharts/amcharts4/charts";
import am4themes_animated from "@amcharts/amcharts4/themes/animated";
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

am4core.useTheme(am4themes_animated);

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.less']
})
export class ChartComponent implements AfterViewInit, OnDestroy {

  ngOnDestroy(): void {
    this.zone.runOutsideAngular(() => {
      if (this.chart) {
        this.chart.dispose();
      }
    });
  }

  private chart: am4charts.XYChart;

  constructor(private httpClient: HttpClient,
              private route: ActivatedRoute,
              private zone: NgZone) { }

  ngAfterViewInit() {
    this.zone.runOutsideAngular(() => {
      let chart = am4core.create("chartdiv", am4charts.XYChart);

      chart.paddingRight = 20;

      this.httpClient.get('https://localhost:44311/api/devices/77f2aa97-f5a0-40e0-a811-5aaf59626356/sensors/temp')
        .subscribe((resp: any) => {
          console.log(resp);
          const data = resp.map(r => {return {date: new Date(r.date), value: Number.parseFloat(r.value)}} );
          console.log(data);
          chart.data = data;

          let dateAxis = chart.xAxes.push(new am4charts.DateAxis());

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
        });
    });
  }
}