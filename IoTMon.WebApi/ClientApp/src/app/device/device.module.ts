import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DeviceRoutingModule } from './device-routing.module';
import { DeviceListComponent } from './device-list/device-list.component';
import { MaterialModule } from '../material/material.module';
import { DeviceNewComponent } from './device-new/device-new.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [DeviceListComponent, DeviceNewComponent],
  imports: [
    CommonModule,
    FormsModule,
    DeviceRoutingModule,
    MaterialModule
  ]
})
export class DeviceModule { }
