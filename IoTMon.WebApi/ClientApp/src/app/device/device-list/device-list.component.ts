import { Component, OnInit, OnDestroy } from '@angular/core';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { DeviceService } from '../device.service';
import { Device } from 'src/app/models/device.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-device-list',
  templateUrl: './device-list.component.html',
  styleUrls: ['./device-list.component.less'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class DeviceListComponent implements OnInit, OnDestroy {
  columnsToDisplay = ['deviceName', 'id', 'isActivated', 'isPublic'];
  expandedElement: Device | null;
  dataSource: Device[];

  private httpSub = new Subscription();

  constructor(private deviceService: DeviceService) { }

  isBoolean(value: string): boolean {
    value = value.toString().toLowerCase();
    return (value == 'true' || value == 'false');
  }

  onDelete(device: Device) {
    if (confirm("Are you sure you want to delete " + device.deviceName)) {
      const sub = this.deviceService.deleteDevice(device.id);
      this.httpSub.add(sub);
    }
  }

  processValue(element: string): any {
    if (this.isBoolean(element)) {
      return element.toString().toLowerCase() === 'true' ? "✔" : "✖";
    }
    else {
      return element;
    }
  }

  ngOnInit() {
    const sub = this.deviceService.getAllDevices()
      .subscribe((resp) => {
        this.dataSource = resp
      }, err => console.log(err));

    const subjectSub = this.deviceService.onDelete
      .subscribe((deviceId) => {
        this.dataSource = this.dataSource
          .filter(d => d.id != deviceId);
      });

    this.httpSub.add(sub);
    this.httpSub.add(subjectSub);
  }

  ngOnDestroy(): void {
    this.httpSub.unsubscribe();
  }
}
