import { NgModule } from '@angular/core';
import {MatButtonModule, MatCheckboxModule, MatToolbarModule, MatSidenavModule, MatIconModule, MatListModule, MatGridListModule, MatCardModule, MatMenuModule, MatTableModule, MatDividerModule, MatInputModule, MatFormFieldModule, MatProgressSpinnerModule, MatTooltipModule, MatSlideToggleModule, MatPaginatorModule, MatSortModule, MatChipsModule} from '@angular/material';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';

const modules = [
  CommonModule,
  BrowserModule,
  BrowserAnimationsModule,
  MatButtonModule,
  MatCheckboxModule,
  MatToolbarModule,
  MatButtonModule,
  MatTooltipModule,
  MatSidenavModule,
  MatIconModule,
  MatListModule,
  MatGridListModule,
  MatCardModule,
  MatChipsModule,
  MatMenuModule,
  MatTableModule,
  MatPaginatorModule,
  MatSortModule,
  MatDividerModule,
  MatInputModule,
  MatFormFieldModule,
  MatProgressSpinnerModule,
  MatSlideToggleModule
]
@NgModule({
  declarations: [],
  imports: [...modules],
  exports: [...modules]
})
export class MaterialModule { }
