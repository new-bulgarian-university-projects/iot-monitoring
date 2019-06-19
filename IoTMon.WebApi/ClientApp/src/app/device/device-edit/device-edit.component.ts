import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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
    private router: Router,
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
    if (confirm("Save changes ?")) {
      const sub = this.deviceService.updateDevice(this.device)
        .subscribe((resp: Device) => {
          console.log('updated ', resp)
          this.router.navigate(['/devices']);
        }, err => console.log(err));

      this.httpSub.add(sub);
    }
  }

  ngOnDestroy() {
    this.httpSub.unsubscribe();
  }

}
