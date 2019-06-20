import { Injectable } from '@angular/core';
import { Subject, Observable, Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import {JwtHelperService} from '@auth0/angular-jwt';
import * as jwt_decode from 'jwt-decode';
import { AppConstants } from '../helpers/constants';
import { RegisterUser } from './models/registerUser.model';
import { SignupUser } from './models/signupUser.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  token: string;
  invalidLogin = false;
  onLogout = new Subject<void>();
  
  constructor(private route: Router,
              private httpClient: HttpClient,
              private jwtHelper: JwtHelperService) {

        this.token = localStorage.getItem(AppConstants.jwtKey);
        console.log("initial jwt ", this.token);
  }

  signupUser(userData: RegisterUser): Observable<SignupUser> {
    console.log('registering ', userData);
    const url = `${AppConstants.baseUrl}/users/signup`;

    return this.httpClient.post<SignupUser>(url, userData);
  }

  signinUser(email: string, password: string): Subscription {
    const url = `${AppConstants.baseUrl}/users/signin`;

    return this.httpClient.post(url, {email, password})
        .subscribe((response: {token: string, status: string}) => {
            this.token = response.token;
            localStorage.setItem(AppConstants.jwtKey, this.token);
            this.invalidLogin = false;
            this.route.navigate(['/devices']);
            return true;
          },(error) => {
              this.invalidLogin = true;
              console.log('error on signin ', error);
        });
  }

  getToken(): string {
    const token = localStorage.getItem(AppConstants.jwtKey);
    return token;
  }

  getUserInfo(): any {
    const decoded = jwt_decode(this.getToken());
    return decoded;
  }

  isTokenExpired(token: string): boolean {
      if (!token) {
        return false;
      }
      return this.jwtHelper.isTokenExpired(token);
  }

  isAuthenticated(): boolean {
    if (!this.token) {
      return false;
    } else if (this.isTokenExpired(this.token)) {
      return false;
    } else {
      return true;
    }
  }

  private removeToken() {
    localStorage.removeItem(AppConstants.jwtKey);
    this.token = null;
  }

  logout() {
    if (confirm('Are you sure you want to logout ?')){
      this.removeToken();
      this.route.navigate(['/']);
      this.onLogout.next();
    }
  }

  redirectToLogin() {
    this.removeToken();
    this.route.navigate(['/signin']);
  }
}
