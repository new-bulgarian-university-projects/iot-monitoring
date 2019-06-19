import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DeviceRoutingModule } from './device-routing.module';
import { DeviceListComponent } from './device-list/device-list.component';
import { MaterialModule } from '../material/material.module';
import { DeviceNewComponent } from './device-new/device-new.component';
import { FormsModule } from '@angular/forms';
import { DeviceEditComponent } from './device-edit/device-edit.component';

@NgModule({
  declarations: [DeviceListComponent, DeviceNewComponent, DeviceEditComponent],
  imports: [
    CommonModule,
    FormsModule,
    DeviceRoutingModule,
    MaterialModule
  ]
})
export class DeviceModule { }
