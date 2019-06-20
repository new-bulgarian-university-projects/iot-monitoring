import { Injectable } from '@angular/core';
import { User } from '../auth/models/user.model';
import { HttpClient } from '@angular/common/http';
import { AppConstants } from '../helpers/constants';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  public getUser(userId: string): Observable<User> {
    return this.httpClient.get<User>(AppConstants.baseUrl + `/users/${userId}`);
  }
}