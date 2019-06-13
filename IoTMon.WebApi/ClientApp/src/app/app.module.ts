import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MaterialModule } from './material/material.module';
import { ChartComponent } from './chart/chart.component';
import { HttpClientModule } from '@angular/common/http';
import { DeviceRoutingModule } from './device/device-routing.module';
import { DeviceModule } from './device/device.module';
import { AppConstants } from './helpers/constants';
import { JwtModule } from '@auth0/angular-jwt';
import { AuthModule } from './auth/auth.module';

export function tokenGetter() {
  return localStorage.getItem(AppConstants.jwtKey);
}

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    ChartComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    AuthModule,
    DeviceModule,
    LayoutModule,
    MaterialModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter
      }
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
