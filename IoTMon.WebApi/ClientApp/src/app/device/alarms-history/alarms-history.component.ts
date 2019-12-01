import { Component, OnInit, ViewChild, OnDestroy } from "@angular/core";
import { MatPaginator } from "@angular/material/paginator";
import { MatTableDataSource } from "@angular/material/table";
import { AlarmsService } from "./alarms.service";
import { ActivatedRoute } from "@angular/router";
import { AlarmRecord } from "src/app/models/alarm-record.model";
import { AlarmHistory } from "src/app/models/alarm-history.model";
import { AlertTypeEnum } from "src/app/models/enums/alert-type-enum";
import { Subscription } from "rxjs";
import { MatSortModule } from '@angular/material';

@Component({
  selector: "app-alarms-history",
  templateUrl: "./alarms-history.component.html",
  styleUrls: ["./alarms-history.component.less"]
})
export class AlarmsHistoryComponent implements OnInit, OnDestroy {
  displayedColumns: string[] = ["value", "alertType", "started", "closed"];
  dataSource: MatTableDataSource<AlarmHistory>;
  private deviceId: string;
  private sensorName: string;
  private subs: Subscription = new Subscription();
  private minValue?:number;
  private maxValue?:number;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  constructor( 
    private route: ActivatedRoute,
    private alarmsService: AlarmsService
  ) {}

  ngOnInit() {
    this.deviceId = this.route.snapshot.params["deviceId"];
    this.sensorName = this.route.snapshot.url[4].path;
    const sub = this.alarmsService
      .getAlarmsHistory(this.deviceId, this.sensorName)
      .subscribe((resp: AlarmRecord) => {
        console.log(resp);
        this.dataSource = new MatTableDataSource<AlarmHistory>(
          resp.alertHistory
        );
        this.minValue = resp.minValue;
        this.maxValue = resp.maxValue;
        this.dataSource.paginator = this.paginator;
      });
    this.subs.add(sub);
  }

  parseEnum(val: AlertTypeEnum): string {
    let message = "";
    if (val === AlertTypeEnum.Above_Max_Value) {
      AlertTypeEnum;
      message = "Above Temp ()";
    } else if (val === AlertTypeEnum.Below_Min_value) {
      message = "Under Temp";
    }
    return message;
  }

  ngOnDestroy() {
    if (this.subs) {
      this.subs.unsubscribe();
    }
  }
}
