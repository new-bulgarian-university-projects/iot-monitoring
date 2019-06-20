import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DeviceListComponent } from './device-list/device-list.component';
import { DeviceNewComponent } from './device-new/device-new.component';
import { DeviceEditComponent } from './device-edit/device-edit.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: '/devices',
    pathMatch: 'full'
  },
  {
    path: 'devices',
    component: DeviceListComponent
  },
  {
    path: 'devices/new',
    component: DeviceNewComponent
  },
  {
    path: 'devices/:deviceId/edit',
    component: DeviceEditComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DeviceRoutingModule { }
