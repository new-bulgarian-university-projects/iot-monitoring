import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ChartComponent } from './chart/chart.component';
import { UserComponent } from './user/user.component';

const routes: Routes = [
  {
    path: 'chart/device/:deviceId/sensor/:sensor',
    component: ChartComponent
  },
  {
    path: 'user/:userId',
    component: UserComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
