import { Component, OnInit, OnDestroy, ViewChild, QueryList, ViewChildren } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Device } from 'src/app/models/device.model';
import { DeviceService } from '../device.service';
import { Subscription } from 'rxjs';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-device-edit',
  templateUrl: './device-edit.component.html',
  styleUrls: ['./device-edit.component.less']
})
export class DeviceEditComponent implements OnInit, OnDestroy {

  private deviceId: string;
  private device: Device;
  private httpSub: Subscription = new Subscription();

  @ViewChild('f', {static: false}) form: any;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private deviceService: DeviceService) { }

  ngOnInit() {
    this.deviceId = this.route.snapshot.params['deviceId'];
    const sub = this.deviceService.getDeviceById(this.deviceId)
      .subscribe((resp: Device) => {
        this.device = resp;
        console.log("resp ", this.device);
      });
    this.httpSub.add(sub);
  }

  onSubmit() {
    if (confirm("Save changes ?")) {
      const sub = this.deviceService.updateDevice(this.device)
        .subscribe((resp: Device) => {
          console.log('updated ', resp)
          this.router.navigate(['/devices']);
        }, err => console.log(err));

      this.httpSub.add(sub);
    }
  }

  isFormValid(): boolean {
    if (this.form) {
      const isValid = this.form.valid;
      if (isValid) {
        for (const sensor of this.device.sensors) {
          if(sensor.minValue == undefined || sensor.maxValue == undefined){
            continue;
          }
          if (sensor.minValue > sensor.maxValue) {
            return false;
          }
        }
      }
      return isValid;
    }
  }

  ngOnDestroy() {
    this.httpSub.unsubscribe();
  }

}
