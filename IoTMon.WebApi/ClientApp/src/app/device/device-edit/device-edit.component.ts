import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Device } from 'src/app/models/device.model';
import { DeviceService } from '../device.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-device-edit',
  templateUrl: './device-edit.component.html',
  styleUrls: ['./device-edit.component.less']
})
export class DeviceEditComponent implements OnInit, OnDestroy {

  private deviceId: string;
  private device: Device;
  private httpSub: Subscription = new Subscription();

  constructor(private route: ActivatedRoute,
    private deviceService: DeviceService) { }

  ngOnInit() {
    this.deviceId = this.route.snapshot.params['deviceId'];
    const sub = this.deviceService.getDeviceById(this.deviceId)
      .subscribe((resp: Device) => {
        this.device = resp;
      });
    this.httpSub.add(sub);
  }

  onSubmit() {
    console.log(this.device);
  }

  ngOnDestroy() {
    this.httpSub.unsubscribe();
  }

}
