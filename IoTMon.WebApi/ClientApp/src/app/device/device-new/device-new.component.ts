import { Component, OnInit, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import { Device } from 'src/app/models/device.model';
import { Sensor } from 'src/app/models/sensor.model';
import { Subscription } from 'rxjs';
import { DeviceService } from '../device.service';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-device-new',
  templateUrl: './device-new.component.html',
  styleUrls: ['./device-new.component.less']
})
export class DeviceNewComponent implements OnInit, OnDestroy {

  subs = new Subscription();

  device: Device;
  availableSensors: Sensor[];

  @ViewChild(NgForm, { static: true }) form;

  constructor(private deviceService: DeviceService,
    private router: Router) {
  }

  ngOnInit() {
    const httpSub = this.deviceService.getAllSensors()
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

    this.subs.add(httpSub);
  }

  onSubmit() {
    if (confirm("Are you sure you want to create this device ?")) {
      const httpSub = this.deviceService.createDevice(this.device)
        .subscribe((resp) => {
          console.log('created ', resp);
          this.router.navigate(['/devices']);
        }, err => console.log(err));

      this.subs.add(httpSub);
    }
  }

  onChange() {
    const sensors: Sensor[] = this.availableSensors
      .filter(s => s.checked)
      .map(s => s);

    this.device.sensors = sensors;
  }

  isFormValid(): boolean {
    const isValid = this.form.valid && this.device.sensors.length > 0;
    return isValid;
  }

  ngOnDestroy() {
    this.subs.unsubscribe();
  }
}
