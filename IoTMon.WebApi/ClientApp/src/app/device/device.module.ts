import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DeviceRoutingModule } from './device-routing.module';
import { DeviceListComponent } from './device-list/device-list.component';
import { MaterialModule } from '../material/material.module';

@NgModule({
  declarations: [DeviceListComponent],
  imports: [
    CommonModule,
    DeviceRoutingModule,
    MaterialModule
  ]
})
export class DeviceModule { }
