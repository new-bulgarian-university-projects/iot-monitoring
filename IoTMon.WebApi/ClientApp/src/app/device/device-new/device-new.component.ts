import { Component, OnInit, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import { Device } from 'src/app/models/device.model';
import { Sensor } from 'src/app/models/sensor.model';
import { Subscription } from 'rxjs';
import { DeviceService } from '../device.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-device-new',
  templateUrl: './device-new.component.html',
  styleUrls: ['./device-new.component.less']
})
export class DeviceNewComponent implements OnInit, OnDestroy {

  sub = new Subscription();

  device: Device;
  availableSensors: Sensor[];

  @ViewChild(NgForm, { static: true }) form;

  constructor(private deviceService: DeviceService) {
  }

  ngOnInit() {
    this.sub = this.deviceService.getAllSensors()
      .subscribe((resp: Sensor[]) => {
        this.availableSensors = resp.map(r => {
          r.checked = false;
          return r;
        });
        console.log(this.availableSensors);
      });
    this.device = new Device();
    this.device.isActivated = true;
    this.device.intervalInSeconds = 15;
  }

  onSubmit() {

    console.log(this.device);
  }

  onChange() {
    const sensorIds = this.availableSensors
      .filter(s => s.checked)
      .map(s => s.id);

    this.device.sensorIds = sensorIds;
  }

  isFormValid(): boolean {
    const isValid = this.form.valid && this.device.sensorIds.length > 0;
    return isValid;
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }
}
