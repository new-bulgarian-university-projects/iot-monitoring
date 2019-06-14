import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DeviceListComponent } from './device-list/device-list.component';
import { DeviceNewComponent } from './device-new/device-new.component';

const routes: Routes = [
  {
    path: 'devices',
    component: DeviceListComponent
  },
  {
    path: 'devices/new',
    component: DeviceNewComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DeviceRoutingModule { }
